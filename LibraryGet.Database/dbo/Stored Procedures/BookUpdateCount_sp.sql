CREATE PROCEDURE [dbo].[BookUpdateCount_sp]
@BookID INT,
@BookReservationStatusID INT
AS

DECLARE @BookReservationStatusName VARCHAR(64)

SELECT @BookReservationStatusName = BookReservationStatusName
FROM BookReservationStatus WITH (NOLOCK)
WHERE BookReservationStatusID = @BookReservationStatusID

IF (@BookReservationStatusName = 'Reserved')
	BEGIN
		DECLARE @BookAvailableCount AS INT

		SELECT @BookAvailableCount = Booked
		FROM Book WITH (NOLOCK)
		WHERE BookID = @BookID

		IF @BookAvailableCount >= 0
			BEGIN
				UPDATE Book
				SET Booked = Booked + 1
				WHERE BookID = @BookID

				SELECT @@ROWCOUNT
			END
	END
ELSE IF (@BookReservationStatusName = 'Returned')
	BEGIN
		UPDATE Book
		SET Booked = Booked - 1
		WHERE BookID = @BookID

		SELECT @@ROWCOUNT
	END
ELSE
	SELECT 0
