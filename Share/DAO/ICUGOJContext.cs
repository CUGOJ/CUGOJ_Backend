using CUGOJ.Share.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Share.DAO
{
    public interface ICUGOJContext
    {
        public DbSet<ContestBasePo> ContestBases { get; set; }
        public DbSet<ContestContentPo> ContestContents { get; set; }
        public DbSet<ContestProblemPo> ContestProblems { get; set; }
        public DbSet<ObjectTagPo> ObjectTags { get; set; }
        public DbSet<OrganizationPo> Organizations { get; set; }
        public DbSet<ProblemBasePo> ProblemBases { get; set; }
        public DbSet<ProblemContentPo> ProblemContents { get; set; }
        public DbSet<ProblemSourcePo> ProblemSources { get; set; }
        public DbSet<ProblemsetPo> Problemsets { get; set; }
        public DbSet<ProblemsetProblemPo> ProblemsetProblems { get; set; }
        public DbSet<RegisterPo> Registers { get; set; }
        public DbSet<ScorePo> Scores { get; set; }
        public DbSet<SolutionBasePo> SolutionBases { get; set; }
        public DbSet<SolutionContentPo> SolutionContents { get; set; }
        public DbSet<SubmissionBasePo> SubmissionBases { get; set; }
        public DbSet<SubmissionContentPo> SubmissionContents { get; set; }
        public DbSet<TagPo> Tags { get; set; }
        public DbSet<TeamPo> Teams { get; set; }
        public DbSet<TeamUserPo> TeamUsers { get; set; }
        public DbSet<UserPo> Users { get; set; }
        public DbSet<UserLoginPo> UserLogins { get; set; }
        public DbSet<UserOrganizationLinkPo> UserOrganizationLinks { get; set; }
        public DbSet<AuthorizePo> Authorizes { get; set; }
    }
}
