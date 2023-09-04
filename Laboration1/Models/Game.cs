using System.ComponentModel.DataAnnotations;

namespace Laboration1.Models
{
    public class Game
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide the Games Title")]
        [Display(Name = "Game Title")]
        public string GameName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide the Platform")]
        public string Platform { get; set; }

        public int? PlayTime { get; set; }

        [Range(1, 5)]
        public int? Rating { get; set; }

        public string? Comment { get; set; }

        [DataType(DataType.Date)]
        public DateTime? RegistrationDate { get; set; }


        public Game() { }
    }
}
