/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
INSERT INTO dbo.AspNetRoles
(Id, Name)
VALUES
(1, 'Admin')

INSERT INTO dbo.AspNetRoles
(Id, Name)
VALUES
(2, 'Content Contributors')

INSERT INTO dbo.AspNetRoles
(Id, Name)
VALUES
(3, 'Viewers')