CREATE TABLE [dbo].[Products]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(255) NOT NULL,
	[ShortDescription] NVARCHAR(500) NULL,
	[LongDescription] NVARCHAR(MAX) NULL,
	[Image] Image NULL,
	[Price] DECIMAL NOT NULL,
	[CreatedOn] DATETIME NOT NULL,
	[CreatedBy] NVARCHAR (128) NOT NULL,
	[UpdatedOn] DATETIME NULL,
	[UpdateBy] NVARCHAR (128) NULL
    CONSTRAINT [FK_dbo.Products_dbo.AspNetUsers_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
	CONSTRAINT [FK_dbo.Products_dbo.AspNetUsers_UpdateBy] FOREIGN KEY ([UpdateBy]) REFERENCES [dbo].[AspNetUsers] ([Id]), 
    [IsActive] BIT DEFAULT(1) NOT NULL
)
