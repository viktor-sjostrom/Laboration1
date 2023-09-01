using System.ComponentModel.DataAnnotations;

namespace Laboration1.Models
{
    public class Game
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide the Games Title")]
        public string GameName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide the Platform")]
        public string Platform { get; set; }

        public int? PlayTime { get; set; }

        [Range(1, 5)]
        public int? Rating { get; set; }

        public string? Comment { get; set; }

        [DataType(DataType.Date)]
        public DateTime? RegistrationDate { get; set; }


        public Game(String name, String platform)
        {
            this.GameName = name;
            this.Platform = platform;
            this.RegistrationDate = DateTime.Now;
        }

        public Game(String name, String platform, DateTime date)
        {
            this.GameName = name;
            this.Platform = platform;
            this.RegistrationDate = date;
        }

        public Game() { }
    }
}
