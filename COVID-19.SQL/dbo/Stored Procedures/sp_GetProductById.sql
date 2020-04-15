CREATE PROCEDURE [dbo].[sp_GetProductById]
@ProductId INT
AS
BEGIN

	SELECT [Id]
      ,[Name]
      ,[ShortDescription]
      ,[LongDescription]
      ,[Image]
      ,[Price]
      ,[CreatedOn]
      ,[CreatedBy]
      ,[UpdatedOn]
      ,[UpdateBy]
	  ,[IsActive] FROM
	  [dbo].[Products] (NOLOCK)
	  WHERE [Id] = @ProductId
END
