-- GymMasterAppDemo: schema reconstructed from the live demo database system catalogs
-- Run files in numbered order using SQL Server tooling (GO batch separator).
SET NOCOUNT ON;
USE [GymMasterDBDemo];
GO
IF DB_NAME() COLLATE Latin1_General_100_BIN2 <> N'GymMasterDBDemo'
    THROW 51000, 'Demo safety check failed. Expected GymMasterDBDemo.', 1;
SET XACT_ABORT ON;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET ARITHABORT ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET NUMERIC_ROUNDABORT OFF;
BEGIN TRY
    BEGIN TRANSACTION;
IF OBJECT_ID(N'dbo.OccupationList', N'U') IS NOT NULL OR
   OBJECT_ID(N'dbo.ServicesList', N'U') IS NOT NULL OR
   OBJECT_ID(N'dbo.MembershipTypeList', N'U') IS NOT NULL OR
   OBJECT_ID(N'dbo.PayMethodList', N'U') IS NOT NULL OR
   OBJECT_ID(N'dbo.StatusList', N'U') IS NOT NULL OR
   OBJECT_ID(N'dbo.WorkoutGoals', N'U') IS NOT NULL OR
   OBJECT_ID(N'dbo.Exercises', N'U') IS NOT NULL OR
   OBJECT_ID(N'dbo.HealthRecord', N'U') IS NOT NULL OR
   OBJECT_ID(N'dbo.Customers', N'U') IS NOT NULL OR
   OBJECT_ID(N'dbo.Membership', N'U') IS NOT NULL OR
   OBJECT_ID(N'dbo.Payments', N'U') IS NOT NULL OR
   OBJECT_ID(N'dbo.ProgramsWorkout', N'U') IS NOT NULL OR
   OBJECT_ID(N'dbo.ProgramsWorkoutDetails', N'U') IS NOT NULL
    THROW 51001, 'Schema already exists or is partially installed; no tables were overwritten.', 1;

CREATE TABLE [dbo].[OccupationList] (
    [OccupationId] char(4) COLLATE Greek_CI_AS NOT NULL,
    [OccupationDesc] nvarchar(80) COLLATE Greek_CI_AS NOT NULL
);

ALTER TABLE [dbo].[OccupationList] ADD CONSTRAINT [PK__Occupati__891711AD46D115D9] PRIMARY KEY CLUSTERED ([OccupationId] ASC) WITH (PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];

CREATE TABLE [dbo].[ServicesList] (
    [ServiceCode] char(3) COLLATE Greek_CI_AS NOT NULL,
    [ServiceDescription] nvarchar(80) COLLATE Greek_CI_AS NOT NULL
);

ALTER TABLE [dbo].[ServicesList] ADD CONSTRAINT [PK__Services__A01D74C8C4ADA1AA] PRIMARY KEY CLUSTERED ([ServiceCode] ASC) WITH (PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];

CREATE TABLE [dbo].[MembershipTypeList] (
    [MembershipType] nvarchar(4) COLLATE Greek_CI_AS NOT NULL,
    [Description] nvarchar(80) COLLATE Greek_CI_AS NOT NULL,
    [DurationDays] int NOT NULL
);

ALTER TABLE [dbo].[MembershipTypeList] ADD CONSTRAINT [PK__Membersh__68737B2010A0E7A9] PRIMARY KEY CLUSTERED ([MembershipType] ASC) WITH (PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];

CREATE TABLE [dbo].[PayMethodList] (
    [MethodType] char(2) COLLATE Greek_CI_AS NOT NULL,
    [MethodDescription] nvarchar(80) COLLATE Greek_CI_AS NOT NULL
);

ALTER TABLE [dbo].[PayMethodList] ADD CONSTRAINT [PK__PayMetho__4B35C52D48AB784A] PRIMARY KEY CLUSTERED ([MethodType] ASC) WITH (PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];

CREATE TABLE [dbo].[StatusList] (
    [StatusCode] char(1) COLLATE Greek_CI_AS NOT NULL,
    [StatusDesc] nvarchar(40) COLLATE Greek_CI_AS NOT NULL
);

ALTER TABLE [dbo].[StatusList] ADD CONSTRAINT [PK__StatusLi__6A7B44FD60A610C2] PRIMARY KEY CLUSTERED ([StatusCode] ASC) WITH (PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];

CREATE TABLE [dbo].[WorkoutGoals] (
    [GoalCode] nvarchar(4) COLLATE Greek_CI_AS NOT NULL,
    [Goal] nvarchar(100) COLLATE Greek_CI_AS NOT NULL,
    [Description] nvarchar(MAX) COLLATE Greek_CI_AS NULL
);

