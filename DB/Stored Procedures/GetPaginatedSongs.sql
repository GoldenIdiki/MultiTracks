CREATE PROCEDURE dbo.GetPaginatedSongs
	@PageNum INT,
	@PageSize INT
AS
BEGIN

	WITH TempResult AS
	(
		SELECT  songID, title, bpm FROM Song
	), 
	TempCount AS
	(
		SELECT COUNT(*) AS  TotalRows FROM TempResult
	)
	SELECT *
		FROM TempResult, TempCount
		ORDER BY TempResult.songID
		OFFSET (@PageNum - 1) * @PageSize ROWS
		FETCH NEXT @PageSize ROWS ONLY; 

END
