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

        public Game (string gameName, string platform, int? playTime, int? rating, string? comment, DateTime? registrationDate)
        {
            this.GameName = gameName;
            this.Platform = platform;
            this.PlayTime = playTime;
            this.Rating = rating;
            this.Comment = comment;
            this.RegistrationDate = registrationDate;
        }

        public Game(string gameName, string platform)
        {
            this.GameName = gameName;
            this.Platform = platform; 
        }
    }
}
