CREATE PROCEDURE [dbo].[BookReadAllAsUser_sp]
@AppUserID nvarchar(450)
AS
SELECT
	B.BookID, 
	B.BookName, 
	B.BookDescription, 
	B.Quantity, 
	B.Quantity - B.Booked AS 'Available',
	B.AuthorName,
	CASE WHEN BS.BookReservationStatusName IS NULL THEN 'Available' else BS.BookReservationStatusName END AS 'BookStatus'
FROM Book AS B WITH (NOLOCK)
LEFT JOIN BookReservation AS BR WITH (NOLOCK)
	ON B.BookID = BR.BookID AND BR.AppUserID = @AppUserID
LEFT JOIN BookReservationStatus AS BS WITH (NOLOCK)
	ON BR.BookReservationStatusID = BS.BookReservationStatusID
