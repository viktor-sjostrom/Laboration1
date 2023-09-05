﻿using Laboration1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;

namespace Laboration1.Controllers
{
    public class GameLibraryController : Controller
    {

        private static List<Game> gameLibrary = new List<Game>();
        //private static int gameCounter = 1;

        [HttpGet]
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
            //game.gameId = gameCounter++;

            if(col["PlayTime"] != string.Empty)
                game.PlayTime = Convert.ToInt32(col["PlayTime"]);
            
            if(col["Rating"] != string.Empty)
                game.Rating = Convert.ToInt32(col["Rating"]);

            game.Comment = col["Comment"];
            if(col["RegistrationDate"] != string.Empty)
                game.RegistrationDate = Convert.ToDateTime(col["RegistrationDate"]);

            gameLibrary.Add(game);

            string s = JsonConvert.SerializeObject(game);
            HttpContext.Session.SetString("gamesession", s);

            return View(game);

        }

        public IActionResult DeleteGame() 
        {
            return View();
        }


        public IActionResult Edit(int id) 
        {
            //"Väljer" fel game - väljer sista
            string s = HttpContext.Session.GetString("gamesession");
            var editGame = gameLibrary.Where(g => g.gameId== id).FirstOrDefault();

            //var editGame = gameLibrary.FirstOrDefault(g => g.gameId== id);


            //Denna väljer alltid första objektet
            //Game game = gameLibrary.Find(g => g.gameId == id);


            if(editGame != null) {
                editGame = JsonConvert.DeserializeObject<Game>(s);
                s = JsonConvert.SerializeObject(editGame);
                HttpContext.Session.SetString("gamesession", s);
                return View(editGame);
            }

            return RedirectToAction("Library");
        }

        [HttpPost]
        public IActionResult Edit(Game eg) 
        {
            //Ändrar alltid de första objektet
            var game = gameLibrary.FirstOrDefault(g => g.gameId == eg.gameId);

            gameLibrary.Remove(game);
            gameLibrary.Add(eg);

            string s = HttpContext.Session.GetString("gamesession");
            game = JsonConvert.DeserializeObject<Game>(s);
            s = JsonConvert.SerializeObject(game);
            HttpContext.Session.SetString("gamesession", s);

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
