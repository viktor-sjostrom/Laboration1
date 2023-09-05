﻿using Laboration1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;

namespace Laboration1.Controllers
{
    public class GameLibraryController : Controller
    {

        private static List<Game> gameLibrary = new List<Game>();
        private static int gameCounter = 1;

        //Sessionsvariabel

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
            game.gameId = gameCounter++;

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


        [HttpGet]
        public IActionResult Edit(int id) 
        {

            var editGame = gameLibrary.FirstOrDefault(g => g.gameId== id);


            //Denna väljer alltid första objektet
            //Game game = gameLibrary.Find(g => g.gameId == id);


            if(editGame != null) {
                return View(editGame);
            }

            return RedirectToAction("Library");
        }

        [HttpPost]
        public IActionResult Edit(Game editedGame) 
        {
            if (ModelState.IsValid)
            {
                /*
                // Find the game in gameLibrary by gameId
                var existingGame = gameLibrary.FirstOrDefault(g => g.gameId == editedGame.gameId);

                if (existingGame != null)
                {
                    // Update the existing game's properties
                    existingGame.GameName = editedGame.GameName;
                    existingGame.Platform = editedGame.Platform;
                    existingGame.PlayTime = editedGame.PlayTime;
                    existingGame.Rating = editedGame.Rating;
                    existingGame.Comment = editedGame.Comment;
                    existingGame.RegistrationDate = editedGame.RegistrationDate;

                    // Serialize the updated game list to store in the session
                    var serializedGameList = JsonConvert.SerializeObject(gameLibrary);
                    HttpContext.Session.SetString("gamesession", serializedGameList);

                    return RedirectToAction("Library");
                }*/

                if (gameLibrary.Any(g => g.gameId == editedGame.gameId))
                {
                    // Find the index of the game in gameLibrary by gameId
                    var index = gameLibrary.FindIndex(g => g.gameId == editedGame.gameId);

                    // Update the game in the list
                    gameLibrary[index] = editedGame;

                    // Serialize the updated game list to store in the session
                    var serializedGameList = JsonConvert.SerializeObject(gameLibrary);
                    HttpContext.Session.SetString("gamesession", serializedGameList);

                    return RedirectToAction("Library"); // Redirect to the "Library" page
                }
                else
                {
                    ModelState.AddModelError("gameId", "Game with the provided gameId does not exist.");
                }
            }

            // If the model is not valid, redisplay the edit view with validation errors.
            return View(editedGame);

            /*
            var game = gameLibrary.FirstOrDefault(g => g.gameId == eg.gameId);

            gameLibrary.Remove(game);
            gameLibrary.Add(eg);

            string s = HttpContext.Session.GetString("gamesession");
            game = JsonConvert.DeserializeObject<Game>(s);
            s = JsonConvert.SerializeObject(game);
            HttpContext.Session.SetString("gamesession", s);

            
            return RedirectToAction("Library");
            */
            //return View(eg);
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
