CREATE TABLE [dbo].[Book]
(
	[BookID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [BookName] VARCHAR(256) NOT NULL, 
    [BookDescription] VARCHAR(2048) NOT NULL, 
    [Quantity] INT NOT NULL, 
    [Booked] INT NOT NULL, 
    [AuthorName] VARCHAR(128) NOT NULL
)
