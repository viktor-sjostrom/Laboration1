using Laboration1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;

namespace Laboration1.Controllers
{
    public class GameLibraryController : Controller
    {

        private static List<Game> gameLibrary = new List<Game>();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult WelcomePage()
        {
            return View();
        }

        [HttpGet]
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

            gameLibrary.Add(game);

            string s = JsonConvert.SerializeObject(game);
            //HttpContext.Session.SetString("gamesession", s);

            return View(game);

        }

        public IActionResult DeleteGame() 
        {
            return View();
        }

        public IActionResult Edit(int id) 
        { 
            var editGame = gameLibrary.Where(g => g.gameId== id).FirstOrDefault();

            if(editGame != null)
                return View(editGame);

            return View();
        }

        [HttpPost]
        public IActionResult Edit(Game eg) 
        {
            var game = gameLibrary.Where(g => g.gameId == eg.gameId).FirstOrDefault();
            gameLibrary.Remove(game);
            gameLibrary.Add(eg);

            return RedirectToAction("Library");
        }

        [HttpGet]
        public IActionResult Library()
        {
            ViewBag.Message = "Welcome to your library with games!";
            GameViewModel myModel = new GameViewModel();
            myModel.Games = gameLibrary;

            return View(myModel);  
        }
    }
}
