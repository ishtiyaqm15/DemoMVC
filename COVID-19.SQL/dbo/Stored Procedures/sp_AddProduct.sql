CREATE PROCEDURE [dbo].[sp_AddProduct]
@Name nvarchar(255)
,@ShortDescription nvarchar(500)
,@LongDescription nvarchar(MAX)
,@Image image
,@Price decimal(18,0)
,@CreatedBy nvarchar(128)
AS
BEGIN
	DECLARE @productId int = 0;
	BEGIN TRY

		BEGIN TRANSACTION

			INSERT INTO [dbo].[Products](Name, ShortDescription, LongDescription, Image, Price, CreatedOn, CreatedBy)
			VALUES (@Name, @ShortDescription, @LongDescription, @Image, @Price, GETDATE(), @CreatedBy)
	
			SET @productId = SCOPE_IDENTITY()
		
		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION

	END CATCH
	SELECT @productId
END
