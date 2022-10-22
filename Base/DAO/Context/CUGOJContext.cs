using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CUGOJ.Backend.Share.Common.Models;

namespace CUGOJ.Backend.Base.DAO.Context
{
    public partial class CUGOJContext : DbContext
    {
        public CUGOJContext()
        {

        }
        public virtual DbSet<ContestBase> ContestBases { get; set; } = null!;
        public virtual DbSet<ContestContent> ContestContents { get; set; } = null!;
        public virtual DbSet<ContestProblem> ContestProblems { get; set; } = null!;
        public virtual DbSet<ObjectTag> ObjectTags { get; set; } = null!;
        public virtual DbSet<Organization> Organizations { get; set; } = null!;
        public virtual DbSet<ProblemBase> ProblemBases { get; set; } = null!;
        public virtual DbSet<ProblemContent> ProblemContents { get; set; } = null!;
        public virtual DbSet<ProblemSource> ProblemSources { get; set; } = null!;
        public virtual DbSet<Problemset> Problemsets { get; set; } = null!;
        public virtual DbSet<ProblemsetProblem> ProblemsetProblems { get; set; } = null!;
        public virtual DbSet<Register> Registers { get; set; } = null!;
        public virtual DbSet<Score> Scores { get; set; } = null!;
        public virtual DbSet<SolutionBase> SolutionBases { get; set; } = null!;
        public virtual DbSet<SolutionContent> SolutionContents { get; set; } = null!;
        public virtual DbSet<SubmissionBase> SubmissionBases { get; set; } = null!;
        public virtual DbSet<SubmissionContent> SubmissionContents { get; set; } = null!;
        public virtual DbSet<Tag> Tags { get; set; } = null!;
        public virtual DbSet<Team> Teams { get; set; } = null!;
        public virtual DbSet<TeamUser> TeamUsers { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserLogin> UserLogins { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Properties.StoreDBConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContestBase>(entity =>
            {
                entity.ToTable("contest_base");

                entity.HasIndex(e => new { e.Type, e.OwnerId }, "idx_type_owner");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("比赛ID");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("GETDATE()")
                    .HasComment("创建时间");

                entity.Property(e => e.EndTime)
                    .HasColumnName("end_time")
                    .HasDefaultValueSql("GETDATE()")
                    .HasComment("结束时间");

                entity.Property(e => e.OrganizationId)
                    .HasColumnName("organization_id")
                    .HasComment("承办组织");

                entity.Property(e => e.OwnerId)
                    .HasColumnName("owner_id")
                    .HasComment("所有者");

                entity.Property(e => e.Profile)
                    .HasMaxLength(1024)
                    .HasColumnName("profile")
                    .HasComment("赛事的简单描述");

                entity.Property(e => e.StartTime)
                    .HasColumnName("start_time")
                    .HasDefaultValueSql("GETDATE()")
                    .HasComment("开始时间");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasComment("比赛状态枚举");

                entity.Property(e => e.Title)
                    .HasMaxLength(64)
                    .HasColumnName("title")
                    .HasComment("比赛名称");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasComment("赛事类型");

                entity.Property(e => e.UpdateTime)
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("GETDATE()")
                    .HasComment("更新时间");

                entity.Property(e => e.Writers)
                    .HasMaxLength(512)
                    .HasColumnName("writers")
                    .HasComment("出题人");
            });

            modelBuilder.Entity<ContestContent>(entity =>
            {
                entity.ToTable("contest_content");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("比赛内容ID");

                entity.Property(e => e.Content)
                    .HasColumnType("text")
                    .HasColumnName("content")
                    .HasComment("赛事描述文字");

                entity.Property(e => e.ContestId)
                    .HasColumnName("contest_id")
                    .HasComment("比赛ID");
            });

            modelBuilder.Entity<ContestProblem>(entity =>
            {
                entity.ToTable("contest_problem");

                entity.HasIndex(e => e.ContestId, "idx_contest");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("赛题ID");

                entity.Property(e => e.AcceptedCount)
                    .HasColumnName("accepted_count")
                    .HasComment("AC数");

                entity.Property(e => e.ContestId)
                    .HasColumnName("contest_id")
                    .HasComment("比赛ID");

                entity.Property(e => e.ProblemId)
                    .HasColumnName("problem_id")
                    .HasComment("题目ID");

                entity.Property(e => e.Properties)
                    .HasMaxLength(2048)
                    .HasColumnName("properties")
                    .HasComment("分数、语言等信息的JSON格式");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasComment("状态枚举");

                entity.Property(e => e.SubmissionCount)
                    .HasColumnName("submission_count")
                    .HasComment("提交数");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasComment("版本");
            });

            modelBuilder.Entity<ObjectTag>(entity =>
            {
                entity.ToTable("object_tag");

                entity.HasIndex(e => new { e.TargetId, e.TargetType }, "idx_target_type_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("主体-标签ID");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasComment("状态");

                entity.Property(e => e.TagId)
                    .HasColumnName("tag_id")
                    .HasComment("标签ID");

                entity.Property(e => e.TargetId)
                    .HasColumnName("target_id")
                    .HasComment("主体ID");

                entity.Property(e => e.TargetType)
                    .HasColumnName("target_type")
                    .HasComment("目标主体类型");
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.ToTable("organization");

                entity.HasIndex(e => e.Owner, "idx_owner");

                entity.HasIndex(e => e.ParentId, "idx_parent");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("自增ID");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(128)
                    .HasColumnName("avatar")
                    .HasComment("头像");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("GETDATE()")
                    .HasComment("创建时间");

                entity.Property(e => e.Description)
                    .HasMaxLength(4096)
                    .HasColumnName("description")
                    .HasComment("描述");

                entity.Property(e => e.Name)
                    .HasMaxLength(64)
                    .HasColumnName("name")
                    .HasComment("组织名");

                entity.Property(e => e.Owner)
                    .HasColumnName("owner")
                    .HasComment("组织所有人");

                entity.Property(e => e.ParentId)
                    .HasColumnName("parent_id")
                    .HasComment("父组织");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasComment("状态");

                entity.Property(e => e.UpdateTime)
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("GETDATE()")
                    .HasComment("更新时间");
            });

            modelBuilder.Entity<ProblemBase>(entity =>
            {
                entity.ToTable("problem_base");

                entity.HasIndex(e => new { e.Type, e.SourceId, e.ShowId }, "idx_type_source_show");

                entity.HasIndex(e => new { e.Type, e.WriterId }, "idx_type_writer");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("题目ID");

                entity.Property(e => e.AcceptedCount)
                    .HasColumnName("accepted_count")
                    .HasDefaultValueSql("'0'")
                    .HasComment("通过数");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("GETDATE()")
                    .HasComment("创建时间");

                entity.Property(e => e.Properties)
                    .HasMaxLength(1024)
                    .HasColumnName("properties")
                    .HasComment("针对不同题目类型的描述JSON");

                entity.Property(e => e.ShowId)
                    .HasMaxLength(16)
                    .HasColumnName("show_id")
                    .HasComment("展示的题号");

                entity.Property(e => e.SourceId)
                    .HasColumnName("source_id")
                    .HasComment("题目来源");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasComment("题目状态");

                entity.Property(e => e.SubmissionCount)
                    .HasColumnName("submission_count")
                    .HasDefaultValueSql("'0'")
                    .HasComment("提交数");

                entity.Property(e => e.Title)
                    .HasMaxLength(512)
                    .HasColumnName("title")
                    .HasComment("题目标题");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasComment("题目类型");

                entity.Property(e => e.UpdateTime)
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("GETDATE()")
                    .HasComment("更新时间");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasDefaultValueSql("'0'")
                    .HasComment("版本");

                entity.Property(e => e.WriterId)
                    .HasColumnName("writer_id")
                    .HasComment("出题人ID");
            });

            modelBuilder.Entity<ProblemContent>(entity =>
            {
                entity.ToTable("problem_content");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("题目内容ID");

                entity.Property(e => e.Content)
                    .HasColumnType("text")
                    .HasColumnName("content")
                    .HasComment("题目具体内容");

                entity.Property(e => e.ProblemId)
                    .HasColumnName("problem_id")
                    .HasComment("题目ID");
            });

            modelBuilder.Entity<ProblemSource>(entity =>
            {
                entity.ToTable("problem_source");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("自增ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(128)
                    .HasColumnName("name")
                    .HasComment("题目来源名");

                entity.Property(e => e.Properties)
                    .HasMaxLength(4098)
                    .HasColumnName("properties")
                    .HasComment("题目show_id组合源链接策略");

                entity.Property(e => e.Url)
                    .HasMaxLength(128)
                    .HasColumnName("url")
                    .HasComment("题目源主页");
            });

            modelBuilder.Entity<Problemset>(entity =>
            {
                entity.ToTable("problemset");

                entity.HasIndex(e => e.CreatorId, "idx_creator");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("题单ID");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("GETDATE()")
                    .HasComment("创建时间");

                entity.Property(e => e.CreatorId)
                    .HasColumnName("creator_id")
                    .HasComment("创建者ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(1024)
                    .HasColumnName("description")
                    .HasComment("简短描述");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasComment("状态枚举");

                entity.Property(e => e.Title)
                    .HasMaxLength(64)
                    .HasColumnName("title")
                    .HasComment("题单名称");

                entity.Property(e => e.UpdateTime)
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("GETDATE()")
                    .HasComment("更新时间");
            });

            modelBuilder.Entity<ProblemsetProblem>(entity =>
            {
                entity.ToTable("problemset_problem");

                entity.HasIndex(e => e.ProblemsetId, "idx_problemset_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("题单-题目ID");

                entity.Property(e => e.ProblemId)
                    .HasColumnName("problem_id")
                    .HasComment("题目ID");

                entity.Property(e => e.ProblemsetId)
                    .HasColumnName("problemset_id")
                    .HasComment("题单ID");

                entity.Property(e => e.Properties)
                    .HasMaxLength(1024)
                    .HasColumnName("properties")
                    .HasComment("JSON");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasComment("状态枚举");
            });

            modelBuilder.Entity<Register>(entity =>
            {
                entity.ToTable("register");

                entity.HasIndex(e => new { e.ContestId, e.Status }, "idx_contest");

                entity.HasIndex(e => new { e.RegistrantId, e.Status }, "idx_registrant");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("自增ID");

                entity.Property(e => e.ContestId)
                    .HasColumnName("contest_id")
                    .HasComment("比赛ID");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("GETDATE()")
                    .HasComment("创建时间");

                entity.Property(e => e.Extra)
                    .HasMaxLength(1024)
                    .HasColumnName("extra")
                    .HasComment("额外信息");

                entity.Property(e => e.RegistrantId)
                    .HasColumnName("registrant_id")
                    .HasComment("注册人ID");

                entity.Property(e => e.RegistrantType)
                    .HasColumnName("registrant_type")
                    .HasComment("注册人类型");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasComment("比赛状态枚举");

                entity.Property(e => e.TeamId)
                    .HasColumnName("team_id")
                    .HasComment("队伍ID");

                entity.Property(e => e.UpdateTime)
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("GETDATE()")
                    .HasComment("更新时间");
            });

            modelBuilder.Entity<Score>(entity =>
            {
                entity.ToTable("score");

                entity.HasIndex(e => new { e.Type, e.AggId, e.Status, e.Value }, "idx_type_agg_id_status_value");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("ScoreID");

                entity.Property(e => e.AggId)
                    .HasColumnName("agg_id")
                    .HasComment("聚合基准");

                entity.Property(e => e.Name)
                    .HasMaxLength(32)
                    .HasColumnName("name")
                    .HasComment("Score名称");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasComment("状态枚举");

                entity.Property(e => e.TargetId)
                    .HasColumnName("target_id")
                    .HasComment("目标主体ID");

                entity.Property(e => e.TargetType)
                    .HasColumnName("target_type")
                    .HasComment("目标主体类型");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasComment("类型");

                entity.Property(e => e.Value)
                    .HasColumnName("value")
                    .HasComment("得分");
            });

            modelBuilder.Entity<SolutionBase>(entity =>
            {
                entity.ToTable("solution_base");

                entity.HasIndex(e => e.ContestId, "idx_contest");

                entity.HasIndex(e => e.ProblemId, "idx_problem");

                entity.HasIndex(e => e.WriterId, "idx_writer");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("题解ID");

                entity.Property(e => e.ContestId)
                    .HasColumnName("contest_id")
                    .HasComment("关联的比赛");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("GETDATE()")
                    .HasComment("创建时间");

                entity.Property(e => e.ProblemId)
                    .HasColumnName("problem_id")
                    .HasComment("关联的题目");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasComment("状态枚举");

                entity.Property(e => e.UpdateTime)
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("GETDATE()")
                    .HasComment("更新时间");

                entity.Property(e => e.WriterId)
                    .HasColumnName("writer_id")
                    .HasComment("作者ID");
            });

            modelBuilder.Entity<SolutionContent>(entity =>
            {
                entity.ToTable("solution_content");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("题解内容ID");

                entity.Property(e => e.Content)
                    .HasColumnType("text")
                    .HasColumnName("content")
                    .HasComment("题解内容");

                entity.Property(e => e.SolutionId)
                    .HasColumnName("solution_id")
                    .HasComment("题解ID");
            });

            modelBuilder.Entity<SubmissionBase>(entity =>
            {
                entity.ToTable("submission_base");

                entity.HasIndex(e => new { e.ContestId, e.CreateTime }, "idx_contest_create_time");

                entity.HasIndex(e => new { e.ContestId, e.ProblemId }, "idx_contest_problem");

                entity.HasIndex(e => new { e.ContestId, e.SubmitterId }, "idx_contest_submitter");

                entity.HasIndex(e => new { e.SubmitterId, e.ProblemId }, "idx_submitter_problem");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("提交ID");

                entity.Property(e => e.ContestId)
                    .HasColumnName("contest_id")
                    .HasComment("关联的比赛");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("GETDATE()")
                    .HasComment("创建时间");

                entity.Property(e => e.ProblemId)
                    .HasColumnName("problem_id")
                    .HasComment("关联的题目");

                entity.Property(e => e.Properties)
                    .HasMaxLength(1024)
                    .HasColumnName("properties")
                    .HasComment("特定配置JSON");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasComment("提交结果");

                entity.Property(e => e.SubmitTime)
                    .HasColumnName("submit_time")
                    .HasDefaultValueSql("GETDATE()")
                    .HasComment("提交时间");

                entity.Property(e => e.SubmitterId)
                    .HasColumnName("submitter_id")
                    .HasComment("提交者ID");

                entity.Property(e => e.SubmitterType)
                    .HasColumnName("submitter_type")
                    .HasComment("提交者类型（团队或个人）");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasComment("提交类型");

                entity.Property(e => e.UpdateTime)
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("GETDATE()")
                    .HasComment("更新时间");
            });

            modelBuilder.Entity<SubmissionContent>(entity =>
            {
                entity.ToTable("submission_content");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("提交内容ID");

                entity.Property(e => e.Content)
                    .HasColumnType("text")
                    .HasColumnName("content")
                    .HasComment("提交内容");

                entity.Property(e => e.SubmissionId)
                    .HasColumnName("submission_id")
                    .HasComment("提交ID");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("tag");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("标签ID");

                entity.Property(e => e.Color)
                    .HasMaxLength(8)
                    .HasColumnName("color")
                    .HasComment("标签颜色");

                entity.Property(e => e.Name)
                    .HasMaxLength(32)
                    .HasColumnName("name")
                    .HasComment("标签名称");

                entity.Property(e => e.Properties)
                    .HasMaxLength(1024)
                    .HasColumnName("properties")
                    .HasComment("配置项JSON");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasComment("状态枚举");

                entity.Property(e => e.TargetType)
                    .HasColumnName("target_type")
                    .HasComment("目标主体类型");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.ToTable("team");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("自增ID");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(128)
                    .HasColumnName("avatar")
                    .HasComment("头像");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("GETDATE()")
                    .HasComment("创建时间");

                entity.Property(e => e.Description)
                    .HasMaxLength(512)
                    .HasColumnName("description")
                    .HasComment("队伍介绍");

                entity.Property(e => e.Leader)
                    .HasColumnName("leader")
                    .HasComment("队长");

                entity.Property(e => e.Name)
                    .HasMaxLength(64)
                    .HasColumnName("name")
                    .HasComment("队伍名");

                entity.Property(e => e.OrganizationId)
                    .HasColumnName("organization_id")
                    .HasComment("所属组织");

                entity.Property(e => e.Signature)
                    .HasMaxLength(512)
                    .HasColumnName("signature")
                    .HasComment("个性签名");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasComment("状态");

                entity.Property(e => e.UpdateTime)
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("GETDATE()")
                    .HasComment("更新时间");
            });

            modelBuilder.Entity<TeamUser>(entity =>
            {
                entity.ToTable("team_user");

                entity.HasIndex(e => e.TeamId, "idx_team");

                entity.HasIndex(e => e.UserId, "idx_user");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("自增ID");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("GETDATE()")
                    .HasComment("创建时间");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasComment("状态");

                entity.Property(e => e.TeamId)
                    .HasColumnName("team_id")
                    .HasComment("队伍Id");

                entity.Property(e => e.UpdateTime)
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("GETDATE()")
                    .HasComment("更新时间");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasComment("用户Id");

                entity.Property(e => e.UserType)
                    .HasColumnName("user_type")
                    .HasComment("用户类型");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.Email, "idx_email");

                entity.HasIndex(e => e.Phone, "idx_phone");

                entity.HasIndex(e => e.Status, "idx_status");

                entity.HasIndex(e => e.UserId, "idx_uid");

                entity.HasIndex(e => e.Username, "idx_user_name");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("自增ID");

                entity.Property(e => e.AllowedIp)
                    .HasMaxLength(2048)
                    .HasColumnName("allowed_ip")
                    .HasComment("允许访问的IP");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(128)
                    .HasColumnName("avatar")
                    .HasComment("头像");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("GETDATE()")
                    .HasComment("创建时间");

                entity.Property(e => e.Email)
                    .HasMaxLength(64)
                    .HasColumnName("email")
                    .HasComment("邮箱");

                entity.Property(e => e.Extra)
                    .HasMaxLength(4096)
                    .HasColumnName("extra")
                    .HasComment("额外信息");

                entity.Property(e => e.Nickname)
                    .HasMaxLength(64)
                    .HasColumnName("nickname")
                    .HasComment("昵称");

                entity.Property(e => e.OrganizationId)
                    .HasColumnName("organization_id")
                    .HasComment("所属组织");

                entity.Property(e => e.Password)
                    .HasMaxLength(130)
                    .HasColumnName("password")
                    .HasComment("密码");

                entity.Property(e => e.Phone)
                    .HasMaxLength(30)
                    .HasColumnName("phone")
                    .HasComment("电话号码");

                entity.Property(e => e.Realname)
                    .HasMaxLength(64)
                    .HasColumnName("realname")
                    .HasComment("真名");

                entity.Property(e => e.Salt)
                    .HasMaxLength(130)
                    .HasColumnName("salt")
                    .HasComment("密码加盐");

                entity.Property(e => e.Signature)
                    .HasMaxLength(512)
                    .HasColumnName("signature")
                    .HasComment("个性签名");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasComment("状态");

                entity.Property(e => e.UpdateTime)
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("GETDATE()")
                    .HasComment("更新时间");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasComment("用户ID");

                entity.Property(e => e.UserType)
                    .HasColumnName("user_type")
                    .HasDefaultValueSql("'3'")
                    .HasComment("用户类型1:super admin,2:admin,3:user");

                entity.Property(e => e.Username)
                    .HasMaxLength(40)
                    .HasColumnName("username")
                    .HasComment("用户名");
            });

            modelBuilder.Entity<UserLogin>(entity =>
            {
                entity.ToTable("user_login");

                entity.HasIndex(e => new { e.UserId, e.DeviceId, e.Time }, "idx_user_device_time");

                entity.HasIndex(e => new { e.UserId, e.Platform, e.Time }, "idx_user_platform_time");

                entity.HasIndex(e => new { e.UserId, e.Time }, "idx_user_time");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("自增ID");

                entity.Property(e => e.DeviceId)
                    .HasMaxLength(128)
                    .HasColumnName("device_id")
                    .HasComment("设备ID");

                entity.Property(e => e.Ip)
                    .HasColumnName("ip")
                    .HasComment("登录IP");

                entity.Property(e => e.LoginType)
                    .HasColumnName("login_type")
                    .HasComment("登录类型");

                entity.Property(e => e.Platform)
                    .HasColumnName("platform")
                    .HasComment("平台");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasDefaultValueSql("GETDATE()")
                    .HasComment("创建时间");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasComment("用户ID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
