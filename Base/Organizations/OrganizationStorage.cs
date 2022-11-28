using CUGOJ.Backend.Base.DAO.Context;
using CUGOJ.Backend.Share.Common.Models;
using CUGOJ.Backend.Share.Common.Organizations;
using CUGOJ.Backend.Share.Common.VersionContainer;
using CUGOJ.Backend.Tools.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Backend.Base.Organizations
{
    public class OrganizationStorage : IVersionItemStorage<IVersionItem>, IOrganizationQuery
    {
        private readonly Dictionary<long, OrganizationBase> organizations = new();
        private readonly Dictionary<long, List<UserOrganizationLinkBase>> userOrganizations = new();

        private readonly ReaderWriterLockSlim _lock = new();
        public ReaderWriterLockSlim Lock => _lock;

        public Task<IEnumerable<IVersionItem>> GetAllVersionItems()
        {
            var res = new List<IVersionItem>();
            res.AddRange(organizations.Values);
            foreach (var user in userOrganizations.Values)
            {
                res.AddRange(user);
            }
            return Task.FromResult((IEnumerable<IVersionItem>)res);
        }

        public async Task Init()
        {
            organizations.Clear();
            userOrganizations.Clear();
            var version = CommonTools.GetUTCUnixMilli();
            using var context = new CUGOJContext();
            var _organizations = await (from o in context.Organizations
                                        select new OrganizationBase
                                        {
                                            Id = o.Id,
                                            Parent = o.ParentId,
                                            OrganizationType = o.OrganizationType,
                                            Role = o.Role,
                                            Version = version,
                                        }).ToArrayAsync();
            var _users = await (from u in context.UserOrganizationLinks
                                select new UserOrganizationLinkBase
                                {
                                    Id = u.Id,
                                    OrganizationId = u.Id,
                                    Role = u.Role,
                                    Version = version,
                                    UserOrganizationLinkBaseType = UserOrganizationLinkBase.UserOrganizationLinkBaseTypeEnum.Add
                                }).ToArrayAsync();
            foreach (var org in _organizations)
            {
                PushOrganization(org);
            }
            foreach (var user in _users)
            {
                PushUser(user);
            }
        }

        private void PushOrganization(OrganizationBase item)
        {
            organizations[item.Id] = item;
        }

        private void PushUser(UserOrganizationLinkBase item)
        {
            if (!userOrganizations.TryGetValue(item.Id, out var user))
            {
                user = new List<UserOrganizationLinkBase>();
            }
            if (item.UserOrganizationLinkBaseType == UserOrganizationLinkBase.UserOrganizationLinkBaseTypeEnum.Add)
            {
                var found = false;
                foreach (var link in user)
                {
                    if (link.OrganizationId == item.OrganizationId)
                    {
                        link.Version = item.Version;
                        link.Role = item.Role;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    user.Add(item);
                }
            }
            else
            {
                for (int i = 0; i < user.Count; i++)
                {
                    if (user[i].OrganizationId == item.OrganizationId)
                    {
                        user.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        public Task PushVersionItems(IEnumerable<IVersionItem> items)
        {
            foreach (var item in items)
            {
                if (item is OrganizationBase @organizationBase)
                {
                    PushOrganization(organizationBase);
                }
                else if (item is UserOrganizationLinkBase @userOrganizationLinkBase)
                {
                    PushUser(userOrganizationLinkBase);
                }
            }
            return Task.CompletedTask;
        }

        private IEnumerable<OrganizationBase>GetOrganization(IEnumerable<OrganizationBase>root,long depLimit)
        {
            HashSet<long> orgIdSet = new(from org in root select org.Id);
            var organizationList = new List<OrganizationBase>(root);
            while (root.Any() && depLimit > 0)
            {
                root = (from rt in root
                        join org in organizations
                        on rt.Parent equals org.Key
                        where rt.Parent != 0 && !orgIdSet.Contains(rt.Parent)
                        select org.Value);
                orgIdSet.UnionWith(from org in root select org.Id);
                organizationList.AddRange(root);
                depLimit--;
            }
            return organizationList;
        }

        public Task<IEnumerable<OrganizationBase>> GetOrganizationByUser(long userId, int maxDep = 0, IEnumerable<Share.Common.Models.Organization.OrganizationTypeEnum>? TypeLimit = null)
        {
            if (!userOrganizations.TryGetValue(userId, out var organizationList))
            {
                return Task.FromResult((IEnumerable<OrganizationBase>)new List<OrganizationBase>());
            }
            var root = (from userOrg in organizationList
                        join org in organizations
                        on userOrg.OrganizationId equals org.Key
                        where userOrg.OrganizationId != 0
                        select org.Value);
            return Task.FromResult(GetOrganization(root, maxDep == 0 ? long.MaxValue : maxDep - 1));
        }

        public Task<IEnumerable<OrganizationBase>> GetOrganizationByOrg(long orgId, int maxDep = 0, IEnumerable<Share.Common.Models.Organization.OrganizationTypeEnum>? TypeLimit = null)
        {
            if (!organizations.TryGetValue(orgId, out var organization)||organization.Parent==0)
            {
                return Task.FromResult((IEnumerable<OrganizationBase>)new List<OrganizationBase>());
            }
            return Task.FromResult(GetOrganization((from org in organizations
                                                    where org.Key == organization.Parent
                                                    select org.Value), maxDep == 0 ? long.MaxValue : maxDep - 1));
        }
    }
}
