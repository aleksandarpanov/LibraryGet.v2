CREATE PROCEDURE [dbo].[BookReadAllAsAdmin_sp]
AS
SELECT
	B.BookID, 
	B.BookName, 
	B.BookDescription, 
	B.Quantity, 
	B.Quantity - B.Booked AS 'Available',
	B.AuthorName
FROM Book AS B WITH (NOLOCK)
