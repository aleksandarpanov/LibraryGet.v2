CREATE TABLE [dbo].[BookReservation]
(
	[BookReservationID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ReturnDate] DATETIME NULL, 
    [BookID] INT NOT NULL, 
    [AppUserID] NVARCHAR(450) NOT NULL, 
    [BookReservationStatusID] INT NOT NULL
)
