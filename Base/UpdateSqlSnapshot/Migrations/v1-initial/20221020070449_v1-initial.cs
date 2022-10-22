using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Base.UpdateSqlSnapshot.Migrations.v1initial
{
    public partial class v1initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "contest_base",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "比赛ID")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    organization_id = table.Column<long>(type: "bigint", nullable: false, comment: "承办组织"),
                    owner_id = table.Column<long>(type: "bigint", nullable: false, comment: "所有者"),
                    type = table.Column<int>(type: "int", nullable: false, comment: "赛事类型"),
                    writers = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true, comment: "出题人"),
                    start_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "开始时间"),
                    end_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "结束时间"),
                    title = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false, comment: "比赛名称"),
                    profile = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true, comment: "赛事的简单描述"),
                    status = table.Column<int>(type: "int", nullable: false, comment: "比赛状态枚举"),
                    update_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "更新时间"),
                    create_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "创建时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contest_base", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "contest_content",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "比赛内容ID")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    contest_id = table.Column<long>(type: "bigint", nullable: false, comment: "比赛ID"),
                    content = table.Column<string>(type: "text", nullable: true, comment: "赛事描述文字")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contest_content", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "contest_problem",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "赛题ID")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    contest_id = table.Column<long>(type: "bigint", nullable: false, comment: "比赛ID"),
                    problem_id = table.Column<long>(type: "bigint", nullable: false, comment: "题目ID"),
                    submission_count = table.Column<long>(type: "bigint", nullable: false, comment: "提交数"),
                    accepted_count = table.Column<long>(type: "bigint", nullable: false, comment: "AC数"),
                    version = table.Column<long>(type: "bigint", nullable: false, comment: "版本"),
                    status = table.Column<int>(type: "int", nullable: false, comment: "状态枚举"),
                    properties = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true, comment: "分数、语言等信息的JSON格式")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contest_problem", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "object_tag",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "主体-标签ID")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    target_id = table.Column<long>(type: "bigint", nullable: false, comment: "主体ID"),
                    target_type = table.Column<int>(type: "int", nullable: false, comment: "目标主体类型"),
                    tag_id = table.Column<long>(type: "bigint", nullable: false, comment: "标签ID"),
                    status = table.Column<int>(type: "int", nullable: false, comment: "状态")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_object_tag", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "organization",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "自增ID")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false, comment: "组织名"),
                    description = table.Column<string>(type: "nvarchar(max)", maxLength: 4096, nullable: true, comment: "描述"),
                    owner = table.Column<long>(type: "bigint", nullable: false, comment: "组织所有人"),
                    parent_id = table.Column<long>(type: "bigint", nullable: false, comment: "父组织"),
                    avatar = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true, comment: "头像"),
                    status = table.Column<int>(type: "int", nullable: false, comment: "状态"),
                    update_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "更新时间"),
                    create_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "创建时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "problem_base",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "题目ID")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false, comment: "题目标题"),
                    writer_id = table.Column<long>(type: "bigint", nullable: false, comment: "出题人ID"),
                    properties = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true, comment: "针对不同题目类型的描述JSON"),
                    show_id = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false, comment: "展示的题号"),
                    source_id = table.Column<long>(type: "bigint", nullable: false, comment: "题目来源"),
                    submission_count = table.Column<long>(type: "bigint", nullable: true, defaultValueSql: "'0'", comment: "提交数"),
                    accepted_count = table.Column<long>(type: "bigint", nullable: true, defaultValueSql: "'0'", comment: "通过数"),
                    type = table.Column<int>(type: "int", nullable: false, comment: "题目类型"),
                    status = table.Column<int>(type: "int", nullable: false, comment: "题目状态"),
                    version = table.Column<long>(type: "bigint", nullable: true, defaultValueSql: "'0'", comment: "版本"),
                    update_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "更新时间"),
                    create_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "创建时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_problem_base", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "problem_content",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "题目内容ID")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    problem_id = table.Column<long>(type: "bigint", nullable: false, comment: "题目ID"),
                    content = table.Column<string>(type: "text", nullable: true, comment: "题目具体内容")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_problem_content", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "problem_source",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "自增ID")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: "题目来源名"),
                    url = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: "题目源主页"),
                    properties = table.Column<string>(type: "nvarchar(max)", maxLength: 4098, nullable: true, comment: "题目show_id组合源链接策略")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_problem_source", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "problemset",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "题单ID")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false, comment: "题单名称"),
                    creator_id = table.Column<long>(type: "bigint", nullable: false, comment: "创建者ID"),
                    description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true, comment: "简短描述"),
                    status = table.Column<int>(type: "int", nullable: false, comment: "状态枚举"),
                    update_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "更新时间"),
                    create_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "创建时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_problemset", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "problemset_problem",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "题单-题目ID")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    problemset_id = table.Column<long>(type: "bigint", nullable: false, comment: "题单ID"),
                    problem_id = table.Column<long>(type: "bigint", nullable: false, comment: "题目ID"),
                    properties = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true, comment: "JSON"),
                    status = table.Column<int>(type: "int", nullable: false, comment: "状态枚举")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_problemset_problem", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "register",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "自增ID")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    contest_id = table.Column<long>(type: "bigint", nullable: false, comment: "比赛ID"),
                    registrant_id = table.Column<long>(type: "bigint", nullable: false, comment: "注册人ID"),
                    registrant_type = table.Column<int>(type: "int", nullable: false, comment: "注册人类型"),
                    team_id = table.Column<long>(type: "bigint", nullable: true, comment: "队伍ID"),
                    extra = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true, comment: "额外信息"),
                    status = table.Column<int>(type: "int", nullable: false, comment: "比赛状态枚举"),
                    update_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "更新时间"),
                    create_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "创建时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_register", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "score",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "ScoreID")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false, comment: "Score名称"),
                    target_type = table.Column<int>(type: "int", nullable: false, comment: "目标主体类型"),
                    target_id = table.Column<long>(type: "bigint", nullable: false, comment: "目标主体ID"),
                    agg_id = table.Column<long>(type: "bigint", nullable: false, comment: "聚合基准"),
                    value = table.Column<long>(type: "bigint", nullable: false, comment: "得分"),
                    status = table.Column<int>(type: "int", nullable: false, comment: "状态枚举"),
                    type = table.Column<int>(type: "int", nullable: false, comment: "类型")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_score", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "solution_base",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "题解ID")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    writer_id = table.Column<long>(type: "bigint", nullable: false, comment: "作者ID"),
                    contest_id = table.Column<long>(type: "bigint", nullable: true, comment: "关联的比赛"),
                    problem_id = table.Column<long>(type: "bigint", nullable: true, comment: "关联的题目"),
                    status = table.Column<int>(type: "int", nullable: false, comment: "状态枚举"),
                    update_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "更新时间"),
                    create_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "创建时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_solution_base", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "solution_content",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "题解内容ID")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    solution_id = table.Column<long>(type: "bigint", nullable: false, comment: "题解ID"),
                    content = table.Column<string>(type: "text", nullable: true, comment: "题解内容")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_solution_content", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "submission_base",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "提交ID")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    submit_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "提交时间"),
                    submitter_id = table.Column<long>(type: "bigint", nullable: false, comment: "提交者ID"),
                    submitter_type = table.Column<int>(type: "int", nullable: false, comment: "提交者类型（团队或个人）"),
                    status = table.Column<int>(type: "int", nullable: false, comment: "提交结果"),
                    type = table.Column<int>(type: "int", nullable: false, comment: "提交类型"),
                    contest_id = table.Column<long>(type: "bigint", nullable: true, comment: "关联的比赛"),
                    problem_id = table.Column<long>(type: "bigint", nullable: true, comment: "关联的题目"),
                    update_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "更新时间"),
                    create_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "创建时间"),
                    properties = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true, comment: "特定配置JSON")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_submission_base", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "submission_content",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "提交内容ID")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    submission_id = table.Column<long>(type: "bigint", nullable: false, comment: "提交ID"),
                    content = table.Column<string>(type: "text", nullable: true, comment: "提交内容")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_submission_content", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tag",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "标签ID")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false, comment: "标签名称"),
                    color = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true, comment: "标签颜色"),
                    target_type = table.Column<int>(type: "int", nullable: false, comment: "目标主体类型"),
                    properties = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true, comment: "配置项JSON"),
                    status = table.Column<int>(type: "int", nullable: false, comment: "状态枚举")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tag", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "team",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "自增ID")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false, comment: "队伍名"),
                    signature = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true, comment: "个性签名"),
                    description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true, comment: "队伍介绍"),
                    leader = table.Column<long>(type: "bigint", nullable: false, comment: "队长"),
                    organization_id = table.Column<long>(type: "bigint", nullable: false, comment: "所属组织"),
                    avatar = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true, comment: "头像"),
                    status = table.Column<int>(type: "int", nullable: false, comment: "状态"),
                    update_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "更新时间"),
                    create_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "创建时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_team", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "team_user",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "自增ID")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<long>(type: "bigint", nullable: false, comment: "用户Id"),
                    team_id = table.Column<long>(type: "bigint", nullable: false, comment: "队伍Id"),
                    user_type = table.Column<int>(type: "int", nullable: false, comment: "用户类型"),
                    status = table.Column<int>(type: "int", nullable: false, comment: "状态"),
                    create_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "创建时间"),
                    update_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "更新时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_team_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "自增ID")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<long>(type: "bigint", nullable: false, comment: "用户ID"),
                    username = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false, comment: "用户名"),
                    password = table.Column<string>(type: "nvarchar(130)", maxLength: 130, nullable: false, comment: "密码"),
                    salt = table.Column<string>(type: "nvarchar(130)", maxLength: 130, nullable: false, comment: "密码加盐"),
                    phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true, comment: "电话号码"),
                    email = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true, comment: "邮箱"),
                    signature = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true, comment: "个性签名"),
                    organization_id = table.Column<long>(type: "bigint", nullable: false, comment: "所属组织"),
                    nickname = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true, comment: "昵称"),
                    realname = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true, comment: "真名"),
                    avatar = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true, comment: "头像"),
                    user_type = table.Column<int>(type: "int", nullable: false, defaultValueSql: "'3'", comment: "用户类型1:super admin,2:admin,3:user"),
                    extra = table.Column<string>(type: "nvarchar(max)", maxLength: 4096, nullable: true, comment: "额外信息"),
                    allowed_ip = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true, comment: "允许访问的IP"),
                    status = table.Column<int>(type: "int", nullable: false, comment: "状态"),
                    update_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "更新时间"),
                    create_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "创建时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_login",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "自增ID")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<long>(type: "bigint", nullable: false, comment: "用户ID"),
                    ip = table.Column<long>(type: "bigint", nullable: false, comment: "登录IP"),
                    device_id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: "设备ID"),
                    platform = table.Column<int>(type: "int", nullable: false, comment: "平台"),
                    login_type = table.Column<int>(type: "int", nullable: false, comment: "登录类型"),
                    time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "创建时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_login", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "idx_type_owner",
                table: "contest_base",
                columns: new[] { "type", "owner_id" });

            migrationBuilder.CreateIndex(
                name: "idx_contest",
                table: "contest_problem",
                column: "contest_id");

            migrationBuilder.CreateIndex(
                name: "idx_target_type_id",
                table: "object_tag",
                columns: new[] { "target_id", "target_type" });

            migrationBuilder.CreateIndex(
                name: "idx_owner",
                table: "organization",
                column: "owner");

            migrationBuilder.CreateIndex(
                name: "idx_parent",
                table: "organization",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "idx_type_source_show",
                table: "problem_base",
                columns: new[] { "type", "source_id", "show_id" });

            migrationBuilder.CreateIndex(
                name: "idx_type_writer",
                table: "problem_base",
                columns: new[] { "type", "writer_id" });

            migrationBuilder.CreateIndex(
                name: "idx_creator",
                table: "problemset",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "idx_problemset_id",
                table: "problemset_problem",
                column: "problemset_id");

            migrationBuilder.CreateIndex(
                name: "idx_contest1",
                table: "register",
                columns: new[] { "contest_id", "status" });

            migrationBuilder.CreateIndex(
                name: "idx_registrant",
                table: "register",
                columns: new[] { "registrant_id", "status" });

            migrationBuilder.CreateIndex(
                name: "idx_type_agg_id_status_value",
                table: "score",
                columns: new[] { "type", "agg_id", "status", "value" });

            migrationBuilder.CreateIndex(
                name: "idx_contest2",
                table: "solution_base",
                column: "contest_id");

            migrationBuilder.CreateIndex(
                name: "idx_problem",
                table: "solution_base",
                column: "problem_id");

            migrationBuilder.CreateIndex(
                name: "idx_writer",
                table: "solution_base",
                column: "writer_id");

            migrationBuilder.CreateIndex(
                name: "idx_contest_create_time",
                table: "submission_base",
                columns: new[] { "contest_id", "create_time" });

            migrationBuilder.CreateIndex(
                name: "idx_contest_problem",
                table: "submission_base",
                columns: new[] { "contest_id", "problem_id" });

            migrationBuilder.CreateIndex(
                name: "idx_contest_submitter",
                table: "submission_base",
                columns: new[] { "contest_id", "submitter_id" });

            migrationBuilder.CreateIndex(
                name: "idx_submitter_problem",
                table: "submission_base",
                columns: new[] { "submitter_id", "problem_id" });

            migrationBuilder.CreateIndex(
                name: "idx_team",
                table: "team_user",
                column: "team_id");

            migrationBuilder.CreateIndex(
                name: "idx_user",
                table: "team_user",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "idx_email",
                table: "user",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "idx_phone",
                table: "user",
                column: "phone");

            migrationBuilder.CreateIndex(
                name: "idx_status",
                table: "user",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "idx_uid",
                table: "user",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "idx_user_name",
                table: "user",
                column: "username");

            migrationBuilder.CreateIndex(
                name: "idx_user_device_time",
                table: "user_login",
                columns: new[] { "user_id", "device_id", "time" });

            migrationBuilder.CreateIndex(
                name: "idx_user_platform_time",
                table: "user_login",
                columns: new[] { "user_id", "platform", "time" });

            migrationBuilder.CreateIndex(
                name: "idx_user_time",
                table: "user_login",
                columns: new[] { "user_id", "time" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contest_base");

            migrationBuilder.DropTable(
                name: "contest_content");

            migrationBuilder.DropTable(
                name: "contest_problem");

            migrationBuilder.DropTable(
                name: "object_tag");

            migrationBuilder.DropTable(
                name: "organization");

            migrationBuilder.DropTable(
                name: "problem_base");

            migrationBuilder.DropTable(
                name: "problem_content");

            migrationBuilder.DropTable(
                name: "problem_source");

            migrationBuilder.DropTable(
                name: "problemset");

            migrationBuilder.DropTable(
                name: "problemset_problem");

            migrationBuilder.DropTable(
                name: "register");

            migrationBuilder.DropTable(
                name: "score");

            migrationBuilder.DropTable(
                name: "solution_base");

            migrationBuilder.DropTable(
                name: "solution_content");

            migrationBuilder.DropTable(
                name: "submission_base");

            migrationBuilder.DropTable(
                name: "submission_content");

            migrationBuilder.DropTable(
                name: "tag");

            migrationBuilder.DropTable(
                name: "team");

            migrationBuilder.DropTable(
                name: "team_user");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "user_login");
        }
    }
}
