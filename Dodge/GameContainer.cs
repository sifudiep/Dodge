using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Dodge
{
    /// <summary>
    /// GameContainern används för att "wrappa" alla spelobjekt, här samlas data för viktiga objekt som NonPlayer, Player och Map.
    /// </summary>
    public class GameContainer
    {
        private Player _player;
        private bool _changeSpeed = true;
        private readonly Stopwatch _progressionTimer = new Stopwatch();

        public static PU PU = new PU(0, 0);
        public static Stopwatch PlayTime = new Stopwatch();
        public static bool Running = true;
        public static List<NonPlayer> NonPlayerList = new List<NonPlayer>();

        /// <summary>
        /// Initializes a new instance of the <see cref="GameContainer"/> class.
        /// Används för att vi ska kunna använda ostatiska metoder och fält i Player, anledningen till varför vi inte använder statiska metoder är pga jag ville ha möjligheten för flera spelare.
        /// </summary>
        /// <param name="player">
        /// Skapa ny instance av player för att få tillgång till dess metoder och koordinater
        /// </param>
        public GameContainer(Player player)
        {
            _player = player;
        }

        private ConsoleKeyInfo keyInfo;

        /// <summary>
        /// The handle input används för att tillåta spelaren att röra sig samt att stänga av spelet.
        /// </summary>
        public void HandleInput()
        {
            if (Console.KeyAvailable)
            {
                if ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Escape)
                {
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:
                            _player.Move(0, -1);
                            break;

                        case ConsoleKey.DownArrow:
                            _player.Move(0, 1);
                            break;

                        case ConsoleKey.LeftArrow:
                            _player.Move(-1, 0);
                            break;

                        case ConsoleKey.RightArrow:
                            _player.Move(1, 0);
                            break;
                    }
                }
                else
                {
                    Environment.Exit(0);
                }
            }
        }

        /// <summary>
        /// CheckProgression används för att öka svårighetsgraden i spelet beroende på hur lång tid det har gått. Den gör detta genom att ändra MoveSpeed och SpawnSpeed för NonPlayer objekten.  
        /// </summary>
        public void CheckProgression()
        {
            _progressionTimer.Start();
            if (_progressionTimer.ElapsedMilliseconds > Map.TimeIncreaseSpeed)
            {
                if (_changeSpeed)
                {
                    if (Map.MoveSpeed > Map.MaxSpeed)
                    {
                        Map.MoveSpeed = Map.MoveSpeed - Map.IncreaseSpeed;
                    }
                    else if (Map.EnemySpawnSpeed > Map.MaxSpeed)
                    {
                        Map.EnemySpawnSpeed = Map.EnemySpawnSpeed - Map.IncreaseSpeed;
                    }
                    _changeSpeed = false;
                }
                else
                {
                    if (Map.EnemySpawnSpeed > Map.MaxSpeed)
                    {
                        Map.EnemySpawnSpeed = Map.EnemySpawnSpeed - Map.IncreaseSpeed;
                    }
                    else if (Map.MoveSpeed > Map.MaxSpeed)
                    {
                        Map.MoveSpeed = Map.MoveSpeed - Map.IncreaseSpeed;
                    }
                    _changeSpeed = true;
                }
                _progressionTimer.Reset();
            }
        }

        /// <summary>
        /// The save stats.
        /// SaveStats skriver poäng, namn osv till en textfil, kan liknas till ett scoreboard. 
        /// </summary>
        /// <param name="Name">
        /// Namnet på spelaren som ska sparas till textfil
        /// </param>
        public static void SaveStats(string Name)
        {
            var path = @"C:\Users\mikael.diep\Desktop\Dodge-2018-04-24\Stats\stats.txt";

            var name = Name;
            var score = Map.Score;
            var time = PlayTime.ElapsedMilliseconds;
            var date = DateTime.Now;

            string statsLog = Environment.NewLine + String.Format("NAME OF PLAYER : {0}, SCORE : {1}, TIME PLAYED : {2}, PLAYED ON THE : {3}", name, score, time, date);

            File.AppendAllText(path, statsLog);
        }

        /// <summary>
        /// The end game.
        /// EndGame körs när Player och ett enemy objekt krockar, EndGame funktionen ser till att spelet avslutas samt att 
        /// spelaren för möjligheten att spara sina stats i en textfil med hjälp av SaveStats funktionen.
        /// </summary>
        public static void EndGame()
        {
            Running = false;
            Console.Clear();

            Console.WriteLine("You dead");
            Console.WriteLine("Type your name to save game or type 'quit' to exit the game");

            var name = Console.ReadLine();

            if (name == "quit")
            {
                Environment.Exit(0);
            }
            else
            {
                SaveStats(name);
                Environment.Exit(0);
            }
        }
    }
}