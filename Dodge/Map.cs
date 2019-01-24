using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Timers;

namespace Dodge
{
    /// <summary>
    /// The map.
    /// Här sparas framförallt spelsettings, map data och uppdatering av mappen.
    /// </summary>
    public class Map
    {
        public static int StartX = 59;
        public static int WindowWidth = 60;
        public static int WindowHeight = 30;

        public static int MaxSpawnY = 28;
        public static int MaxSpawnFiguresY = 20;

        public static ConsoleColor BackGroundColor = ConsoleColor.Black;
        public static ConsoleColor PlayerColor = ConsoleColor.Blue;
        public static ConsoleColor EnemyColor = ConsoleColor.Red;
        public static ConsoleColor ScoreColor = ConsoleColor.Yellow;
        public static ConsoleColor PowerUpColor = ConsoleColor.Cyan;
        public static ConsoleColor TimeColor = ConsoleColor.DarkGreen;

        public static int MaxSpeed = 100;
        public static int IncreaseSpeed = 100;
        public static int TimeIncreaseSpeed = 10000;

        public static int Score = 0;
        public static int AddScore = 1000;

        public static int PUDuration = 5;
        public static bool GodMode = false;
        public static bool SlowTime = false;

        public static int MoveSpeed = 500;
        public static int EnemySpawnSpeed = 1000;
        public static int PUSpawnSpeed = 8000;
        public static int ScoreSpawnSpeed = 2000;

        /// <summary>
        /// The update score.
        /// UpdateScore ökar och uppdaterar score på kartan
        /// </summary>
        public static void UpdateScore()
        {
            Console.BackgroundColor = Map.ScoreColor;
            Console.SetCursorPosition(0, 28);
            Score = Score + Map.AddScore;
            Console.Write(" ");
            Console.Write("SCORE : " + Score);
        }

        /// <summary>
        /// The update pu.
        /// UpdatePU Skriver ut PU på kartan
        /// </summary>
        /// <param name="PU">
        /// Eftersom att vi har två olika Power ups så använder vi en string parameter för att kunna få två eller mera utfall.
        /// </param>
        public static void UpdatePU(string PU)
        {
            Console.BackgroundColor = PowerUpColor;
            Console.SetCursorPosition(15, 28);
            Console.Write(PU);
        }

        /// <summary>
        /// The remove pu text.
        /// RemovePUText används för att ta bort powerup texten från kartan.
        /// </summary>
        public static void RemovePUText()
        {
            Console.BackgroundColor = BackGroundColor;
            Console.SetCursorPosition(15, 28);
            Console.WriteLine("                  ");
        }

        /// <summary>
        /// The update play time.
        /// UpdatePlayTime används för att skriva ut hur många millisekunder som spelaren har spelat i 
        /// </summary>
        public static void UpdatePlayTime()
        {
            Console.BackgroundColor = TimeColor;
            Console.SetCursorPosition(50, 28);
            Console.Write(GameContainer.PlayTime.ElapsedMilliseconds + "ms");
        }
    }
}