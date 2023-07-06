using MultiTracksWebApi.Models;
using MultiTracksWebApi.Requests;

namespace MultiTracksWebApi.Services.Interface
{
    public interface IArtistDetails
    {
        List<Artist> GetArtistByName(string artistName);
        int CreateArtist(ArtistRequest request);
    }
}
