IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE TABLE [contest_base] (
        [id] bigint NOT NULL IDENTITY,
        [organization_id] bigint NOT NULL,
        [owner_id] bigint NOT NULL,
        [type] int NOT NULL,
        [writers] nvarchar(512) NULL,
        [start_time] datetime2 NOT NULL DEFAULT (GETDATE()),
        [end_time] datetime2 NOT NULL DEFAULT (GETDATE()),
        [title] nvarchar(64) NOT NULL,
        [profile] nvarchar(1024) NULL,
        [status] int NOT NULL,
        [update_time] datetime2 NOT NULL DEFAULT (GETDATE()),
        [create_time] datetime2 NOT NULL DEFAULT (GETDATE()),
        CONSTRAINT [PK_contest_base] PRIMARY KEY ([id])
    );
    DECLARE @defaultSchema AS sysname;
    SET @defaultSchema = SCHEMA_NAME();
    DECLARE @description AS sql_variant;
    SET @description = N'比赛ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'contest_base', 'COLUMN', N'id';
    SET @description = N'承办组织';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'contest_base', 'COLUMN', N'organization_id';
    SET @description = N'所有者';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'contest_base', 'COLUMN', N'owner_id';
    SET @description = N'赛事类型';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'contest_base', 'COLUMN', N'type';
    SET @description = N'出题人';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'contest_base', 'COLUMN', N'writers';
    SET @description = N'开始时间';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'contest_base', 'COLUMN', N'start_time';
    SET @description = N'结束时间';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'contest_base', 'COLUMN', N'end_time';
    SET @description = N'比赛名称';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'contest_base', 'COLUMN', N'title';
    SET @description = N'赛事的简单描述';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'contest_base', 'COLUMN', N'profile';
    SET @description = N'比赛状态枚举';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'contest_base', 'COLUMN', N'status';
    SET @description = N'更新时间';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'contest_base', 'COLUMN', N'update_time';
    SET @description = N'创建时间';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'contest_base', 'COLUMN', N'create_time';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE TABLE [contest_content] (
        [id] bigint NOT NULL IDENTITY,
        [contest_id] bigint NOT NULL,
        [content] text NULL,
        CONSTRAINT [PK_contest_content] PRIMARY KEY ([id])
    );
    DECLARE @defaultSchema AS sysname;
    SET @defaultSchema = SCHEMA_NAME();
    DECLARE @description AS sql_variant;
    SET @description = N'比赛内容ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'contest_content', 'COLUMN', N'id';
    SET @description = N'比赛ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'contest_content', 'COLUMN', N'contest_id';
    SET @description = N'赛事描述文字';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'contest_content', 'COLUMN', N'content';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE TABLE [contest_problem] (
        [id] bigint NOT NULL IDENTITY,
        [contest_id] bigint NOT NULL,
        [problem_id] bigint NOT NULL,
        [submission_count] bigint NOT NULL,
        [accepted_count] bigint NOT NULL,
        [version] bigint NOT NULL,
        [status] int NOT NULL,
        [properties] nvarchar(2048) NULL,
        CONSTRAINT [PK_contest_problem] PRIMARY KEY ([id])
    );
    DECLARE @defaultSchema AS sysname;
    SET @defaultSchema = SCHEMA_NAME();
    DECLARE @description AS sql_variant;
    SET @description = N'赛题ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'contest_problem', 'COLUMN', N'id';
    SET @description = N'比赛ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'contest_problem', 'COLUMN', N'contest_id';
    SET @description = N'题目ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'contest_problem', 'COLUMN', N'problem_id';
    SET @description = N'提交数';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'contest_problem', 'COLUMN', N'submission_count';
    SET @description = N'AC数';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'contest_problem', 'COLUMN', N'accepted_count';
    SET @description = N'版本';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'contest_problem', 'COLUMN', N'version';
    SET @description = N'状态枚举';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'contest_problem', 'COLUMN', N'status';
    SET @description = N'分数、语言等信息的JSON格式';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'contest_problem', 'COLUMN', N'properties';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE TABLE [object_tag] (
        [id] bigint NOT NULL IDENTITY,
        [target_id] bigint NOT NULL,
        [target_type] int NOT NULL,
        [tag_id] bigint NOT NULL,
        [status] int NOT NULL,
        CONSTRAINT [PK_object_tag] PRIMARY KEY ([id])
    );
    DECLARE @defaultSchema AS sysname;
    SET @defaultSchema = SCHEMA_NAME();
    DECLARE @description AS sql_variant;
    SET @description = N'主体-标签ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'object_tag', 'COLUMN', N'id';
    SET @description = N'主体ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'object_tag', 'COLUMN', N'target_id';
    SET @description = N'目标主体类型';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'object_tag', 'COLUMN', N'target_type';
    SET @description = N'标签ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'object_tag', 'COLUMN', N'tag_id';
    SET @description = N'状态';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'object_tag', 'COLUMN', N'status';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE TABLE [organization] (
        [id] bigint NOT NULL IDENTITY,
        [name] nvarchar(64) NOT NULL,
        [description] nvarchar(max) NULL,
        [owner] bigint NOT NULL,
        [parent_id] bigint NOT NULL,
        [avatar] nvarchar(128) NULL,
        [status] int NOT NULL,
        [update_time] datetime2 NOT NULL DEFAULT (GETDATE()),
        [create_time] datetime2 NOT NULL DEFAULT (GETDATE()),
        CONSTRAINT [PK_organization] PRIMARY KEY ([id])
    );
    DECLARE @defaultSchema AS sysname;
    SET @defaultSchema = SCHEMA_NAME();
    DECLARE @description AS sql_variant;
    SET @description = N'自增ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'organization', 'COLUMN', N'id';
    SET @description = N'组织名';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'organization', 'COLUMN', N'name';
    SET @description = N'描述';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'organization', 'COLUMN', N'description';
    SET @description = N'组织所有人';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'organization', 'COLUMN', N'owner';
    SET @description = N'父组织';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'organization', 'COLUMN', N'parent_id';
    SET @description = N'头像';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'organization', 'COLUMN', N'avatar';
    SET @description = N'状态';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'organization', 'COLUMN', N'status';
    SET @description = N'更新时间';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'organization', 'COLUMN', N'update_time';
    SET @description = N'创建时间';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'organization', 'COLUMN', N'create_time';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE TABLE [problem_base] (
        [id] bigint NOT NULL IDENTITY,
        [title] nvarchar(512) NOT NULL,
        [writer_id] bigint NOT NULL,
        [properties] nvarchar(1024) NULL,
        [show_id] nvarchar(16) NOT NULL,
        [source_id] bigint NOT NULL,
        [submission_count] bigint NULL DEFAULT ('0'),
        [accepted_count] bigint NULL DEFAULT ('0'),
        [type] int NOT NULL,
        [status] int NOT NULL,
        [version] bigint NULL DEFAULT ('0'),
        [update_time] datetime2 NOT NULL DEFAULT (GETDATE()),
        [create_time] datetime2 NOT NULL DEFAULT (GETDATE()),
        CONSTRAINT [PK_problem_base] PRIMARY KEY ([id])
    );
    DECLARE @defaultSchema AS sysname;
    SET @defaultSchema = SCHEMA_NAME();
    DECLARE @description AS sql_variant;
    SET @description = N'题目ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'problem_base', 'COLUMN', N'id';
    SET @description = N'题目标题';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'problem_base', 'COLUMN', N'title';
    SET @description = N'出题人ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'problem_base', 'COLUMN', N'writer_id';
    SET @description = N'针对不同题目类型的描述JSON';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'problem_base', 'COLUMN', N'properties';
    SET @description = N'展示的题号';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'problem_base', 'COLUMN', N'show_id';
    SET @description = N'题目来源';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'problem_base', 'COLUMN', N'source_id';
    SET @description = N'提交数';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'problem_base', 'COLUMN', N'submission_count';
    SET @description = N'通过数';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'problem_base', 'COLUMN', N'accepted_count';
    SET @description = N'题目类型';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'problem_base', 'COLUMN', N'type';
    SET @description = N'题目状态';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'problem_base', 'COLUMN', N'status';
    SET @description = N'版本';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'problem_base', 'COLUMN', N'version';
    SET @description = N'更新时间';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'problem_base', 'COLUMN', N'update_time';
    SET @description = N'创建时间';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'problem_base', 'COLUMN', N'create_time';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE TABLE [problem_content] (
        [id] bigint NOT NULL IDENTITY,
        [problem_id] bigint NOT NULL,
        [content] text NULL,
        CONSTRAINT [PK_problem_content] PRIMARY KEY ([id])
    );
    DECLARE @defaultSchema AS sysname;
    SET @defaultSchema = SCHEMA_NAME();
    DECLARE @description AS sql_variant;
    SET @description = N'题目内容ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'problem_content', 'COLUMN', N'id';
    SET @description = N'题目ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'problem_content', 'COLUMN', N'problem_id';
    SET @description = N'题目具体内容';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'problem_content', 'COLUMN', N'content';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE TABLE [problem_source] (
        [id] bigint NOT NULL IDENTITY,
        [name] nvarchar(128) NOT NULL,
        [url] nvarchar(128) NOT NULL,
        [properties] nvarchar(max) NULL,
        CONSTRAINT [PK_problem_source] PRIMARY KEY ([id])
    );
    DECLARE @defaultSchema AS sysname;
    SET @defaultSchema = SCHEMA_NAME();
    DECLARE @description AS sql_variant;
    SET @description = N'自增ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'problem_source', 'COLUMN', N'id';
    SET @description = N'题目来源名';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'problem_source', 'COLUMN', N'name';
    SET @description = N'题目源主页';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'problem_source', 'COLUMN', N'url';
    SET @description = N'题目show_id组合源链接策略';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'problem_source', 'COLUMN', N'properties';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE TABLE [problemset] (
        [id] bigint NOT NULL IDENTITY,
        [title] nvarchar(64) NOT NULL,
        [creator_id] bigint NOT NULL,
        [description] nvarchar(1024) NULL,
        [status] int NOT NULL,
        [update_time] datetime2 NOT NULL DEFAULT (GETDATE()),
        [create_time] datetime2 NOT NULL DEFAULT (GETDATE()),
        CONSTRAINT [PK_problemset] PRIMARY KEY ([id])
    );
    DECLARE @defaultSchema AS sysname;
    SET @defaultSchema = SCHEMA_NAME();
    DECLARE @description AS sql_variant;
    SET @description = N'题单ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'problemset', 'COLUMN', N'id';
    SET @description = N'题单名称';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'problemset', 'COLUMN', N'title';
    SET @description = N'创建者ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'problemset', 'COLUMN', N'creator_id';
    SET @description = N'简短描述';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'problemset', 'COLUMN', N'description';
    SET @description = N'状态枚举';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'problemset', 'COLUMN', N'status';
    SET @description = N'更新时间';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'problemset', 'COLUMN', N'update_time';
    SET @description = N'创建时间';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'problemset', 'COLUMN', N'create_time';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE TABLE [problemset_problem] (
        [id] bigint NOT NULL IDENTITY,
        [problemset_id] bigint NOT NULL,
        [problem_id] bigint NOT NULL,
        [properties] nvarchar(1024) NULL,
        [status] int NOT NULL,
        CONSTRAINT [PK_problemset_problem] PRIMARY KEY ([id])
    );
    DECLARE @defaultSchema AS sysname;
    SET @defaultSchema = SCHEMA_NAME();
    DECLARE @description AS sql_variant;
    SET @description = N'题单-题目ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'problemset_problem', 'COLUMN', N'id';
    SET @description = N'题单ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'problemset_problem', 'COLUMN', N'problemset_id';
    SET @description = N'题目ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'problemset_problem', 'COLUMN', N'problem_id';
    SET @description = N'JSON';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'problemset_problem', 'COLUMN', N'properties';
    SET @description = N'状态枚举';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'problemset_problem', 'COLUMN', N'status';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE TABLE [register] (
        [id] bigint NOT NULL IDENTITY,
        [contest_id] bigint NOT NULL,
        [registrant_id] bigint NOT NULL,
        [registrant_type] int NOT NULL,
        [team_id] bigint NULL,
        [extra] nvarchar(1024) NULL,
        [status] int NOT NULL,
        [update_time] datetime2 NOT NULL DEFAULT (GETDATE()),
        [create_time] datetime2 NOT NULL DEFAULT (GETDATE()),
        CONSTRAINT [PK_register] PRIMARY KEY ([id])
    );
    DECLARE @defaultSchema AS sysname;
    SET @defaultSchema = SCHEMA_NAME();
    DECLARE @description AS sql_variant;
    SET @description = N'自增ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'register', 'COLUMN', N'id';
    SET @description = N'比赛ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'register', 'COLUMN', N'contest_id';
    SET @description = N'注册人ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'register', 'COLUMN', N'registrant_id';
    SET @description = N'注册人类型';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'register', 'COLUMN', N'registrant_type';
    SET @description = N'队伍ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'register', 'COLUMN', N'team_id';
    SET @description = N'额外信息';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'register', 'COLUMN', N'extra';
    SET @description = N'比赛状态枚举';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'register', 'COLUMN', N'status';
    SET @description = N'更新时间';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'register', 'COLUMN', N'update_time';
    SET @description = N'创建时间';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'register', 'COLUMN', N'create_time';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE TABLE [score] (
        [id] bigint NOT NULL IDENTITY,
        [name] nvarchar(32) NOT NULL,
        [target_type] int NOT NULL,
        [target_id] bigint NOT NULL,
        [agg_id] bigint NOT NULL,
        [value] bigint NOT NULL,
        [status] int NOT NULL,
        [type] int NOT NULL,
        CONSTRAINT [PK_score] PRIMARY KEY ([id])
    );
    DECLARE @defaultSchema AS sysname;
    SET @defaultSchema = SCHEMA_NAME();
    DECLARE @description AS sql_variant;
    SET @description = N'ScoreID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'score', 'COLUMN', N'id';
    SET @description = N'Score名称';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'score', 'COLUMN', N'name';
    SET @description = N'目标主体类型';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'score', 'COLUMN', N'target_type';
    SET @description = N'目标主体ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'score', 'COLUMN', N'target_id';
    SET @description = N'聚合基准';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'score', 'COLUMN', N'agg_id';
    SET @description = N'得分';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'score', 'COLUMN', N'value';
    SET @description = N'状态枚举';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'score', 'COLUMN', N'status';
    SET @description = N'类型';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'score', 'COLUMN', N'type';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE TABLE [solution_base] (
        [id] bigint NOT NULL IDENTITY,
        [writer_id] bigint NOT NULL,
        [contest_id] bigint NULL,
        [problem_id] bigint NULL,
        [status] int NOT NULL,
        [update_time] datetime2 NOT NULL DEFAULT (GETDATE()),
        [create_time] datetime2 NOT NULL DEFAULT (GETDATE()),
        CONSTRAINT [PK_solution_base] PRIMARY KEY ([id])
    );
    DECLARE @defaultSchema AS sysname;
    SET @defaultSchema = SCHEMA_NAME();
    DECLARE @description AS sql_variant;
    SET @description = N'题解ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'solution_base', 'COLUMN', N'id';
    SET @description = N'作者ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'solution_base', 'COLUMN', N'writer_id';
    SET @description = N'关联的比赛';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'solution_base', 'COLUMN', N'contest_id';
    SET @description = N'关联的题目';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'solution_base', 'COLUMN', N'problem_id';
    SET @description = N'状态枚举';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'solution_base', 'COLUMN', N'status';
    SET @description = N'更新时间';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'solution_base', 'COLUMN', N'update_time';
    SET @description = N'创建时间';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'solution_base', 'COLUMN', N'create_time';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE TABLE [solution_content] (
        [id] bigint NOT NULL IDENTITY,
        [solution_id] bigint NOT NULL,
        [content] text NULL,
        CONSTRAINT [PK_solution_content] PRIMARY KEY ([id])
    );
    DECLARE @defaultSchema AS sysname;
    SET @defaultSchema = SCHEMA_NAME();
    DECLARE @description AS sql_variant;
    SET @description = N'题解内容ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'solution_content', 'COLUMN', N'id';
    SET @description = N'题解ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'solution_content', 'COLUMN', N'solution_id';
    SET @description = N'题解内容';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'solution_content', 'COLUMN', N'content';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE TABLE [submission_base] (
        [id] bigint NOT NULL IDENTITY,
        [submit_time] datetime2 NOT NULL DEFAULT (GETDATE()),
        [submitter_id] bigint NOT NULL,
        [submitter_type] int NOT NULL,
        [status] int NOT NULL,
        [type] int NOT NULL,
        [contest_id] bigint NULL,
        [problem_id] bigint NULL,
        [update_time] datetime2 NOT NULL DEFAULT (GETDATE()),
        [create_time] datetime2 NOT NULL DEFAULT (GETDATE()),
        [properties] nvarchar(1024) NULL,
        CONSTRAINT [PK_submission_base] PRIMARY KEY ([id])
    );
    DECLARE @defaultSchema AS sysname;
    SET @defaultSchema = SCHEMA_NAME();
    DECLARE @description AS sql_variant;
    SET @description = N'提交ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'submission_base', 'COLUMN', N'id';
    SET @description = N'提交时间';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'submission_base', 'COLUMN', N'submit_time';
    SET @description = N'提交者ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'submission_base', 'COLUMN', N'submitter_id';
    SET @description = N'提交者类型（团队或个人）';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'submission_base', 'COLUMN', N'submitter_type';
    SET @description = N'提交结果';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'submission_base', 'COLUMN', N'status';
    SET @description = N'提交类型';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'submission_base', 'COLUMN', N'type';
    SET @description = N'关联的比赛';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'submission_base', 'COLUMN', N'contest_id';
    SET @description = N'关联的题目';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'submission_base', 'COLUMN', N'problem_id';
    SET @description = N'更新时间';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'submission_base', 'COLUMN', N'update_time';
    SET @description = N'创建时间';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'submission_base', 'COLUMN', N'create_time';
    SET @description = N'特定配置JSON';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'submission_base', 'COLUMN', N'properties';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE TABLE [submission_content] (
        [id] bigint NOT NULL IDENTITY,
        [submission_id] bigint NOT NULL,
        [content] text NULL,
        CONSTRAINT [PK_submission_content] PRIMARY KEY ([id])
    );
    DECLARE @defaultSchema AS sysname;
    SET @defaultSchema = SCHEMA_NAME();
    DECLARE @description AS sql_variant;
    SET @description = N'提交内容ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'submission_content', 'COLUMN', N'id';
    SET @description = N'提交ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'submission_content', 'COLUMN', N'submission_id';
    SET @description = N'提交内容';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'submission_content', 'COLUMN', N'content';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE TABLE [tag] (
        [id] bigint NOT NULL IDENTITY,
        [name] nvarchar(32) NOT NULL,
        [color] nvarchar(8) NULL,
        [target_type] int NOT NULL,
        [properties] nvarchar(1024) NULL,
        [status] int NOT NULL,
        CONSTRAINT [PK_tag] PRIMARY KEY ([id])
    );
    DECLARE @defaultSchema AS sysname;
    SET @defaultSchema = SCHEMA_NAME();
    DECLARE @description AS sql_variant;
    SET @description = N'标签ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'tag', 'COLUMN', N'id';
    SET @description = N'标签名称';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'tag', 'COLUMN', N'name';
    SET @description = N'标签颜色';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'tag', 'COLUMN', N'color';
    SET @description = N'目标主体类型';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'tag', 'COLUMN', N'target_type';
    SET @description = N'配置项JSON';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'tag', 'COLUMN', N'properties';
    SET @description = N'状态枚举';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'tag', 'COLUMN', N'status';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE TABLE [team] (
        [id] bigint NOT NULL IDENTITY,
        [name] nvarchar(64) NOT NULL,
        [signature] nvarchar(512) NULL,
        [description] nvarchar(512) NULL,
        [leader] bigint NOT NULL,
        [organization_id] bigint NOT NULL,
        [avatar] nvarchar(128) NULL,
        [status] int NOT NULL,
        [update_time] datetime2 NOT NULL DEFAULT (GETDATE()),
        [create_time] datetime2 NOT NULL DEFAULT (GETDATE()),
        CONSTRAINT [PK_team] PRIMARY KEY ([id])
    );
    DECLARE @defaultSchema AS sysname;
    SET @defaultSchema = SCHEMA_NAME();
    DECLARE @description AS sql_variant;
    SET @description = N'自增ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'team', 'COLUMN', N'id';
    SET @description = N'队伍名';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'team', 'COLUMN', N'name';
    SET @description = N'个性签名';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'team', 'COLUMN', N'signature';
    SET @description = N'队伍介绍';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'team', 'COLUMN', N'description';
    SET @description = N'队长';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'team', 'COLUMN', N'leader';
    SET @description = N'所属组织';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'team', 'COLUMN', N'organization_id';
    SET @description = N'头像';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'team', 'COLUMN', N'avatar';
    SET @description = N'状态';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'team', 'COLUMN', N'status';
    SET @description = N'更新时间';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'team', 'COLUMN', N'update_time';
    SET @description = N'创建时间';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'team', 'COLUMN', N'create_time';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE TABLE [team_user] (
        [id] bigint NOT NULL IDENTITY,
        [user_id] bigint NOT NULL,
        [team_id] bigint NOT NULL,
        [user_type] int NOT NULL,
        [status] int NOT NULL,
        [create_time] datetime2 NOT NULL DEFAULT (GETDATE()),
        [update_time] datetime2 NOT NULL DEFAULT (GETDATE()),
        CONSTRAINT [PK_team_user] PRIMARY KEY ([id])
    );
    DECLARE @defaultSchema AS sysname;
    SET @defaultSchema = SCHEMA_NAME();
    DECLARE @description AS sql_variant;
    SET @description = N'自增ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'team_user', 'COLUMN', N'id';
    SET @description = N'用户Id';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'team_user', 'COLUMN', N'user_id';
    SET @description = N'队伍Id';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'team_user', 'COLUMN', N'team_id';
    SET @description = N'用户类型';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'team_user', 'COLUMN', N'user_type';
    SET @description = N'状态';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'team_user', 'COLUMN', N'status';
    SET @description = N'创建时间';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'team_user', 'COLUMN', N'create_time';
    SET @description = N'更新时间';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'team_user', 'COLUMN', N'update_time';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE TABLE [user] (
        [id] bigint NOT NULL IDENTITY,
        [user_id] bigint NOT NULL,
        [username] nvarchar(40) NOT NULL,
        [password] nvarchar(130) NOT NULL,
        [salt] nvarchar(130) NOT NULL,
        [phone] nvarchar(30) NULL,
        [email] nvarchar(64) NULL,
        [signature] nvarchar(512) NULL,
        [organization_id] bigint NOT NULL,
        [nickname] nvarchar(64) NULL,
        [realname] nvarchar(64) NULL,
        [avatar] nvarchar(128) NULL,
        [user_type] int NOT NULL DEFAULT ('3'),
        [extra] nvarchar(max) NULL,
        [allowed_ip] nvarchar(2048) NULL,
        [status] int NOT NULL,
        [update_time] datetime2 NOT NULL DEFAULT (GETDATE()),
        [create_time] datetime2 NOT NULL DEFAULT (GETDATE()),
        CONSTRAINT [PK_user] PRIMARY KEY ([id])
    );
    DECLARE @defaultSchema AS sysname;
    SET @defaultSchema = SCHEMA_NAME();
    DECLARE @description AS sql_variant;
    SET @description = N'自增ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'user', 'COLUMN', N'id';
    SET @description = N'用户ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'user', 'COLUMN', N'user_id';
    SET @description = N'用户名';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'user', 'COLUMN', N'username';
    SET @description = N'密码';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'user', 'COLUMN', N'password';
    SET @description = N'密码加盐';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'user', 'COLUMN', N'salt';
    SET @description = N'电话号码';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'user', 'COLUMN', N'phone';
    SET @description = N'邮箱';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'user', 'COLUMN', N'email';
    SET @description = N'个性签名';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'user', 'COLUMN', N'signature';
    SET @description = N'所属组织';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'user', 'COLUMN', N'organization_id';
    SET @description = N'昵称';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'user', 'COLUMN', N'nickname';
    SET @description = N'真名';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'user', 'COLUMN', N'realname';
    SET @description = N'头像';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'user', 'COLUMN', N'avatar';
    SET @description = N'用户类型1:super admin,2:admin,3:user';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'user', 'COLUMN', N'user_type';
    SET @description = N'额外信息';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'user', 'COLUMN', N'extra';
    SET @description = N'允许访问的IP';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'user', 'COLUMN', N'allowed_ip';
    SET @description = N'状态';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'user', 'COLUMN', N'status';
    SET @description = N'更新时间';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'user', 'COLUMN', N'update_time';
    SET @description = N'创建时间';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'user', 'COLUMN', N'create_time';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE TABLE [user_login] (
        [id] bigint NOT NULL IDENTITY,
        [user_id] bigint NOT NULL,
        [ip] bigint NOT NULL,
        [device_id] nvarchar(128) NOT NULL,
        [platform] int NOT NULL,
        [login_type] int NOT NULL,
        [time] datetime2 NOT NULL DEFAULT (GETDATE()),
        CONSTRAINT [PK_user_login] PRIMARY KEY ([id])
    );
    DECLARE @defaultSchema AS sysname;
    SET @defaultSchema = SCHEMA_NAME();
    DECLARE @description AS sql_variant;
    SET @description = N'自增ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'user_login', 'COLUMN', N'id';
    SET @description = N'用户ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'user_login', 'COLUMN', N'user_id';
    SET @description = N'登录IP';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'user_login', 'COLUMN', N'ip';
    SET @description = N'设备ID';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'user_login', 'COLUMN', N'device_id';
    SET @description = N'平台';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'user_login', 'COLUMN', N'platform';
    SET @description = N'登录类型';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'user_login', 'COLUMN', N'login_type';
    SET @description = N'创建时间';
    EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'user_login', 'COLUMN', N'time';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE INDEX [idx_type_owner] ON [contest_base] ([type], [owner_id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE INDEX [idx_contest] ON [contest_problem] ([contest_id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE INDEX [idx_target_type_id] ON [object_tag] ([target_id], [target_type]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE INDEX [idx_owner] ON [organization] ([owner]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE INDEX [idx_parent] ON [organization] ([parent_id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE INDEX [idx_type_source_show] ON [problem_base] ([type], [source_id], [show_id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE INDEX [idx_type_writer] ON [problem_base] ([type], [writer_id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE INDEX [idx_creator] ON [problemset] ([creator_id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE INDEX [idx_problemset_id] ON [problemset_problem] ([problemset_id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE INDEX [idx_contest1] ON [register] ([contest_id], [status]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE INDEX [idx_registrant] ON [register] ([registrant_id], [status]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE INDEX [idx_type_agg_id_status_value] ON [score] ([type], [agg_id], [status], [value]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE INDEX [idx_contest2] ON [solution_base] ([contest_id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE INDEX [idx_problem] ON [solution_base] ([problem_id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE INDEX [idx_writer] ON [solution_base] ([writer_id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE INDEX [idx_contest_create_time] ON [submission_base] ([contest_id], [create_time]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE INDEX [idx_contest_problem] ON [submission_base] ([contest_id], [problem_id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE INDEX [idx_contest_submitter] ON [submission_base] ([contest_id], [submitter_id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE INDEX [idx_submitter_problem] ON [submission_base] ([submitter_id], [problem_id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE INDEX [idx_team] ON [team_user] ([team_id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE INDEX [idx_user] ON [team_user] ([user_id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE INDEX [idx_email] ON [user] ([email]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE INDEX [idx_phone] ON [user] ([phone]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE INDEX [idx_status] ON [user] ([status]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE INDEX [idx_uid] ON [user] ([user_id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE INDEX [idx_user_name] ON [user] ([username]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE INDEX [idx_user_device_time] ON [user_login] ([user_id], [device_id], [time]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE INDEX [idx_user_platform_time] ON [user_login] ([user_id], [platform], [time]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    CREATE INDEX [idx_user_time] ON [user_login] ([user_id], [time]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221020070449_v1-initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20221020070449_v1-initial', N'6.0.10');
END;
GO

COMMIT;
GO
