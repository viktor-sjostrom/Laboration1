using System.ComponentModel.DataAnnotations;

namespace Laboration1.Models
{
    public class GameLibrary
    {

        List<Game> GamesList;

        public GameLibrary() 
        {
            GamesList = new List<Game>();
        }

        public Boolean Validate(String check)
        {
            

            return true;
        }

        public void AddGame(Game game)
        {
            GamesList.Add(game);
        }

    }
}
