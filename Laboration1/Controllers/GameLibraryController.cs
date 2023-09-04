using Laboration1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;

namespace Laboration1.Controllers
{
    public class GameLibraryController : Controller
    {

        GameLibrary gameLibraryss = new GameLibrary();

        //List<Game> gameLibrary = new List<Game>();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult WelcomePage()
        {
            return View();
        }

        //[HttpGet]
        public IActionResult AddNewGame()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Confirmation(IFormCollection col)
        {
            Game game = new Game();
            game.GameName = col["GameName"];
            game.Platform = col["Platform"];
            if(col["PlayTime"] != string.Empty)
                game.PlayTime = Convert.ToInt32(col["PlayTime"]);
            
            if(col["Rating"] != string.Empty)
                game.Rating = Convert.ToInt32(col["Rating"]);

            game.Comment = col["Comment"];
            if(col["RegistrationDate"] != string.Empty)
                game.RegistrationDate = Convert.ToDateTime(col["RegistrationDate"]);

            gameLibraryss.AddGame(game);
            gameLibraryss.TestAdd();

            string s = JsonConvert.SerializeObject(game);
            //HttpContext.Session.SetString("gamesession", s);

            return View(game);

        }

        public IActionResult DeleteGame() 
        {
            return View();
        }

        public ActionResult Library()
        {
            ViewBag.Message = "Welcome to your library with games!";
            GameViewModel myModel = new GameViewModel();
            myModel.Games = gameLibraryss.GetGames();

            return View(myModel);  
        }
    }
}
