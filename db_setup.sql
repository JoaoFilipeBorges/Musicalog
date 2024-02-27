CREATE DATABASE Musicalog;
GO

USE Musicalog;
GO

go
Create table Album
(
	id int identity(1,1) primary key,
	clientid uniqueidentifier  unique not null,
	title nvarchar(256) not null,
	artistName nvarchar(256) not null,
	[type] nvarchar(8) not null,
	stock int not null,
	cover nvarchar(max)
	CONSTRAINT [album_artist_type] UNIQUE NONCLUSTERED
	(
		title, artistName, type
	)
)

go
CREATE PROC GetAlbumsPage @pageSz INT, @pageNum INT, @artistName nvarchar(256), @title nvarchar(256)
AS
BEGIN
	declare @fstEntry as INT
	set @fstEntry = (@pageNum-1) * @pageSz
	SELECT clientid as id, title, artistName, type, stock, cover
	FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY id) AS RowNum , *
			  FROM      Album
			  WHERE (@title IS NULL OR title = @title) AND (@artistName IS NULL OR artistName = @artistName)
			) AS result
	WHERE   RowNum > @fstEntry
		AND RowNum <= @fstEntry+@pageSz
	ORDER BY RowNum
END

go
CREATE PROC GetAlbumsCount @artistName nvarchar(256), @title nvarchar(256)
AS
BEGIN
	SELECT COUNT(1) 
	FROM Album
	WHERE (@title IS NULL OR title = @title) AND (@artistName IS NULL OR artistName = @artistName)
END

go
INSERT INTO Album (clientid, title, artistName, [type], stock) VALUES
(NEWID(),'Abbey Road', 'The Beatles', 'CD', 200),
(NEWID(),'Rumours', 'Fleetwood Mac', 'CD', 120),
(NEWID(),'Back in Black', 'AC/DC', 'CD', 180),
(NEWID(),'Led Zeppelin IV', 'Led Zeppelin', 'CD', 90),
(NEWID(),'The Wall', 'Pink Floyd', 'CD', 110),
(NEWID(),'Hotel California', 'Eagles', 'CD', 170),
(NEWID(),'A Night at the Opera', 'Queen', 'CD', 130),
(NEWID(),'Born to Run', 'Bruce Springsteen', 'CD', 140),
(NEWID(),'Nevermind', 'Nirvana', 'CD', 190),
(NEWID(),'The White Album', 'The Beatles', 'CD', 220),
(NEWID(),'The Joshua Tree', 'U2', 'CD', 250),
(NEWID(),'Bridge over Troubled Water', 'Simon & Garfunkel', 'CD', 80),
(NEWID(),'Darkness on the Edge of Town', 'Bruce Springsteen', 'CD', 110),
(NEWID(),'Who''s Next', 'The Who', 'CD', 130),
(NEWID(),'Led Zeppelin II', 'Led Zeppelin', 'CD', 170),
(NEWID(),'Pet Sounds', 'The Beach Boys', 'CD', 150),
(NEWID(),'Revolver', 'The Beatles', 'CD', 120),
(NEWID(),'The Rise and Fall of Ziggy Stardust and the Spiders from Mars', 'David Bowie', 'CD', 160),
(NEWID(),'The Doors', 'The Doors', 'CD', 180),
(NEWID(),'Blue', 'Joni Mitchell', 'CD', 100),
(NEWID(),'The College Dropout', 'Kanye West', 'CD', 200),
(NEWID(),'Goodbye Yellow Brick Road', 'Elton John', 'CD', 120),
(NEWID(),'The Chronic', 'Dr. Dre', 'CD', 140),
(NEWID(),'Sign o'' the Times', 'Prince', 'CD', 160),
(NEWID(),'Songs in the Key of Life', 'Stevie Wonder', 'CD', 180),
(NEWID(),'The Marshall Mathers LP', 'Eminem', 'CD', 200),
(NEWID(),'Court and Spark', 'Joni Mitchell', 'CD', 220),
(NEWID(),'Back to Black', 'Amy Winehouse', 'CD', 200),
(NEWID(),'The Marshall Mathers LP 2', 'Eminem', 'CD', 240),
(NEWID(),'Purple Rain', 'Prince', 'CD', 170),
(NEWID(),'The Dark Side of the Moon', 'Pink Floyd', 'CD', 220);
