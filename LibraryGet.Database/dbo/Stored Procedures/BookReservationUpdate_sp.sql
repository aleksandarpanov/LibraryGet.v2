CREATE PROCEDURE [dbo].[BookReservationUpdate_sp]
@BookReservationID INT,
@ReturnDate DATETIME NULL,
@BookReservationStatusID INT
AS

-- check status name
DECLARE @BookReservationStatusName VARCHAR(64)

SELECT BookReservationStatusName = @BookReservationStatusName
FROM BookReservationStatus WITH (NOLOCK)
WHERE BookReservationStatusID = @BookReservationStatusID

IF @BookReservationStatusName = 'Pending'
	BEGIN
		-- check if there are available books
		DECLARE @BookID INT

		SELECT BookID = @BookID
		FROM BookReservation WITH (NOLOCK)
		WHERE @BookReservationID = @BookReservationID

		DECLARE @Booked INT
		DECLARE @Available INT
		
		SELECT Booked = @Booked, Available = @Available
		FROM Book WITH (NOLOCK)
		WHERE BookID = @BookID

		IF (@Available > @Booked)
			BEGIN
				UPDATE BookReservation
				SET 
					ReturnDate = @ReturnDate,
					BookReservationStatusID = @BookReservationStatusID
				WHERE BookReservationID = @BookReservationID

				RETURN 1
			END
		ELSE
			BEGIN
				RETURN 0
			END
	END
ELSE
	BEGIN
		UPDATE BookReservation
		SET 
			ReturnDate = GETDATE(),
			BookReservationStatusID = @BookReservationStatusID
			WHERE BookReservationID = @BookReservationID

			RETURN 1
	END

RETURN 0
