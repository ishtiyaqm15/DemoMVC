CREATE PROCEDURE [dbo].[sp_GetAllProducts]
@isActive BIT = 1
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
	  WHERE [IsActive] = @isActive
	  ORDER BY NAME ASC
END
