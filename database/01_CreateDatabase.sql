-- Create only the portfolio demo database. Existing databases are left untouched.
SET NOCOUNT ON;
IF DB_ID(N'GymMasterDBDemo') IS NULL
BEGIN
    EXEC(N'CREATE DATABASE [GymMasterDBDemo]');
    PRINT N'GymMasterDBDemo created using SQL Server default storage locations.';
END
ELSE
    PRINT N'GymMasterDBDemo already exists; no changes made.';
GO
