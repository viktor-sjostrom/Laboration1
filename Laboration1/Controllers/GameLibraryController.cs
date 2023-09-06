using Laboration1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;

namespace Laboration1.Controllers
{
    public class GameLibraryController : Controller
    {



        //Sessionsvariabel
        private const string GameLibrarySessionKey = "GameLibrary";
        private const string GameCounterSessionKey = "GameCounter";



        

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
            /*********/
            //Hämtar listan med spel - skapar ny om ingen finns
            var serializedGameLibrary = HttpContext.Session.GetString(GameLibrarySessionKey);
            List<Game> gameLibrary;
            if (!string.IsNullOrEmpty(serializedGameLibrary))
            {
                gameLibrary = JsonConvert.DeserializeObject<List<Game>>(serializedGameLibrary);
            } else
            {
                gameLibrary = new List<Game>();
            }
            //var gameLibrary = JsonConvert.DeserializeObject<List<Game>>(serializedGameLibrary) ?? new List<Game>();
            /*********/


            Game game = new Game();
            game.GameName = col["GameName"];
            game.Platform = col["Platform"];

            /*********/
            // Retrieve the game counter from the session
            var gameCounter = HttpContext.Session.GetInt32(GameCounterSessionKey) ?? 0;
            //game.gameId = gameCounter;

            //UTAN session variable
            game.gameId = ++gameCounter;
            /*********/


            if (col["PlayTime"] != string.Empty)
                game.PlayTime = Convert.ToInt32(col["PlayTime"]);
            
            if(col["Rating"] != string.Empty)
                game.Rating = Convert.ToInt32(col["Rating"]);

            game.Comment = col["Comment"];
            if(col["RegistrationDate"] != string.Empty)
                game.RegistrationDate = Convert.ToDateTime(col["RegistrationDate"]);



            /*********/
            // Update the session variables
            HttpContext.Session.SetInt32(GameCounterSessionKey, gameCounter);
            /*********/


            gameLibrary.Add(game);


            string s = JsonConvert.SerializeObject(game);
            HttpContext.Session.SetString("gamesession", s);

            /*********/
            // Serialize and update the session variable
            HttpContext.Session.SetString(GameLibrarySessionKey, JsonConvert.SerializeObject(gameLibrary));
            /*********/


            return View(game);

        }

        [HttpGet]
        public IActionResult Delete(int id) 
        {
            /*********/
            //Hämtar listan med spel - skapar ny om ingen finns
            var serializedGameLibrary = HttpContext.Session.GetString(GameLibrarySessionKey);
            List<Game> gameLibrary;
            if (!string.IsNullOrEmpty(serializedGameLibrary))
            {
                gameLibrary = JsonConvert.DeserializeObject<List<Game>>(serializedGameLibrary);
            }
            else
            {
                gameLibrary = new List<Game>();
            }
            //var gameLibrary = JsonConvert.DeserializeObject<List<Game>>(serializedGameLibrary) ?? new List<Game>();
            /*********/

            var deleteGame = gameLibrary.FirstOrDefault(g => g.gameId== id);

            if(deleteGame == null)
            {
                return NotFound();
            }

            return View(deleteGame);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteGameConfirmed(int id)
        {

            //Hämtar listan med spel - skapar ny om ingen finns
            var serializedGameLibrary = HttpContext.Session.GetString(GameLibrarySessionKey);
            List<Game> gameLibrary;
            if (!string.IsNullOrEmpty(serializedGameLibrary))
            {
                gameLibrary = JsonConvert.DeserializeObject<List<Game>>(serializedGameLibrary);
            }
            else
            {
                gameLibrary = new List<Game>();
            }


            var deleteGame = gameLibrary.FirstOrDefault(g => g.gameId == id);

            if (deleteGame == null)
            {
                return NotFound();
            }

            gameLibrary.Remove(deleteGame);


            /*********/
            // Update the session variables
            HttpContext.Session.SetInt32(GameCounterSessionKey, gameLibrary.Count);
            /*********/



            /*********/
            // Serialize and update the session variable
            HttpContext.Session.SetString(GameLibrarySessionKey, JsonConvert.SerializeObject(gameLibrary));
            /*********/

            return RedirectToAction("Library");
        }

        
        [HttpGet]
        public IActionResult Edit(int id) 
        {

            /*********/
            //Hämtar listan med spel - skapar ny om ingen finns
            var serializedGameLibrary = HttpContext.Session.GetString(GameLibrarySessionKey);
            List<Game> gameLibrary;
            if (!string.IsNullOrEmpty(serializedGameLibrary))
            {
                gameLibrary = JsonConvert.DeserializeObject<List<Game>>(serializedGameLibrary);
            }
            else
            {
                gameLibrary = new List<Game>();
            }
            /*********/

            var editGame = gameLibrary.FirstOrDefault(g => g.gameId== id);

            
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
                /*********/
                //Hämtar listan med spel - skapar ny om ingen finns
                var serializedGameLibrary = HttpContext.Session.GetString(GameLibrarySessionKey);
                List<Game> gameLibrary;
                if (!string.IsNullOrEmpty(serializedGameLibrary))
                {
                    gameLibrary = JsonConvert.DeserializeObject<List<Game>>(serializedGameLibrary);
                }
                else
                {
                    gameLibrary = new List<Game>();
                }
                /*********/

                if (gameLibrary.Any(g => g.gameId == editedGame.gameId))
                {
                    // Find the index of the game in gameLibrary by gameId
                    var index = gameLibrary.FindIndex(g => g.gameId == editedGame.gameId);

                    // Update the game in the list
                    gameLibrary[index] = editedGame;

                    /*
                    // Serialize the updated game list to store in the session
                    var serializedGameList = JsonConvert.SerializeObject(gameLibrary);
                    HttpContext.Session.SetString("gamesession", serializedGameList);
                    */


                    /*********/
                    // Serialize and update the session variable
                    HttpContext.Session.SetString(GameLibrarySessionKey, JsonConvert.SerializeObject(gameLibrary));
                    /*********/

                    return RedirectToAction("Library"); // Redirect to the "Library" page
                }
                else
                {
                    ModelState.AddModelError("gameId", "Game with the provided gameId does not exist.");
                }
                
            }

            // If the model is not valid, redisplay the edit view with validation errors. 

            return View(editedGame);

        } 


        
        [HttpGet]
        public IActionResult Library()
        {
            /*********/
            //Hämtar listan med spel - skapar ny om ingen finns
            var serializedGameLibrary = HttpContext.Session.GetString(GameLibrarySessionKey);
            List<Game> gameLibrary;
            if (!string.IsNullOrEmpty(serializedGameLibrary))
            {
                gameLibrary = JsonConvert.DeserializeObject<List<Game>>(serializedGameLibrary);
            }
            else
            {
                gameLibrary = new List<Game>();
            }
            /*********/

            ViewBag.Message = "Welcome to your library with games!";
            //ViewData["gameCounter"] = gameLibrary.Count;

            /*********/
            ViewData["gameCounter"] = HttpContext.Session.GetInt32(GameCounterSessionKey) ?? 0;
            /*********/


            GameViewModel myModel = new GameViewModel();
            myModel.Games = gameLibrary;

            return View(myModel);  
        } 

    }
}