ALTER TABLE [dbo].[WorkoutGoals] ADD CONSTRAINT [PK_WorkoutGoals] PRIMARY KEY CLUSTERED ([GoalCode] ASC) WITH (PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];

CREATE TABLE [dbo].[Exercises] (
    [ExerciseCode] nvarchar(7) COLLATE Greek_CI_AS NOT NULL,
    [ExerciseName] nvarchar(200) COLLATE Greek_CI_AS NOT NULL,
    [MuscleGroup] nvarchar(50) COLLATE Greek_CI_AS NOT NULL
);

ALTER TABLE [dbo].[Exercises] ADD CONSTRAINT [PK_Exercises] PRIMARY KEY CLUSTERED ([ExerciseCode] ASC) WITH (PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];

CREATE TABLE [dbo].[HealthRecord] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [HasBodyPain] bit NOT NULL CONSTRAINT [DF__HealthRec__HasBo__412EB0B6] DEFAULT ((0)),
    [BodyPainDesc] nvarchar(200) COLLATE Greek_CI_AS NULL,
    [IsObest] bit NOT NULL CONSTRAINT [DF__HealthRec__IsObe__4222D4EF] DEFAULT ((0)),
    [IsSmoker] bit NOT NULL CONSTRAINT [DF__HealthRec__IsSmo__4316F928] DEFAULT ((0)),
    [FamilyHeartHistory] bit NOT NULL CONSTRAINT [DF__HealthRec__Famil__440B1D61] DEFAULT ((0)),
    [HasHypertasis] bit NOT NULL CONSTRAINT [DF__HealthRec__HasHy__44FF419A] DEFAULT ((0)),
    [HasDiabetes] bit NOT NULL CONSTRAINT [DF__HealthRec__HasDi__45F365D3] DEFAULT ((0)),
    [HasHeartIssues] bit NOT NULL CONSTRAINT [DF__HealthRec__HasHe__46E78A0C] DEFAULT ((0)),
    [HasAsthma] bit NOT NULL CONSTRAINT [DF__HealthRec__HasAs__47DBAE45] DEFAULT ((0)),
    [HasThyroedes] bit NOT NULL CONSTRAINT [DF__HealthRec__HasTh__48CFD27E] DEFAULT ((0)),
    [HasArthritis] bit NOT NULL CONSTRAINT [DF__HealthRec__HasAr__49C3F6B7] DEFAULT ((0)),
    [HasOsteoporosis] bit NOT NULL CONSTRAINT [DF__HealthRec__HasOs__4AB81AF0] DEFAULT ((0)),
    [HasAllergies] bit NOT NULL CONSTRAINT [DF__HealthRec__HasAl__4BAC3F29] DEFAULT ((0)),
    [HasMyosceletic] bit NOT NULL CONSTRAINT [DF__HealthRec__HasMy__4CA06362] DEFAULT ((0)),
    [MyoskeleticDesc] nvarchar(200) COLLATE Greek_CI_AS NULL,
    [HasOther] bit NOT NULL CONSTRAINT [DF__HealthRec__HasOt__4D94879B] DEFAULT ((0)),
    [OtherDesc] nvarchar(200) COLLATE Greek_CI_AS NULL,
    [EmergencyContact] nvarchar(100) COLLATE Greek_CI_AS NULL,
    [EmergencyPhone] char(10) COLLATE Greek_CI_AS NULL,
    [Comment] nvarchar(MAX) COLLATE Greek_CI_AS NULL
);

ALTER TABLE [dbo].[HealthRecord] ADD CONSTRAINT [PK__HealthRe__3214EC07C735A6DD] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];

CREATE TABLE [dbo].[Customers] (
    [Id] bigint NOT NULL,
    [LastName] nvarchar(50) COLLATE Greek_CI_AS NOT NULL,
    [FirstName] nvarchar(50) COLLATE Greek_CI_AS NOT NULL,
    [FatherName] nvarchar(50) COLLATE Greek_CI_AS NULL,
    [Birthday] date NULL,
    [Gender] nchar(1) COLLATE Greek_CI_AS NULL,
    [OccupationId] char(4) COLLATE Greek_CI_AS NULL,
    [HealthId] bigint NULL,
    [Address] nvarchar(100) COLLATE Greek_CI_AS NULL,
    [City] nvarchar(50) COLLATE Greek_CI_AS NOT NULL CONSTRAINT [DF_Customers_City] DEFAULT (N'Θεσσαλονίκη'),
    [Home] char(10) COLLATE Greek_CI_AS NULL,
    [Mobile] char(10) COLLATE Greek_CI_AS NOT NULL,
    [Email] nvarchar(50) COLLATE Greek_CI_AS NOT NULL,
    [Status] bit NOT NULL CONSTRAINT [DF_Customers_Status] DEFAULT ((1)),
    [CreationDate] datetime2(0) NOT NULL CONSTRAINT [DF_Customers_CreationDate] DEFAULT (sysdatetime()),
    [Comments] nvarchar(MAX) COLLATE Greek_CI_AS NULL
);

