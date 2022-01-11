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

CREATE TABLE [Countries] (
    [ID] int NOT NULL IDENTITY,
    [Name] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_Countries] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [Pilots] (
    [ID] int NOT NULL IDENTITY,
    [LicensNr] nvarchar(max) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [FirstName] nvarchar(max) NOT NULL,
    [NickName] nvarchar(max) NULL,
    [PhotoPath] nvarchar(max) NOT NULL,
    [Gender] nvarchar(max) NOT NULL,
    [Birthdate] datetime2 NOT NULL,
    [Length] int NOT NULL,
    [Weight] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_Pilots] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [Series] (
    [ID] int NOT NULL IDENTITY,
    [Name] nvarchar(450) NOT NULL,
    [Active] bit NOT NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NOT NULL,
    [SortingOrder] int NOT NULL,
    CONSTRAINT [PK_Series] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [Teams] (
    [ID] int NOT NULL IDENTITY,
    [Name] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_Teams] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [Circuits] (
    [ID] int NOT NULL IDENTITY,
    [Name] nvarchar(450) NOT NULL,
    [Length] decimal(18,2) NOT NULL,
    [CountryId] int NOT NULL,
    [State] nvarchar(max) NULL,
    [Street] nvarchar(max) NOT NULL,
    [Number] int NOT NULL,
    CONSTRAINT [PK_Circuits] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Circuits_Countries_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [Countries] ([ID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Seasons] (
    [ID] int NOT NULL IDENTITY,
    [SeriesId] int NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NOT NULL,
    [Active] bit NOT NULL,
    CONSTRAINT [PK_Seasons] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Seasons_Series_SeriesId] FOREIGN KEY ([SeriesId]) REFERENCES [Series] ([ID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Races] (
    [ID] int NOT NULL IDENTITY,
    [SeasonId] int NOT NULL,
    [CircuitId] int NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Races] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Races_Circuits_CircuitId] FOREIGN KEY ([CircuitId]) REFERENCES [Circuits] ([ID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Races_Seasons_SeasonId] FOREIGN KEY ([SeasonId]) REFERENCES [Seasons] ([ID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [TeamParticipants] (
    [ID] int NOT NULL IDENTITY,
    [TeamId] int NOT NULL,
    [RaceId] int NOT NULL,
    [PilotId] int NOT NULL,
    CONSTRAINT [PK_TeamParticipants] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_TeamParticipants_Pilots_PilotId] FOREIGN KEY ([PilotId]) REFERENCES [Pilots] ([ID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_TeamParticipants_Races_RaceId] FOREIGN KEY ([RaceId]) REFERENCES [Races] ([ID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_TeamParticipants_Teams_TeamId] FOREIGN KEY ([TeamId]) REFERENCES [Teams] ([ID]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Circuits_CountryId] ON [Circuits] ([CountryId]);
GO

CREATE UNIQUE INDEX [IX_Circuits_Name] ON [Circuits] ([Name]);
GO

CREATE UNIQUE INDEX [IX_Countries_Name] ON [Countries] ([Name]);
GO

CREATE INDEX [IX_Races_CircuitId] ON [Races] ([CircuitId]);
GO

CREATE UNIQUE INDEX [IX_Races_SeasonId_CircuitId_Name] ON [Races] ([SeasonId], [CircuitId], [Name]);
GO

CREATE INDEX [IX_Races_StartDate] ON [Races] ([StartDate]);
GO

CREATE UNIQUE INDEX [IX_Seasons_SeriesId_Name] ON [Seasons] ([SeriesId], [Name]);
GO

CREATE INDEX [IX_Seasons_StartDate] ON [Seasons] ([StartDate]);
GO

CREATE UNIQUE INDEX [IX_Series_Name] ON [Series] ([Name]);
GO

CREATE INDEX [IX_Series_SortingOrder] ON [Series] ([SortingOrder]);
GO

CREATE UNIQUE INDEX [IX_TeamParticipants_PilotId_RaceId] ON [TeamParticipants] ([PilotId], [RaceId]);
GO

CREATE INDEX [IX_TeamParticipants_RaceId] ON [TeamParticipants] ([RaceId]);
GO

CREATE INDEX [IX_TeamParticipants_TeamId] ON [TeamParticipants] ([TeamId]);
GO

CREATE UNIQUE INDEX [IX_Teams_Name] ON [Teams] ([Name]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220104140930_InitialCreate', N'5.0.12');
GO

COMMIT;
GO

