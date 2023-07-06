using System.ComponentModel.DataAnnotations;

namespace MultiTracksWebApi.Requests
{
    public class ArtistRequest
    {
        [Required(ErrorMessage = "input field can not be empty"), MaxLength(30)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "only letters are required")]
        [DataType(DataType.Text)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "input field can not be empty"), MaxLength(1000)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "only letters are required")]
        [DataType(DataType.Text)]
        [Display(Name = "Biography")]
        public string Biography { get; set; }
        public IFormFile ArtistImage { get; set; }
        public IFormFile HeroImage { get; set; }
    }
}
