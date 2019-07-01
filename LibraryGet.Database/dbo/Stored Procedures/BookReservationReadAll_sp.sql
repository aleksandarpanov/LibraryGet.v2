CREATE PROCEDURE [dbo].[BookReservationReadAll_sp]
AS
SELECT
	BR.BookReservationID,
	BR.BookID,
	B.BookName,
	BR.AppUserID,
	AU.UserName,
	B.Quantity - B.Booked AS 'Available',
	BR.BookReservationStatusID,
	BRS.BookReservationStatusName,
	BR.ReturnDate
FROM BookReservation AS BR WITH (NOLOCK)
INNER JOIN Book AS B WITH (NOLOCK)
	ON BR.BookID = B.BookID
INNER JOIN BookReservationStatus AS BRS WITH (NOLOCK)
	ON BR.BookReservationStatusID = BRS.BookReservationStatusID
INNER JOIN AspNetUsers AS AU WITH (NOLOCK)
	ON BR.AppUserID = AU.Id
WHERE BRS.BookReservationStatusName IN ('Pending', 'Reserved')