ALTER TABLE [dbo].[Customers] ADD CONSTRAINT [PK__Customer__3214EC074BB508BF] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];

CREATE UNIQUE NONCLUSTERED INDEX [UX_Customers_HealthId] ON [dbo].[Customers] ([HealthId] ASC) WHERE ([HealthId] IS NOT NULL) WITH (PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];

ALTER TABLE [dbo].[Customers] WITH CHECK ADD CONSTRAINT [CK__Customers__Gende__5070F446] CHECK ([Gender]=N'Γ' OR [Gender]=N'Α');

CREATE TABLE [dbo].[Membership] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [CustomerId] bigint NOT NULL,
    [ServiceCode] char(3) COLLATE Greek_CI_AS NOT NULL,
    [MembershipType] nvarchar(4) COLLATE Greek_CI_AS NOT NULL,
    [Duration] int NOT NULL,
    [StartDate] date NOT NULL,
    [EndDate] date NOT NULL,
    [Price] decimal(10,2) NOT NULL,
    [StatusCode] char(1) COLLATE Greek_CI_AS NOT NULL CONSTRAINT [DF_Membership_Status] DEFAULT ('0'),
    [Comment] nvarchar(MAX) COLLATE Greek_CI_AS NULL,
    [CreationDate] datetime NOT NULL CONSTRAINT [DF_Membership_CreationDate] DEFAULT (getdate())
);

ALTER TABLE [dbo].[Membership] ADD CONSTRAINT [PK__Membersh__3214EC0700635927] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];

CREATE NONCLUSTERED INDEX [IX_Membership_CustomerDates] ON [dbo].[Membership] ([CustomerId] ASC, [StartDate] ASC, [EndDate] ASC) WITH (PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];

CREATE TABLE [dbo].[Payments] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [MembershipId] bigint NOT NULL,
    [PaymentDate] date NOT NULL,
    [PaymentTime] time(0) NULL,
    [MethodType] char(2) COLLATE Greek_CI_AS NOT NULL,
    [Amount] decimal(10,2) NOT NULL,
    [CreationDate] datetime2(0) NOT NULL CONSTRAINT [DF_Payments_CreationDate] DEFAULT (sysdatetime()),
    [Comment] nvarchar(MAX) COLLATE Greek_CI_AS NULL
);

ALTER TABLE [dbo].[Payments] ADD CONSTRAINT [PK__Payments__3214EC070B931B5C] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];

CREATE TABLE [dbo].[ProgramsWorkout] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CustomerId] bigint NOT NULL,
    [GoalCode] nvarchar(4) COLLATE Greek_CI_AS NOT NULL,
    [Frequency] int NOT NULL,
    [StartDate] date NOT NULL,
    [EndDate] date NOT NULL,
    [Comments] nvarchar(MAX) COLLATE Greek_CI_AS NULL,
    [Duration] int NOT NULL
);

ALTER TABLE [dbo].[ProgramsWorkout] ADD CONSTRAINT [PK_ProgramWorkout] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];

CREATE NONCLUSTERED INDEX [IX_ProgramWorkout_CustomerId] ON [dbo].[ProgramsWorkout] ([CustomerId] ASC) WITH (PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];

CREATE NONCLUSTERED INDEX [IX_ProgramWorkout_GoalCode] ON [dbo].[ProgramsWorkout] ([GoalCode] ASC) WITH (PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];

ALTER TABLE [dbo].[ProgramsWorkout] WITH CHECK ADD CONSTRAINT [CK_ProgramWorkout_Frequency] CHECK ([Frequency]>(0));

ALTER TABLE [dbo].[ProgramsWorkout] WITH CHECK ADD CONSTRAINT [CK_ProgramWorkout_Dates] CHECK ([EndDate]>=[StartDate]);

