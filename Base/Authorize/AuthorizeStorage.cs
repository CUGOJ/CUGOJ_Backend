using CUGOJ.Base.DAO.Context;
using CUGOJ.Share.Common.Authorize;
using CUGOJ.Share.Common.Models;
using CUGOJ.Share.Common.VersionContainer;
using CUGOJ.Tools.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Base.Authorize
{
    public class AuthorizeStorage : IVersionItemStorage<AuthorizePoBase>, IAuthorizeQuery
    {
        private class ResourceSet
        {
            public AuthorizePo.ResourceTypeEnum ResourceType { get; set; }
            public SortedList<long, AuthorizePoBase> ActionList { get; set; } = new();
            public AuthorizePoBase? GetAuthorizeByResourceId(long resourceId)
            {
                ActionList.TryGetValue(resourceId, out AuthorizePoBase? value);
                return value;
            }
            public IEnumerable<AuthorizePoBase> GetAllAuthorize()
            {
                return ActionList.Values;
            }
            public void SetAuthorize(AuthorizePoBase authorize)
            {
                if (ActionList.ContainsKey(authorize.ResourceId))
                {
                    ActionList[authorize.ResourceId] = authorize;
                }
                else
                {
                    ActionList.Add(authorize.ResourceId, authorize);
                }

            }
        }
        private class Grantee
        {
            public long GranteeId { get; set; }
            public AuthorizePo.GranteeTypeEnum GranteeType { get; set; }
            public long Role { get; set; }
            public long Version { get; private set; }
            private List<ResourceSet> resourceList = new();
            public ResourceSet GetResource(AuthorizePo.ResourceTypeEnum type)
            {
                var res = resourceList.Find(x => x.ResourceType == type);
                if (res == null)
                {
                    res = new ResourceSet
                    {
                        ResourceType = type
                    };
                    resourceList.Add(res);
                }
                return res;
            }
            public AuthorizePoBase? GetAuthorize(AuthorizePo.ResourceTypeEnum type, long resourceId)
            {
                return GetResource(type).GetAuthorizeByResourceId(resourceId);
            }

            public IEnumerable<AuthorizePoBase> GetAllAuthorizeByType(AuthorizePo.ResourceTypeEnum type)
            {
                return GetResource(type).GetAllAuthorize();
            }

            public void SetAuthorize(AuthorizePoBase authorize)
            {
                Version = long.Max(Version, authorize.Version);
                GetResource(authorize.ResourceType).SetAuthorize(authorize);
            }
            public override int GetHashCode()
            {
                return (GranteeId, GranteeType, Role).GetHashCode();
            }

            public override bool Equals(object? obj)
            {
                if (obj is Grantee @grantee)
                {
                    return grantee.GranteeType == GranteeType && grantee.GranteeId == GranteeId && grantee.Role == Role;
                }
                return base.Equals(obj);
            }
        }

        private readonly HashSet<Grantee> grantees = new();
        private Grantee? GetGrantee(long granteeId, AuthorizePo.GranteeTypeEnum granteeType, long role)
        {
            grantees.TryGetValue(new Grantee { GranteeId = granteeId, GranteeType = granteeType, Role = role }, out var grantee);
            return grantee;
        }
        public Task<IEnumerable<AuthorizePoBase>> GetAllVersionItems()
        {
            List<AuthorizePoBase> res = new();
            foreach (var grantee in grantees)
            {
                foreach (AuthorizePo.ResourceTypeEnum type in Enum.GetValues(typeof(AuthorizePo.ResourceTypeEnum)))
                {
                    res.AddRange(grantee.GetAllAuthorizeByType(type));
                }
            }
            return Task.FromResult(res as IEnumerable<AuthorizePoBase>);
        }

        public async Task Init()
        {
            using var context = new CUGOJContext();
            var version = CommonTools.GetUTCUnixMilli();
            var items = await context.Authorizes.Select(a => new AuthorizePoBase
            {
                Action = a.Action,
                GranteeId = a.GranteeId,
                GranteeType = a.GranteeType,
                ResourceId = a.ResourceId,
                ResourceType = a.ResourceType,
                Role = a.Role,
                Version = version
            }).ToListAsync();
            await PushVersionItems(items);
        }

        public Task PushVersionItems(IEnumerable<AuthorizePoBase> items)
        {
            foreach (var item in items)
            {
                var grantee = GetGrantee(item.GranteeId, item.GranteeType, item.Role);
                if (grantee == null)
                {
                    grantee = new()
                    {
                        GranteeId = item.GranteeId,
                        GranteeType = item.GranteeType,
                    };
                    grantees.Add(grantee);
                }
                grantee.SetAuthorize(item);
            }
            return Task.CompletedTask;
        }

        public Task<QueryAuthorizeResponse> QueryAuthorize(QueryAuthorizeRequest request)
        {
            QueryAuthorizeResponse res = new();
            Dictionary<AuthorizeResource, AuthorizePoBase> authorizes = new();
            foreach (var queryGrantee in request.Grantees)
            {
                var grantee = GetGrantee(queryGrantee.GranteeId, queryGrantee.GranteeType, queryGrantee.Role);
                if (grantee == null || grantee.Version <= queryGrantee.Version)
                    continue;
                IEnumerable<AuthorizePoBase?> authorizeList;
                if (request.ResourceType != null)
                {
                    authorizeList = grantee.GetAllAuthorizeByType((AuthorizePo.ResourceTypeEnum)request.ResourceType);
                }
                else
                {
                    authorizeList = request.Resources.Select(x => grantee.GetAuthorize(x.ResourceType, x.ResourceId)).Where(x => x != null);
                }
                res.ChangedGrantees.Add(new AuthorizeGrantee { GranteeId = queryGrantee.GranteeId, GranteeType = queryGrantee.GranteeType, Role = queryGrantee.Role, Version = grantee.Version });
                foreach (var auth in authorizeList)
                {
                    if (auth is null) continue;
                    var key = new AuthorizeResource { ResourceId = auth.ResourceId, ResourceType = auth.ResourceType };
                    if (authorizes.TryGetValue(key, out var existAuth))
                    {
                        existAuth.Action |= auth.Action;
                    }
                    else
                    {
                        authorizes.Add(key, auth);
                    }
                }
            }
            res.Authorizes = authorizes.Select(x => AuthorizeBase.ParseAuthorizeBase(x.Value)).ToList();
            return Task.FromResult(res);
        }
    }
}
