using MultiTracksWebApi.Models;
using MultiTracksWebApi.Pagination;
using MultiTracksWebApi.Response;

namespace MultiTracksWebApi.Services.Interface
{
    public interface ISongDetails
    {
        SongsAndPageDetails GetSongs(int pageNumber, int pageSize);
    }
}
