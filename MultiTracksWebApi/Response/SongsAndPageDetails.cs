using MultiTracksWebApi.Models;
using MultiTracksWebApi.Pagination;

namespace MultiTracksWebApi.Response
{
    public class SongsAndPageDetails
    {
        public SongsAndPageDetails(List<Song> songs, PageDetails pageDetails)
        {
            Songs = songs;
            PageDetails = pageDetails;
        }

        public List<Song> Songs { get; set; }
        public PageDetails PageDetails { get; set; }
    }
}
