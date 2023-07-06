using DataAccess;
using MultiTracksWebApi.Models;
using MultiTracksWebApi.Requests;
using MultiTracksWebApi.Services.Interface;
using System.Data;

namespace MultiTracksWebApi.Services.Implementation
{
    public class ArtistDetails : IArtistDetails
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ArtistDetails> _logger;
        private readonly IFileUpload _file;

        public ArtistDetails(IConfiguration configuration, ILogger<ArtistDetails> logger, IFileUpload file)
        {
            _configuration = configuration;
            _logger = logger;
            _file = file;
        }
        public List<Artist> GetArtistByName(string artistName)
        {
            var artists = new List<Artist>();
            try
            {
                _logger.LogInformation("Connecting to Database...");
                var sql = new SQL(60, _configuration);
                var script = $"SELECT * FROM Artist WHERE title = @artistName";
                var parameters = sql.Parameters;
                parameters.Add("@artistName", artistName);
                _logger.LogInformation("Executing query...");
                var result = sql.ExecuteDT(script);

                foreach (DataRow item in result.Rows)
                {
                    artists.Add(new Artist
                    {
                        ArtistID = Convert.ToInt32(item["artistID"]),
                        DateCreation = DateTime.Parse(item["dateCreation"].ToString() ?? string.Empty),
                        Title = item["title"].ToString() ?? string.Empty,
                        Biography = item["biography"].ToString() ?? string.Empty,
                        ImageURL = item["imageUrl"].ToString() ?? string.Empty,
                        HeroURL = item["heroUrl"].ToString() ?? string.Empty,
                    });
                }
                _logger.LogInformation("Query was successful...");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                throw;
            }
            
            return artists;
        }

        public int CreateArtist(ArtistRequest request)
        {
            int result;
            try
            {
                _logger.LogInformation("Connecting to Database...");
                var sql = new SQL(60, _configuration);

                _logger.LogInformation("Uploading Pictures...");
                var artistImageResponse = _file.UploadAvatar(request.ArtistImage);
                var heroImageResponse = _file.UploadAvatar(request.HeroImage);
                var script = "Insert Into Artist (dateCreation, title, biography, imageURL, heroURL) " +
                    "Values (@dateCreation, @title, @biography, @imageURL, @heroURL)";
                var test = sql.Parameters;
                test.Add("@dateCreation", DateTime.UtcNow);
                test.Add("@title", request.Title);
                test.Add("@biography", request.Biography);
                test.Add("@imageURL", artistImageResponse.ImageUrl ?? string.Empty);
                test.Add("@heroURL", heroImageResponse.ImageUrl ?? string.Empty);

                _logger.LogInformation("Executing query...");
                result = sql.Execute(script);
            }
            catch (Exception)
            {

                throw;
            }
            _logger.LogInformation("Query was successful...");
            return result;
        }
    }
}
