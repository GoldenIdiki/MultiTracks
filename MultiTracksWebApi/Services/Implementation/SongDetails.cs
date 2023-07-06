using DataAccess;
using MultiTracksWebApi.Models;
using MultiTracksWebApi.Pagination;
using MultiTracksWebApi.Response;
using MultiTracksWebApi.Services.Interface;
using System.Data;

namespace MultiTracksWebApi.Services.Implementation
{
    public class SongDetails : ISongDetails
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<SongDetails> _logger;
        public SongDetails(IConfiguration configuration, ILogger<SongDetails> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        public SongsAndPageDetails GetSongs(int pageNumber, int pageSize)
        {
            SongsAndPageDetails songsAndPageDetails;
            try
            {
                _logger.LogInformation("Connecting to Database...");
                var sql = new SQL(60, _configuration);
                _logger.LogInformation("Executing query...");

                var page = new PageDetails(pageSize);

                var parameters = sql.Parameters;
                parameters.Add("@PageNum", pageNumber);
                parameters.Add("@PageSize", page.ExpectedPageSize);
                var result = sql.ExecuteStoredProcedureDT("GetPaginatedSongs");

                var songs = new List<Song>();
                foreach (DataRow item in result.Rows)
                {
                    songs.Add(new Song
                    {
                        SongID = Convert.ToInt32(item["songID"]),
                        Title = item["title"].ToString() ?? string.Empty,
                        BPM = Convert.ToDecimal(item["bpm"].ToString() ?? string.Empty),
                    });
                }

                int totalRows = 0;
                foreach (DataRow item in result.Rows)
                {
                    totalRows = Convert.ToInt32(item["TotalRows"]);
                    break;
                }

                var pageDetails = new PageDetails(totalRows, pageNumber, pageSize, songs.Count);
                songsAndPageDetails = new SongsAndPageDetails(songs, pageDetails);

                _logger.LogInformation("Query was successful...");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                throw;
            }
            return songsAndPageDetails;
        }
    }
}
