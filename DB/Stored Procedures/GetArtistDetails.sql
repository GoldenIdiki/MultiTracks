CREATE PROCEDURE dbo.GetArtistDetails

AS
BEGIN

	SELECT 
		s.songID AS SongID,
		s.title AS SongTitle,
		s.bpm AS BPM,
		s.timeSignature AS TimeSignature,
		a.artistID AS ArtistID,
		a.Title AS ArtistName,
		a.biography AS ArtistBiography,
		a.imageURL AS ArtistImageURL,
		al.albumID AS AlbumID,
		al.Title AS AlbumTitle,
		al.imageURL AS AlbumImageURL
	FROM Song s 
	INNER JOIN Artist a ON s.artistID = a.artistID 
	INNER JOIN Album al ON a.artistID = al.artistID;

END