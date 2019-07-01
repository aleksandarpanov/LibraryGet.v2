CREATE PROCEDURE [dbo].[BookReservationCreate_sp]
@BookID int,
@AppUserID nvarchar(256)
AS

DECLARE @ExistingID INT

SELECT @ExistingID = BR.BookReservationID
FROM BookReservation AS BR WITH (NOLOCK)
INNER JOIN BookReservationStatus AS BRS WITH (NOLOCK)
	ON BR.BookReservationStatusID = BRS.BookReservationStatusID
WHERE 
	BR.BookID = @BookID
AND
	BR.AppUserID = @AppUserID
AND
	BRS.BookReservationStatusName = 'Pending'


IF @ExistingID > 0
	BEGIN
		SELECT 0
	END
ELSE
	BEGIN

		DECLARE @BookReservationStatusID INT

		SELECT @BookReservationStatusID = BookReservationStatusID
		FROM BookReservationStatus
		WHERE
			BookReservationStatusName = 'Pending'

		IF @BookReservationStatusID > 0
			BEGIN
				INSERT INTO [dbo].[BookReservation]([BookID], [AppUserID], [BookReservationStatusID]) 
				VALUES(@BookID, @AppUserID, @BookReservationStatusID)

				SELECT
					BR.BookReservationID,
					BR.BookID,
					B.BookName,
					BR.AppUserID,
					AU.UserName
				FROM
				BookReservation AS BR WITH (NOLOCK)
				INNER JOIN Book AS B  WITH (NOLOCK)
					ON BR.BookID = B.BookID
				INNER JOIN AspNetUsers AS AU WITH (NOLOCK)
					ON BR.AppUserID = AU.Id
				WHERE BookReservationID = CAST (SCOPE_IDENTITY() AS INT)
			END
		ELSE
			SELECT 0

	END