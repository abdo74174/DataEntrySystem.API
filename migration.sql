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
CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [Username] nvarchar(max) NOT NULL,
    [PasswordHash] nvarchar(max) NOT NULL,
    [Role] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);

CREATE TABLE [Expenses] (
    [Id] int NOT NULL IDENTITY,
    [Date] datetime2 NOT NULL,
    [ExpenseType] nvarchar(max) NOT NULL,
    [Amount] decimal(18,2) NOT NULL,
    [Notes] nvarchar(max) NULL,
    [CreatedByUserId] int NOT NULL,
    CONSTRAINT [PK_Expenses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Expenses_Users_CreatedByUserId] FOREIGN KEY ([CreatedByUserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Revenues] (
    [Id] int NOT NULL IDENTITY,
    [ClientName] nvarchar(max) NOT NULL,
    [Date] datetime2 NOT NULL,
    [OperationType] nvarchar(max) NOT NULL,
    [ContractPrice] decimal(18,2) NOT NULL,
    [OfferPrice] decimal(18,2) NOT NULL,
    [PaidAmount] decimal(18,2) NOT NULL,
    [CreatedByUserId] int NOT NULL,
    CONSTRAINT [PK_Revenues] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Revenues_Users_CreatedByUserId] FOREIGN KEY ([CreatedByUserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_Expenses_CreatedByUserId] ON [Expenses] ([CreatedByUserId]);

CREATE INDEX [IX_Revenues_CreatedByUserId] ON [Revenues] ([CreatedByUserId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260330205417_init', N'9.0.14');

COMMIT;
GO

