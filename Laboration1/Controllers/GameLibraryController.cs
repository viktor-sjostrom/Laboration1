using Laboration1.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Laboration1.Controllers
{
    public class GameLibraryController : Controller
    {

        GameLibrary gameLibrary = new GameLibrary();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult WelcomePage()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddNewGame(IFormCollection col)
        {
            Game game = new Game();
            game.GameName = col["GameName"];
            game.Platform = col["Platform"];
            game.PlayTime = Convert.ToInt32(col["PlayTime"]);
            game.Rating = Convert.ToInt32(col["Rating"]);
            game.Comment = col["Comment"];
            game.RegistrationDate = Convert.ToDateTime(col["RegistrationDate"]);

            gameLibrary.AddGame(game);

            string s = JsonConvert.SerializeObject(game);
            //HttpContext.Session.SetString("session", s);

            return View(game);
        }
    }
}