ALTER TABLE [dbo].[ProgramsWorkout] WITH CHECK ADD CONSTRAINT [CK_ProgramsWorkout_Duration] CHECK ([Duration]>=(1) AND [Duration]<=(8));

CREATE TABLE [dbo].[ProgramsWorkoutDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProgramId] int NOT NULL,
    [ExerciseCode] nvarchar(7) COLLATE Greek_CI_AS NOT NULL,
    [Sets] int NOT NULL,
    [Reps] int NOT NULL,
    [RestTime] nvarchar(20) COLLATE Greek_CI_AS NOT NULL,
    [DayOfWeek] nvarchar(20) COLLATE Greek_CI_AS NOT NULL
);

ALTER TABLE [dbo].[ProgramsWorkoutDetails] ADD CONSTRAINT [PK_ProgramWorkoutDetails] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];

CREATE NONCLUSTERED INDEX [IX_ProgramWorkoutDetails_ProgramId] ON [dbo].[ProgramsWorkoutDetails] ([ProgramId] ASC) WITH (PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];

CREATE NONCLUSTERED INDEX [IX_ProgramWorkoutDetails_ExerciseCode] ON [dbo].[ProgramsWorkoutDetails] ([ExerciseCode] ASC) WITH (PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];

ALTER TABLE [dbo].[ProgramsWorkoutDetails] WITH CHECK ADD CONSTRAINT [CK_ProgramWorkoutDetails_Sets] CHECK ([Sets]>(0));

ALTER TABLE [dbo].[ProgramsWorkoutDetails] WITH CHECK ADD CONSTRAINT [CK_ProgramWorkoutDetails_Reps] CHECK ([Reps]>(0));

ALTER TABLE [dbo].[Customers] WITH CHECK ADD CONSTRAINT [FK_Customers_Occupation] FOREIGN KEY ([OccupationId]) REFERENCES [dbo].[OccupationList] ([OccupationId]);

ALTER TABLE [dbo].[Customers] WITH CHECK ADD CONSTRAINT [FK_Customers_Health] FOREIGN KEY ([HealthId]) REFERENCES [dbo].[HealthRecord] ([Id]);

ALTER TABLE [dbo].[Membership] WITH CHECK ADD CONSTRAINT [FK_Membership_Customers] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers] ([Id]);

ALTER TABLE [dbo].[Membership] WITH CHECK ADD CONSTRAINT [FK_Membership_Service] FOREIGN KEY ([ServiceCode]) REFERENCES [dbo].[ServicesList] ([ServiceCode]);

ALTER TABLE [dbo].[Membership] WITH CHECK ADD CONSTRAINT [FK_Membership_Type] FOREIGN KEY ([MembershipType]) REFERENCES [dbo].[MembershipTypeList] ([MembershipType]);

ALTER TABLE [dbo].[Membership] WITH CHECK ADD CONSTRAINT [FK_Membership_Status] FOREIGN KEY ([StatusCode]) REFERENCES [dbo].[StatusList] ([StatusCode]);

ALTER TABLE [dbo].[Payments] WITH CHECK ADD CONSTRAINT [FK_Payments_Method] FOREIGN KEY ([MethodType]) REFERENCES [dbo].[PayMethodList] ([MethodType]);

ALTER TABLE [dbo].[Payments] WITH CHECK ADD CONSTRAINT [FK_Payments_Membership] FOREIGN KEY ([MembershipId]) REFERENCES [dbo].[Membership] ([Id]);

ALTER TABLE [dbo].[ProgramsWorkout] WITH CHECK ADD CONSTRAINT [FK_ProgramWorkout_Customers] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers] ([Id]);

ALTER TABLE [dbo].[ProgramsWorkout] WITH CHECK ADD CONSTRAINT [FK_ProgramWorkout_WorkoutGoals] FOREIGN KEY ([GoalCode]) REFERENCES [dbo].[WorkoutGoals] ([GoalCode]);

ALTER TABLE [dbo].[ProgramsWorkoutDetails] WITH CHECK ADD CONSTRAINT [FK_ProgramWorkoutDetails_Exercises] FOREIGN KEY ([ExerciseCode]) REFERENCES [dbo].[Exercises] ([ExerciseCode]);

ALTER TABLE [dbo].[ProgramsWorkoutDetails] WITH CHECK ADD CONSTRAINT [FK_ProgramWorkoutDetails_ProgramWorkout] FOREIGN KEY ([ProgramId]) REFERENCES [dbo].[ProgramsWorkout] ([Id]) ON DELETE CASCADE;
    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
