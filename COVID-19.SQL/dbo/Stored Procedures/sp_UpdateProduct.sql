CREATE PROCEDURE [dbo].[sp_UpdateProduct]
@ProductId INT
,@Name nvarchar(255)
,@ShortDescription nvarchar(500)
,@LongDescription nvarchar(MAX)
,@Image image
,@Price decimal(18,0)
,@UpdatedBy nvarchar(128)
AS
BEGIN
	BEGIN TRY

		BEGIN TRANSACTION

			UPDATE [dbo].[Products]	
			SET [Name] = @Name
			,[ShortDescription] = @ShortDescription
			,[LongDescription] = @LongDescription
			,[Image] = @Image
			,[Price] = @Price
			,[UpdatedOn] = GETDATE()
			,[UpdateBy] = @UpdatedBy
			FROM
			[dbo].[Products] (NOLOCK)
			WHERE [Id] = @ProductId
			
			SELECT 1
		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION

	END CATCH
END
