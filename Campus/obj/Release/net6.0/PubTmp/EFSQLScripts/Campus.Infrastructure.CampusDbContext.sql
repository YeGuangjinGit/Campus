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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    CREATE TABLE [c_Authentications] (
        [AuthenticationId] int NOT NULL IDENTITY,
        [UserId] uniqueidentifier NOT NULL,
        [IdCard] nvarchar(20) NOT NULL,
        [Photo] nvarchar(100) NOT NULL,
        [CreateAt] datetime2 NOT NULL DEFAULT (GetDate()),
        [IsPass] bit NULL,
        [Handle] uniqueidentifier NULL,
        CONSTRAINT [PK_c_Authentications] PRIMARY KEY ([AuthenticationId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    CREATE TABLE [c_Genders] (
        [GenderId] int NOT NULL IDENTITY,
        [GenderValue] nvarchar(40) NOT NULL,
        CONSTRAINT [PK_c_Genders] PRIMARY KEY ([GenderId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    CREATE TABLE [c_Identitys] (
        [IdentityId] int NOT NULL IDENTITY,
        [IdentityValue] nvarchar(40) NOT NULL,
        CONSTRAINT [PK_c_Identitys] PRIMARY KEY ([IdentityId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    CREATE TABLE [c_SpecialColumns] (
        [SpecialColumnId] int NOT NULL IDENTITY,
        [SpecialColumnValue] nvarchar(40) NOT NULL,
        CONSTRAINT [PK_c_SpecialColumns] PRIMARY KEY ([SpecialColumnId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    CREATE TABLE [c_Users] (
        [UserId] uniqueidentifier NOT NULL DEFAULT (NewID()),
        [UserName] nvarchar(40) NOT NULL,
        [Password] nvarchar(40) NOT NULL,
        [HeadPortrait] nvarchar(100) NULL,
        [Nickname] nvarchar(40) NOT NULL,
        [PersonalSignature] nvarchar(100) NOT NULL,
        [Name] nvarchar(100) NULL,
        [GenderId] int NOT NULL DEFAULT 1,
        [Birth] datetime2 NULL DEFAULT (GetDate()),
        [Email] nvarchar(40) NOT NULL,
        [CreateAt] datetime2 NULL DEFAULT (GetDate()),
        [Code] uniqueidentifier NULL DEFAULT (NewID()),
        [State] bit NOT NULL DEFAULT CAST(0 AS bit),
        [IdentityId] int NOT NULL DEFAULT 1,
        [IsDelete] bit NOT NULL DEFAULT CAST(0 AS bit),
        [AuthenticationId] int NULL,
        CONSTRAINT [PK_c_Users] PRIMARY KEY ([UserId]),
        CONSTRAINT [FK_c_Users_c_Authentications_AuthenticationId] FOREIGN KEY ([AuthenticationId]) REFERENCES [c_Authentications] ([AuthenticationId]),
        CONSTRAINT [FK_c_Users_c_Genders_GenderId] FOREIGN KEY ([GenderId]) REFERENCES [c_Genders] ([GenderId]),
        CONSTRAINT [FK_c_Users_c_Identitys_IdentityId] FOREIGN KEY ([IdentityId]) REFERENCES [c_Identitys] ([IdentityId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    CREATE TABLE [c_AccountInformationChange] (
        [AccountChangeId] int NOT NULL IDENTITY,
        [UserId] uniqueidentifier NOT NULL,
        [ChangeReason] nvarchar(100) NOT NULL,
        [Code] uniqueidentifier NULL DEFAULT (NewID()),
        CONSTRAINT [PK_c_AccountInformationChange] PRIMARY KEY ([AccountChangeId]),
        CONSTRAINT [FK_c_AccountInformationChange_c_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [c_Users] ([UserId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    CREATE TABLE [c_Activitys] (
        [ActivityId] int NOT NULL IDENTITY,
        [UserId] uniqueidentifier NOT NULL,
        [ActivityTitle] nvarchar(100) NOT NULL,
        [ActivityContent] nvarchar(1000) NOT NULL,
        [ActivityLocale] nvarchar(100) NOT NULL,
        [ActivityNumber] int NOT NULL DEFAULT 2,
        [ActivityTime] datetime2 NOT NULL DEFAULT (GetDate()),
        [ReleaseTime] datetime2 NOT NULL DEFAULT (GetDate()),
        CONSTRAINT [PK_c_Activitys] PRIMARY KEY ([ActivityId]),
        CONSTRAINT [FK_c_Activitys_c_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [c_Users] ([UserId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    CREATE TABLE [c_Comments] (
        [CommentId] int NOT NULL IDENTITY,
        [UserId] uniqueidentifier NOT NULL,
        [Content] nvarchar(1000) NOT NULL,
        [Parentid] int NOT NULL,
        [ReleaseTime] datetime2 NOT NULL DEFAULT (GetDate()),
        [Like] int NOT NULL DEFAULT 0,
        CONSTRAINT [PK_c_Comments] PRIMARY KEY ([CommentId]),
        CONSTRAINT [FK_c_Comments_c_Comments_Parentid] FOREIGN KEY ([Parentid]) REFERENCES [c_Comments] ([CommentId]),
        CONSTRAINT [FK_c_Comments_c_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [c_Users] ([UserId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    CREATE TABLE [c_Follows] (
        [FollowId] int NOT NULL IDENTITY,
        [UserId] uniqueidentifier NOT NULL,
        [TargetId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_c_Follows] PRIMARY KEY ([FollowId]),
        CONSTRAINT [FK_c_Follows_c_Users_TargetId] FOREIGN KEY ([TargetId]) REFERENCES [c_Users] ([UserId]),
        CONSTRAINT [FK_c_Follows_c_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [c_Users] ([UserId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    CREATE TABLE [c_Opinions] (
        [OpinionId] int NOT NULL IDENTITY,
        [UserId] uniqueidentifier NOT NULL,
        [OpinionTitle] nvarchar(100) NOT NULL,
        [OpinionContent] nvarchar(1000) NOT NULL,
        [ReleaseTime] datetime2 NOT NULL DEFAULT (GetDate()),
        [HandleId] uniqueidentifier NOT NULL,
        [Result] nvarchar(1000) NULL,
        CONSTRAINT [PK_c_Opinions] PRIMARY KEY ([OpinionId]),
        CONSTRAINT [FK_c_Opinions_c_Users_HandleId] FOREIGN KEY ([HandleId]) REFERENCES [c_Users] ([UserId]) ON DELETE CASCADE,
        CONSTRAINT [FK_c_Opinions_c_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [c_Users] ([UserId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    CREATE TABLE [c_Works] (
        [WorksId] int NOT NULL IDENTITY,
        [UserId] uniqueidentifier NOT NULL,
        [Title] nvarchar(100) NOT NULL,
        [Content] nvarchar(1000) NOT NULL,
        [ReleaseTime] datetime2 NOT NULL DEFAULT (GetDate()),
        [SpecialColumnId] int NOT NULL DEFAULT 1,
        CONSTRAINT [PK_c_Works] PRIMARY KEY ([WorksId]),
        CONSTRAINT [FK_c_Works_c_SpecialColumns_SpecialColumnId] FOREIGN KEY ([SpecialColumnId]) REFERENCES [c_SpecialColumns] ([SpecialColumnId]),
        CONSTRAINT [FK_c_Works_c_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [c_Users] ([UserId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    CREATE TABLE [c_Enrolls] (
        [EnrolId] int NOT NULL IDENTITY,
        [ActivityId] int NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        [Participate] bit NOT NULL DEFAULT CAST(0 AS bit),
        CONSTRAINT [PK_c_Enrolls] PRIMARY KEY ([EnrolId]),
        CONSTRAINT [FK_c_Enrolls_c_Activitys_ActivityId] FOREIGN KEY ([ActivityId]) REFERENCES [c_Activitys] ([ActivityId]),
        CONSTRAINT [FK_c_Enrolls_c_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [c_Users] ([UserId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    CREATE TABLE [c_Collections] (
        [CollectionId] int NOT NULL IDENTITY,
        [UserId] uniqueidentifier NOT NULL,
        [WorksId] int NOT NULL,
        CONSTRAINT [PK_c_Collections] PRIMARY KEY ([CollectionId]),
        CONSTRAINT [FK_c_Collections_c_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [c_Users] ([UserId]),
        CONSTRAINT [FK_c_Collections_c_Works_WorksId] FOREIGN KEY ([WorksId]) REFERENCES [c_Works] ([WorksId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    CREATE TABLE [c_Fabulous] (
        [FabulousId] int NOT NULL IDENTITY,
        [UserId] uniqueidentifier NOT NULL,
        [WorksId] int NOT NULL,
        CONSTRAINT [PK_c_Fabulous] PRIMARY KEY ([FabulousId]),
        CONSTRAINT [FK_c_Fabulous_c_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [c_Users] ([UserId]),
        CONSTRAINT [FK_c_Fabulous_c_Works_WorksId] FOREIGN KEY ([WorksId]) REFERENCES [c_Works] ([WorksId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    CREATE TABLE [c_Pictures] (
        [PictureId] int NOT NULL IDENTITY,
        [WorksId] int NOT NULL,
        [Url] nvarchar(100) NOT NULL,
        CONSTRAINT [PK_c_Pictures] PRIMARY KEY ([PictureId]),
        CONSTRAINT [FK_c_Pictures_c_Works_WorksId] FOREIGN KEY ([WorksId]) REFERENCES [c_Works] ([WorksId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'GenderId', N'GenderValue') AND [object_id] = OBJECT_ID(N'[c_Genders]'))
        SET IDENTITY_INSERT [c_Genders] ON;
    EXEC(N'INSERT INTO [c_Genders] ([GenderId], [GenderValue])
    VALUES (1, N''保密''),
    (2, N''男''),
    (3, N''女'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'GenderId', N'GenderValue') AND [object_id] = OBJECT_ID(N'[c_Genders]'))
        SET IDENTITY_INSERT [c_Genders] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'IdentityId', N'IdentityValue') AND [object_id] = OBJECT_ID(N'[c_Identitys]'))
        SET IDENTITY_INSERT [c_Identitys] ON;
    EXEC(N'INSERT INTO [c_Identitys] ([IdentityId], [IdentityValue])
    VALUES (1, N''无''),
    (2, N''学生''),
    (3, N''老师''),
    (4, N''管理员'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'IdentityId', N'IdentityValue') AND [object_id] = OBJECT_ID(N'[c_Identitys]'))
        SET IDENTITY_INSERT [c_Identitys] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'SpecialColumnId', N'SpecialColumnValue') AND [object_id] = OBJECT_ID(N'[c_SpecialColumns]'))
        SET IDENTITY_INSERT [c_SpecialColumns] ON;
    EXEC(N'INSERT INTO [c_SpecialColumns] ([SpecialColumnId], [SpecialColumnValue])
    VALUES (1, N''校园天地''),
    (2, N''学生论坛''),
    (3, N''社团活动''),
    (4, N''校园热卖''),
    (5, N''校园防疫''),
    (6, N''校园活动'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'SpecialColumnId', N'SpecialColumnValue') AND [object_id] = OBJECT_ID(N'[c_SpecialColumns]'))
        SET IDENTITY_INSERT [c_SpecialColumns] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    CREATE INDEX [IX_c_AccountInformationChange_UserId] ON [c_AccountInformationChange] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    CREATE INDEX [IX_c_Activitys_UserId] ON [c_Activitys] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    CREATE INDEX [IX_c_Collections_UserId] ON [c_Collections] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    CREATE INDEX [IX_c_Collections_WorksId] ON [c_Collections] ([WorksId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    CREATE INDEX [IX_c_Comments_Parentid] ON [c_Comments] ([Parentid]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    CREATE INDEX [IX_c_Comments_UserId] ON [c_Comments] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    CREATE INDEX [IX_c_Enrolls_ActivityId] ON [c_Enrolls] ([ActivityId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    CREATE INDEX [IX_c_Enrolls_UserId] ON [c_Enrolls] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    CREATE INDEX [IX_c_Fabulous_UserId] ON [c_Fabulous] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    CREATE INDEX [IX_c_Fabulous_WorksId] ON [c_Fabulous] ([WorksId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    CREATE INDEX [IX_c_Follows_TargetId] ON [c_Follows] ([TargetId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    CREATE INDEX [IX_c_Follows_UserId] ON [c_Follows] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    CREATE INDEX [IX_c_Opinions_HandleId] ON [c_Opinions] ([HandleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    CREATE INDEX [IX_c_Opinions_UserId] ON [c_Opinions] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    CREATE INDEX [IX_c_Pictures_WorksId] ON [c_Pictures] ([WorksId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_c_Users_AuthenticationId] ON [c_Users] ([AuthenticationId]) WHERE [AuthenticationId] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    CREATE INDEX [IX_c_Users_GenderId] ON [c_Users] ([GenderId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    CREATE INDEX [IX_c_Users_IdentityId] ON [c_Users] ([IdentityId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    CREATE INDEX [IX_c_Works_SpecialColumnId] ON [c_Works] ([SpecialColumnId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    CREATE INDEX [IX_c_Works_UserId] ON [c_Works] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082239_UpdateTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220512082239_UpdateTable', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082541_UpdateTable2')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[c_Users]') AND [c].[name] = N'PersonalSignature');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [c_Users] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [c_Users] ALTER COLUMN [PersonalSignature] nvarchar(100) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082541_UpdateTable2')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[c_Users]') AND [c].[name] = N'Nickname');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [c_Users] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [c_Users] ALTER COLUMN [Nickname] nvarchar(40) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512082541_UpdateTable2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220512082541_UpdateTable2', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512115438_updateChangeTable')
BEGIN
    ALTER TABLE [c_AccountInformationChange] ADD [CreateAt] datetime2 NOT NULL DEFAULT (GetDate());
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512115438_updateChangeTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220512115438_updateChangeTable', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513055608_AddingIdentity')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513055608_AddingIdentity')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513055608_AddingIdentity')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513055608_AddingIdentity')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513055608_AddingIdentity')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513055608_AddingIdentity')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513055608_AddingIdentity')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513055608_AddingIdentity')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513055608_AddingIdentity')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513055608_AddingIdentity')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513055608_AddingIdentity')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513055608_AddingIdentity')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513055608_AddingIdentity')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513055608_AddingIdentity')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513055608_AddingIdentity')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220513055608_AddingIdentity', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    ALTER TABLE [c_AccountInformationChange] DROP CONSTRAINT [FK_c_AccountInformationChange_c_Users_UserId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    ALTER TABLE [c_Activitys] DROP CONSTRAINT [FK_c_Activitys_c_Users_UserId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    ALTER TABLE [c_Collections] DROP CONSTRAINT [FK_c_Collections_c_Users_UserId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    ALTER TABLE [c_Comments] DROP CONSTRAINT [FK_c_Comments_c_Users_UserId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    ALTER TABLE [c_Enrolls] DROP CONSTRAINT [FK_c_Enrolls_c_Users_UserId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    ALTER TABLE [c_Fabulous] DROP CONSTRAINT [FK_c_Fabulous_c_Users_UserId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    ALTER TABLE [c_Follows] DROP CONSTRAINT [FK_c_Follows_c_Users_TargetId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    ALTER TABLE [c_Follows] DROP CONSTRAINT [FK_c_Follows_c_Users_UserId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    ALTER TABLE [c_Opinions] DROP CONSTRAINT [FK_c_Opinions_c_Users_HandleId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    ALTER TABLE [c_Opinions] DROP CONSTRAINT [FK_c_Opinions_c_Users_UserId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    ALTER TABLE [c_Works] DROP CONSTRAINT [FK_c_Works_c_Users_UserId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    DROP TABLE [c_Users];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    DROP INDEX [IX_c_Works_UserId] ON [c_Works];
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[c_Works]') AND [c].[name] = N'UserId');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [c_Works] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [c_Works] ALTER COLUMN [UserId] nvarchar(450) NOT NULL;
    CREATE INDEX [IX_c_Works_UserId] ON [c_Works] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    DROP INDEX [IX_c_Opinions_UserId] ON [c_Opinions];
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[c_Opinions]') AND [c].[name] = N'UserId');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [c_Opinions] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [c_Opinions] ALTER COLUMN [UserId] nvarchar(450) NOT NULL;
    CREATE INDEX [IX_c_Opinions_UserId] ON [c_Opinions] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    DROP INDEX [IX_c_Opinions_HandleId] ON [c_Opinions];
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[c_Opinions]') AND [c].[name] = N'HandleId');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [c_Opinions] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [c_Opinions] ALTER COLUMN [HandleId] nvarchar(450) NOT NULL;
    CREATE INDEX [IX_c_Opinions_HandleId] ON [c_Opinions] ([HandleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    DROP INDEX [IX_c_Follows_UserId] ON [c_Follows];
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[c_Follows]') AND [c].[name] = N'UserId');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [c_Follows] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [c_Follows] ALTER COLUMN [UserId] nvarchar(450) NOT NULL;
    CREATE INDEX [IX_c_Follows_UserId] ON [c_Follows] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    DROP INDEX [IX_c_Follows_TargetId] ON [c_Follows];
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[c_Follows]') AND [c].[name] = N'TargetId');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [c_Follows] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [c_Follows] ALTER COLUMN [TargetId] nvarchar(450) NOT NULL;
    CREATE INDEX [IX_c_Follows_TargetId] ON [c_Follows] ([TargetId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    DROP INDEX [IX_c_Fabulous_UserId] ON [c_Fabulous];
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[c_Fabulous]') AND [c].[name] = N'UserId');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [c_Fabulous] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [c_Fabulous] ALTER COLUMN [UserId] nvarchar(450) NOT NULL;
    CREATE INDEX [IX_c_Fabulous_UserId] ON [c_Fabulous] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    DROP INDEX [IX_c_Enrolls_UserId] ON [c_Enrolls];
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[c_Enrolls]') AND [c].[name] = N'UserId');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [c_Enrolls] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [c_Enrolls] ALTER COLUMN [UserId] nvarchar(450) NOT NULL;
    CREATE INDEX [IX_c_Enrolls_UserId] ON [c_Enrolls] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    DROP INDEX [IX_c_Comments_UserId] ON [c_Comments];
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[c_Comments]') AND [c].[name] = N'UserId');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [c_Comments] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [c_Comments] ALTER COLUMN [UserId] nvarchar(450) NOT NULL;
    CREATE INDEX [IX_c_Comments_UserId] ON [c_Comments] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    DROP INDEX [IX_c_Collections_UserId] ON [c_Collections];
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[c_Collections]') AND [c].[name] = N'UserId');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [c_Collections] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [c_Collections] ALTER COLUMN [UserId] nvarchar(450) NOT NULL;
    CREATE INDEX [IX_c_Collections_UserId] ON [c_Collections] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[c_Authentications]') AND [c].[name] = N'UserId');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [c_Authentications] DROP CONSTRAINT [' + @var11 + '];');
    ALTER TABLE [c_Authentications] ALTER COLUMN [UserId] nvarchar(max) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    DECLARE @var12 sysname;
    SELECT @var12 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[c_Authentications]') AND [c].[name] = N'Handle');
    IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [c_Authentications] DROP CONSTRAINT [' + @var12 + '];');
    ALTER TABLE [c_Authentications] ALTER COLUMN [Handle] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    DROP INDEX [IX_c_Activitys_UserId] ON [c_Activitys];
    DECLARE @var13 sysname;
    SELECT @var13 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[c_Activitys]') AND [c].[name] = N'UserId');
    IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [c_Activitys] DROP CONSTRAINT [' + @var13 + '];');
    ALTER TABLE [c_Activitys] ALTER COLUMN [UserId] nvarchar(450) NOT NULL;
    CREATE INDEX [IX_c_Activitys_UserId] ON [c_Activitys] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    DROP INDEX [IX_c_AccountInformationChange_UserId] ON [c_AccountInformationChange];
    DECLARE @var14 sysname;
    SELECT @var14 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[c_AccountInformationChange]') AND [c].[name] = N'UserId');
    IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [c_AccountInformationChange] DROP CONSTRAINT [' + @var14 + '];');
    ALTER TABLE [c_AccountInformationChange] ALTER COLUMN [UserId] nvarchar(450) NOT NULL;
    CREATE INDEX [IX_c_AccountInformationChange_UserId] ON [c_AccountInformationChange] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    DECLARE @var15 sysname;
    SELECT @var15 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[c_AccountInformationChange]') AND [c].[name] = N'Code');
    IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [c_AccountInformationChange] DROP CONSTRAINT [' + @var15 + '];');
    ALTER TABLE [c_AccountInformationChange] ALTER COLUMN [Code] nvarchar(max) NULL;
    ALTER TABLE [c_AccountInformationChange] ADD DEFAULT (NewID()) FOR [Code];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [AuthenticationId] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Birth] datetime2 NULL DEFAULT (GetDate());
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [CreateAt] datetime2 NULL DEFAULT (GetDate());
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [GenderId] int NOT NULL DEFAULT 1;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [HeadPortrait] nvarchar(100) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [IdentityId] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Nickname] nvarchar(40) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [PersonalSignature] nvarchar(100) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_AspNetUsers_AuthenticationId] ON [AspNetUsers] ([AuthenticationId]) WHERE [AuthenticationId] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    CREATE INDEX [IX_AspNetUsers_GenderId] ON [AspNetUsers] ([GenderId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    CREATE INDEX [IX_AspNetUsers_IdentityId] ON [AspNetUsers] ([IdentityId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    ALTER TABLE [AspNetUsers] ADD CONSTRAINT [FK_AspNetUsers_c_Authentications_AuthenticationId] FOREIGN KEY ([AuthenticationId]) REFERENCES [c_Authentications] ([AuthenticationId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    ALTER TABLE [AspNetUsers] ADD CONSTRAINT [FK_AspNetUsers_c_Genders_GenderId] FOREIGN KEY ([GenderId]) REFERENCES [c_Genders] ([GenderId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    ALTER TABLE [AspNetUsers] ADD CONSTRAINT [FK_AspNetUsers_c_Identitys_IdentityId] FOREIGN KEY ([IdentityId]) REFERENCES [c_Identitys] ([IdentityId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    ALTER TABLE [c_AccountInformationChange] ADD CONSTRAINT [FK_c_AccountInformationChange_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    ALTER TABLE [c_Activitys] ADD CONSTRAINT [FK_c_Activitys_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    ALTER TABLE [c_Collections] ADD CONSTRAINT [FK_c_Collections_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    ALTER TABLE [c_Comments] ADD CONSTRAINT [FK_c_Comments_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    ALTER TABLE [c_Enrolls] ADD CONSTRAINT [FK_c_Enrolls_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    ALTER TABLE [c_Fabulous] ADD CONSTRAINT [FK_c_Fabulous_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    ALTER TABLE [c_Follows] ADD CONSTRAINT [FK_c_Follows_AspNetUsers_TargetId] FOREIGN KEY ([TargetId]) REFERENCES [AspNetUsers] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    ALTER TABLE [c_Follows] ADD CONSTRAINT [FK_c_Follows_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    ALTER TABLE [c_Opinions] ADD CONSTRAINT [FK_c_Opinions_AspNetUsers_HandleId] FOREIGN KEY ([HandleId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    ALTER TABLE [c_Opinions] ADD CONSTRAINT [FK_c_Opinions_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    ALTER TABLE [c_Works] ADD CONSTRAINT [FK_c_Works_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220513120848_Extend_IdentityUser')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220513120848_Extend_IdentityUser', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220514011733_HeadImgDefaultValue')
BEGIN
    DECLARE @var16 sysname;
    SELECT @var16 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'HeadPortrait');
    IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var16 + '];');
    ALTER TABLE [AspNetUsers] ADD DEFAULT N'~/img/head_img.png' FOR [HeadPortrait];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220514011733_HeadImgDefaultValue')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220514011733_HeadImgDefaultValue', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220514013421_HeadImgDefaultValue2')
BEGIN
    DECLARE @var17 sysname;
    SELECT @var17 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'HeadPortrait');
    IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var17 + '];');
    ALTER TABLE [AspNetUsers] ADD DEFAULT N'/img/head_img.png' FOR [HeadPortrait];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220514013421_HeadImgDefaultValue2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220514013421_HeadImgDefaultValue2', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220515124304_DefaultNickName')
BEGIN
    ALTER TABLE [AspNetUsers] DROP CONSTRAINT [FK_AspNetUsers_c_Identitys_IdentityId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220515124304_DefaultNickName')
BEGIN
    ALTER TABLE [c_Identitys] DROP CONSTRAINT [PK_c_Identitys];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220515124304_DefaultNickName')
BEGIN
    EXEC sp_rename N'[c_Identitys]', N'Identity';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220515124304_DefaultNickName')
BEGIN
    DECLARE @var18 sysname;
    SELECT @var18 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Nickname');
    IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var18 + '];');
    ALTER TABLE [AspNetUsers] ADD DEFAULT N'user_31BE16' FOR [Nickname];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220515124304_DefaultNickName')
BEGIN
    DECLARE @var19 sysname;
    SELECT @var19 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Identity]') AND [c].[name] = N'IdentityValue');
    IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [Identity] DROP CONSTRAINT [' + @var19 + '];');
    ALTER TABLE [Identity] ALTER COLUMN [IdentityValue] nvarchar(max) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220515124304_DefaultNickName')
BEGIN
    ALTER TABLE [Identity] ADD CONSTRAINT [PK_Identity] PRIMARY KEY ([IdentityId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220515124304_DefaultNickName')
BEGIN
    ALTER TABLE [AspNetUsers] ADD CONSTRAINT [FK_AspNetUsers_Identity_IdentityId] FOREIGN KEY ([IdentityId]) REFERENCES [Identity] ([IdentityId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220515124304_DefaultNickName')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220515124304_DefaultNickName', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220515125914_DefaultNickName2')
BEGIN
    DECLARE @var20 sysname;
    SELECT @var20 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Nickname');
    IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var20 + '];');
    ALTER TABLE [AspNetUsers] ADD DEFAULT N'''user_'' + Left(newId(),8)' FOR [Nickname];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220515125914_DefaultNickName2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220515125914_DefaultNickName2', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220515131623_DefaultNickName3')
BEGIN
    DECLARE @var21 sysname;
    SELECT @var21 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Nickname');
    IF @var21 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var21 + '];');
    ALTER TABLE [AspNetUsers] ADD DEFAULT N'user_ ''+ Left(newId(),8)+ ''' FOR [Nickname];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220515131623_DefaultNickName3')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220515131623_DefaultNickName3', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220515131844_DefaultNickName4')
BEGIN
    DECLARE @var22 sysname;
    SELECT @var22 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Nickname');
    IF @var22 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var22 + '];');
    ALTER TABLE [AspNetUsers] ADD DEFAULT N'user_Left(newId(),8)' FOR [Nickname];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220515131844_DefaultNickName4')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220515131844_DefaultNickName4', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220515132124_DefaultNickName5')
BEGIN
    ALTER TABLE [AspNetUsers] DROP CONSTRAINT [FK_AspNetUsers_Identity_IdentityId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220515132124_DefaultNickName5')
BEGIN
    DROP TABLE [Identity];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220515132124_DefaultNickName5')
BEGIN
    DROP INDEX [IX_AspNetUsers_IdentityId] ON [AspNetUsers];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220515132124_DefaultNickName5')
BEGIN
    DECLARE @var23 sysname;
    SELECT @var23 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'IdentityId');
    IF @var23 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var23 + '];');
    ALTER TABLE [AspNetUsers] DROP COLUMN [IdentityId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220515132124_DefaultNickName5')
BEGIN
    DECLARE @var24 sysname;
    SELECT @var24 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Nickname');
    IF @var24 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var24 + '];');
    ALTER TABLE [AspNetUsers] ADD DEFAULT N'Left(newId(),8)' FOR [Nickname];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220515132124_DefaultNickName5')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220515132124_DefaultNickName5', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220515132925_DefaultNickName6')
BEGIN
    DECLARE @var25 sysname;
    SELECT @var25 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Nickname');
    IF @var25 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var25 + '];');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220515132925_DefaultNickName6')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220515132925_DefaultNickName6', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220523012728_UpdateWorksContentMaxLenth')
BEGIN
    DECLARE @var26 sysname;
    SELECT @var26 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[c_Works]') AND [c].[name] = N'Content');
    IF @var26 IS NOT NULL EXEC(N'ALTER TABLE [c_Works] DROP CONSTRAINT [' + @var26 + '];');
    ALTER TABLE [c_Works] ALTER COLUMN [Content] nvarchar(max) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220523012728_UpdateWorksContentMaxLenth')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220523012728_UpdateWorksContentMaxLenth', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220523013219_UpdateWorksContentMaxLenth2')
BEGIN
    DECLARE @var27 sysname;
    SELECT @var27 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[c_Works]') AND [c].[name] = N'Title');
    IF @var27 IS NOT NULL EXEC(N'ALTER TABLE [c_Works] DROP CONSTRAINT [' + @var27 + '];');
    ALTER TABLE [c_Works] ALTER COLUMN [Title] nvarchar(max) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220523013219_UpdateWorksContentMaxLenth2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220523013219_UpdateWorksContentMaxLenth2', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220523013331_UpdateWorksContentMaxLenth3')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220523013331_UpdateWorksContentMaxLenth3', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220528043233_CreateWorksBrowse')
BEGIN
    ALTER TABLE [c_Works] ADD [Browse] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220528043233_CreateWorksBrowse')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220528043233_CreateWorksBrowse', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220529094511_AddDisplayContent')
BEGIN
    ALTER TABLE [c_Works] ADD [DisplayContent] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220529094511_AddDisplayContent')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220529094511_AddDisplayContent', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220529124307_ParentidIsNull')
BEGIN
    DECLARE @var28 sysname;
    SELECT @var28 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[c_Comments]') AND [c].[name] = N'Parentid');
    IF @var28 IS NOT NULL EXEC(N'ALTER TABLE [c_Comments] DROP CONSTRAINT [' + @var28 + '];');
    ALTER TABLE [c_Comments] ALTER COLUMN [Parentid] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220529124307_ParentidIsNull')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220529124307_ParentidIsNull', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220529132317_CommentAddWorksId')
BEGIN
    ALTER TABLE [c_Comments] ADD [WorksId] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220529132317_CommentAddWorksId')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220529132317_CommentAddWorksId', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220602031413_AddWorksCommentAndRemovePictures')
BEGIN
    DROP TABLE [c_Pictures];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220602031413_AddWorksCommentAndRemovePictures')
BEGIN
    DECLARE @var29 sysname;
    SELECT @var29 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[c_Comments]') AND [c].[name] = N'WorksId');
    IF @var29 IS NOT NULL EXEC(N'ALTER TABLE [c_Comments] DROP CONSTRAINT [' + @var29 + '];');
    ALTER TABLE [c_Comments] ALTER COLUMN [WorksId] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220602031413_AddWorksCommentAndRemovePictures')
BEGIN
    CREATE INDEX [IX_c_Comments_WorksId] ON [c_Comments] ([WorksId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220602031413_AddWorksCommentAndRemovePictures')
BEGIN
    ALTER TABLE [c_Comments] ADD CONSTRAINT [FK_c_Comments_c_Works_WorksId] FOREIGN KEY ([WorksId]) REFERENCES [c_Works] ([WorksId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220602031413_AddWorksCommentAndRemovePictures')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220602031413_AddWorksCommentAndRemovePictures', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220602031945_CommentWorksIdNullable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220602031945_CommentWorksIdNullable', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220602032832_CommentWorksIdNullable2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220602032832_CommentWorksIdNullable2', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220602032922_CommentWorksIdNullable3')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220602032922_CommentWorksIdNullable3', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220607042751_HandleIdIsNull')
BEGIN
    DECLARE @var30 sysname;
    SELECT @var30 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[c_Opinions]') AND [c].[name] = N'HandleId');
    IF @var30 IS NOT NULL EXEC(N'ALTER TABLE [c_Opinions] DROP CONSTRAINT [' + @var30 + '];');
    ALTER TABLE [c_Opinions] ALTER COLUMN [HandleId] nvarchar(450) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220607042751_HandleIdIsNull')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220607042751_HandleIdIsNull', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220607090217_WorksIsDelete')
BEGIN
    ALTER TABLE [c_Works] ADD [IsDelete] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220607090217_WorksIsDelete')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220607090217_WorksIsDelete', N'6.0.5');
END;
GO

COMMIT;
GO

