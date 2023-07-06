using Microsoft.AspNetCore.Mvc;
using MultiTracksWebApi.Models;
using MultiTracksWebApi.Requests;
using MultiTracksWebApi.Services.Interface;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MultiTracksWebApi.Controllers
{
    [Route("api.multitracks.com/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistDetails _artistDetails;

        public ArtistController(IArtistDetails artistDetails)
        {
            _artistDetails = artistDetails;
        }

        /// <summary>
        /// Gets artist by name from the database
        /// </summary>
        /// <param name="name"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api.multitracks.com/artist/search?name=golden
        /// </remarks>
        /// <returns code="200">Artists that matches the input parameter</returns>
        /// <response code="200">Returns 200 and the the Artist type</response>
        /// <response code="500">Returns 500 if a system error occurred while running the query</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<Artist>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet("search", Name = nameof(Search))]
        public IActionResult Search(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Inputted value not in the right format");
            }
            var result = _artistDetails.GetArtistByName(name);
            return Ok(result);
        }


        /// <summary>
        /// Adds a new artist to the database
        /// </summary>
        /// <param name="request"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post /api.multitracks.com/artist/add
        /// </remarks>
        /// <returns code="200">Artists was created successfully</returns>
        /// <response code="200">Returns 200 and the number of rows affected by the operation</response>
        /// <response code="500">Returns 500 if a system error occurred while running the query</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(int))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost("add", Name = nameof(AddArtist))]
        public IActionResult AddArtist([FromForm] ArtistRequest request)
        {
            Dictionary<string, string> errorMessage = new Dictionary<string, string>();
            if (!ModelState.IsValid) 
            {
                foreach (var error in ModelState)
                {
                    errorMessage.Add(error.Key, error.Value.ToString() ?? string.Empty);
                }
                return BadRequest(errorMessage);
            }
            var result = _artistDetails.CreateArtist(request);
            return Ok(result);
        }
    }
}
