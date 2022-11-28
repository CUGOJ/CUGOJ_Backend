
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Backend.Share.DAO
{
    public interface ICUGOJContext
    {
        public DbSet<ContestBase> ContestBases { get; set; }
        public DbSet<ContestContent> ContestContents { get; set; }
        public DbSet<ContestProblem> ContestProblems { get; set; }
        public DbSet<ObjectTag> ObjectTags { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<ProblemBase> ProblemBases { get; set; }
        public DbSet<ProblemContent> ProblemContents { get; set; }
        public DbSet<ProblemSource> ProblemSources { get; set; }
        public DbSet<Problemset> Problemsets { get; set; }
        public DbSet<ProblemsetProblem> ProblemsetProblems { get; set; }
        public DbSet<Register> Registers { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<SolutionBase> SolutionBases { get; set; }
        public DbSet<SolutionContent> SolutionContents { get; set; }
        public DbSet<SubmissionBase> SubmissionBases { get; set; }
        public DbSet<SubmissionContent> SubmissionContents { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamUser> TeamUsers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<UserOrganizationLink> UserOrganizationLinks { get; set; }
        public DbSet<Authorize> Authorizes { get; set; }
    }
}
