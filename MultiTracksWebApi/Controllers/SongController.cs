using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MultiTracksWebApi.Response;
using MultiTracksWebApi.Services.Interface;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MultiTracksWebApi.Controllers
{
    [Route("api.multitracks.com/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly ISongDetails _songDetails;

        public SongController(ISongDetails songDetails)
        {
            _songDetails = songDetails;
        }

        /// <summary>
        /// Gets songs from the database and adds pagination to it.&#xA;
        /// If you input a page size that is more than 50, then 50 will be used as page size.
        /// In that case ExpectedPageSize will be what you inputted but ActualPageSize will be 50
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api.multitracks.com/song/list?pageNumber=1&amp;pageSize=10
        /// </remarks>
        /// <returns code="200">For a successful pagination of the data gotten</returns>
        /// <response code="200">Returns 200 and the paginated data according to the inputted page number and inputted page size</response>
        /// <response code="500">Returns 500 if a system error occurred while running the query</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SongsAndPageDetails))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet("list", Name = nameof(GetSongs))]
        public ActionResult<SongsAndPageDetails> GetSongs([BindRequired] int pageNumber, [BindRequired] int pageSize)
        {
            var result = _songDetails.GetSongs(pageNumber,pageSize);
            return Ok(result);
        }
    }
}
