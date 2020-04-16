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
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'1', N'Admin')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'2', N'Content Contributors')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'3', N'Viewers')
GO

INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'05149b66-5907-47b1-b3d8-4d7d181f7ded', N'viewers@mail.com', 0, N'AADQFw28NwGWMltx1MIxFsIUNdd/h9qIW7+9WkvV7s9JiRcjW8IYdDyEEyMLucNgpw==', N'3b9637f5-04e7-4c64-91a4-0d0ef2ef8350', NULL, 0, 0, NULL, 1, 0, N'viewers@mail.com')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'078d69a9-003a-4e7a-bf7d-b165664115a4', N'admin@mail.com', 0, N'AADQFw28NwGWMltx1MIxFsIUNdd/h9qIW7+9WkvV7s9JiRcjW8IYdDyEEyMLucNgpw==', N'3b9637f5-04e7-4c64-91a4-0d0ef2ef8350', NULL, 0, 0, NULL, 1, 0, N'ishtiyaqm15@gmail.com')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'72dba633-313f-45f6-b2d3-be097ce30050', N'contentcontributors@mail.com', 0, N'AADQFw28NwGWMltx1MIxFsIUNdd/h9qIW7+9WkvV7s9JiRcjW8IYdDyEEyMLucNgpw==', N'3b9637f5-04e7-4c64-91a4-0d0ef2ef8350', NULL, 0, 0, NULL, 1, 0, N'contentcontributors@mail.com')
GO

INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'078d69a9-003a-4e7a-bf7d-b165664115a4', N'1')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'72dba633-313f-45f6-b2d3-be097ce30050', N'2')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'05149b66-5907-47b1-b3d8-4d7d181f7ded', N'3')
GO