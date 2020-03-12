IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Apps] (
    [Id] int NOT NULL IDENTITY,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [RecordStatus] int NOT NULL,
    [Name] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    CONSTRAINT [PK_Apps] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200304083730_InitialMigration', N'3.1.2');

GO

CREATE TABLE [ReviewTypes] (
    [Id] bigint NOT NULL IDENTITY,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [RecordStatus] int NOT NULL,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_ReviewTypes] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200305084104_ReviewTypeTableCreated', N'3.1.2');

GO

CREATE TABLE [AppClients] (
    [Id] bigint NOT NULL IDENTITY,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [RecordStatus] int NOT NULL,
    [AppId] int NOT NULL,
    [ClientId] int NOT NULL,
    [ClientSecret] nvarchar(max) NULL,
    CONSTRAINT [PK_AppClients] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200305155258_AddAppClientsTable', N'3.1.2');

GO

CREATE TABLE [Reviews] (
    [Id] uniqueidentifier NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [RecordStatus] int NOT NULL,
    [AppClientId] bigint NOT NULL,
    [Comment] nvarchar(max) NULL,
    [Rating] int NOT NULL,
    [AppFeature] nvarchar(max) NULL,
    [UserId] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    [ReviewTypeId] bigint NOT NULL,
    [ParentId] int NOT NULL,
    CONSTRAINT [PK_Reviews] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [ReviewVoteTypes] (
    [Id] int NOT NULL IDENTITY,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [RecordStatus] int NOT NULL,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_ReviewVoteTypes] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200305182403_AddedReviewVoteTypesTable', N'3.1.2');

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200306102846_ReviewTableAdded', N'3.1.2');

GO

CREATE TABLE [ReviewVotes] (
    [Id] uniqueidentifier NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [RecordStatus] int NOT NULL,
    [ReviewId] uniqueidentifier NOT NULL,
    [UserId] bigint NOT NULL,
    [ReviewVoteTypeId] int NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_ReviewVotes] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200306113156_AddReviewVotesTable', N'3.1.2');

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ReviewVotes]') AND [c].[name] = N'UserId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [ReviewVotes] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [ReviewVotes] ALTER COLUMN [UserId] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200306122103_UpdateReviewVotesTable', N'3.1.2');

GO

CREATE OR ALTER VIEW [dbo].[reviewmodel]
 AS
 SELECT x.*,
    (SELECT Distinct COUNT(*) From ReviewVotes c  WHERE x.Id = c.ReviewId  AND ReviewVoteTypeId = 1 AND IsActive = 1) as ReviewUpVotes,
	(SELECT Distinct COUNT(*) From ReviewVotes c WHERE x.Id = c.ReviewId AND ReviewVoteTypeId = 2 AND IsActive = 1) as ReviewDownVotes  
   FROM Reviews x
  WHERE x.RecordStatus <> 3 AND x.RecordStatus <> 4

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200311152920_add_review_model_view', N'3.1.2');

GO

CREATE OR ALTER VIEW [dbo].[appclientmodel]
AS
SELECT Id, CreatedAt, UpdatedAt, RecordStatus, AppId, ClientId, ClientSecret
FROM     dbo.AppClients
WHERE  (RecordStatus <> 3) AND (RecordStatus <> 4)

GO

CREATE OR ALTER VIEW [dbo].[appmodel]
AS
SELECT Id, CreatedAt, UpdatedAt, RecordStatus, Name, Description
FROM     dbo.Apps
WHERE  (RecordStatus <> 3) AND (RecordStatus <> 4)

GO

CREATE OR ALTER VIEW [dbo].[clientmodel]
AS
SELECT Id, CreatedAt, UpdatedAt, RecordStatus, Name
FROM     dbo.Clients
WHERE  (RecordStatus <> 3) AND (RecordStatus <> 4)

GO

CREATE OR ALTER VIEW [dbo].[reviewtypemodel]
AS
SELECT Id, CreatedAt, UpdatedAt, RecordStatus, Name
FROM     dbo.ReviewTypes
WHERE  (RecordStatus <> 3) AND (RecordStatus <> 4)

GO

CREATE OR ALTER VIEW [dbo].[reviewvotemodel]
AS
SELECT Id, CreatedAt, UpdatedAt, RecordStatus, ReviewId, UserId, ReviewVoteTypeId, IsActive
FROM     dbo.ReviewVotes
WHERE  (RecordStatus <> 3) AND (RecordStatus <> 4)

GO

CREATE OR ALTER VIEW [dbo].[reviewvotetypemodel]
AS
SELECT Id, CreatedAt, UpdatedAt, RecordStatus, Name
FROM     dbo.ReviewVoteTypes
WHERE  (RecordStatus <> 3) AND (RecordStatus <> 4)

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200311153633_add_table_entity_views', N'3.1.2');

GO

