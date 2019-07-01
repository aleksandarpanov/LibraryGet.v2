CREATE PROCEDURE [dbo].[BookCreate_sp]
@BookName varchar(128),
@BookDescription varchar(128),
@Quantity int,
@AuthorName varchar(64)
AS

DECLARE @BookID AS INT

SELECT @BookID = ISNULL(BookID, 0)
FROM Book
WHERE
	BookName = @BookName
AND
	AuthorName = @AuthorName

IF @BookID > 0
	BEGIN
		SELECT -1
	END
ELSE
	BEGIN
		INSERT INTO [dbo].[Book]([BookName],[BookDescription],[Quantity],[AuthorName])
			 VALUES (@BookName, @BookDescription, @Quantity, @AuthorName)
  
		SELECT CAST (SCOPE_IDENTITY() AS INT)
	END