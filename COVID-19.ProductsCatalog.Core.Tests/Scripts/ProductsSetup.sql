DECLARE @Id AS NVARCHAR(128) = CAST(NewID() AS NVARCHAR(128))

INSERT INTO dbo.AspNetUsers
(Id, EmailConfirmed, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, UserName)
VALUES (@Id, 0, 0, 0, 0, 0, 'TestUser')


SET IDENTITY_INSERT [dbo].[Products] ON 
INSERT [dbo].[Products] ([Id], [Name], [ShortDescription], [LongDescription], [Image], [Price], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdateBy], [IsActive]) VALUES (1, N'Mask', N'Health Equipment', N'Health Equipment', NULL, CAST(2 AS Decimal(18, 0)), CAST(N'2019-04-14T00:00:00.000' AS DateTime), @Id, NULL, NULL, 1)
INSERT [dbo].[Products] ([Id], [Name], [ShortDescription], [LongDescription], [Image], [Price], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdateBy], [IsActive]) VALUES (2, N'Gloves', N'Health Equipment', N'Health Equipment', NULL, CAST(4 AS Decimal(18, 0)), CAST(N'2019-04-14T00:00:00.000' AS DateTime), @Id, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Products] OFF