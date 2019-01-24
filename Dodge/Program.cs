using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dodge
{
    /// <summary>
    /// Spel som går ut på att samla poäng och inte kollidera med enemy objekt.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main funktionen skapar objekt av klasser och använder en while loop för att konstant uppdatera programmet och ta emot data från t.ex tangentklick.
        /// </summary>
        /// <param name="args">
        /// Args argumentet används inte
        /// </param>
        static void Main(string[] args)
        {
            GameContainer Gc = new GameContainer(new Player());
            Spawner Spawner = new Spawner();
            UpdateNonPlayers Updater = new UpdateNonPlayers();
            InitGame();

            while (GameContainer.Running)
            {
                Gc.HandleInput();
                Spawner.CheckSpawn();
                Updater.Update();
                Gc.CheckProgression();
                Map.UpdatePlayTime();
            }
        }

        /// <summary>
        /// InitGame används för att ge värden till kart värden.
        /// </summary>
        public static void InitGame()
        {
            Console.BufferWidth = Map.WindowWidth;
            Console.BufferHeight = Map.WindowHeight;
            Console.WindowWidth = Map.WindowWidth;
            Console.WindowHeight = Map.WindowHeight;

            Map.UpdateScore();
            Map.UpdatePU("");

            GameContainer.PlayTime.Start();
        }
    }
}
