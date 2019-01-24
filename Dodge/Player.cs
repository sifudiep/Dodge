using System;
using System.Security.Principal;
using System.Xml;

namespace Dodge
{
    /// <summary>
    /// Player klassen är vad spelaren kontrollerar via tangentklick.
    /// </summary>
    public class Player
    {
        public static int X = 0;
        public static int Y = Console.WindowHeight / 2;

        /// <summary>
        /// Move tilldelar nya koordinater för player objektet
        /// </summary>
        /// <param name="x">
        /// X koordinaten som bestämmer om player objektet ska röras frammåt eller bakåt
        /// </param>
        /// <param name="y">
        /// Y koordinaten som bestämmer om player objektet ska röras uppåt eller nedåt
        /// </param>
        public void Move(int x, int y)
        {
            var newX = X + x;
            var newY = Y + y;

            if (CheckMove(newX, newY))
            {
                Remove(X, Y);
                Draw(newX, newY);
                X = newX;
                Y = newY;
            }
        }

        /// <summary>
        /// CheckMove kollar om player objektet rör sig utanför kartan eller om den kolliderar med ett NonPlayer objekt.
        /// </summary>
        /// <param name="x">
        /// X koordinaten som player objektet står på.
        /// </param>
        /// <param name="y">
        /// Y koordinaten som player objektet står på.
        /// </param>
        /// <returns>
        /// Returnar true då player objektet inte kolliderar med NonPlayer objekt eller går utanför kartan, returnerar false då något om de tidigare kraven har hänt.
        /// </returns>
        public bool CheckMove(int x, int y)
        {
            if (x < 0 ||x >= Console.WindowWidth-1)
            {
                return false;
            }

            if (y < 0 || y >= Console.WindowHeight-2)
            {
                return false;
            }

            for (int i = 0; i < GameContainer.NonPlayerList.Count; i++)
            {
                var nonPlayer = GameContainer.NonPlayerList[i];                
                if (nonPlayer.FetchX() == Player.X && nonPlayer.FetchY() == Player.Y)
                {
                    switch (nonPlayer.FetchEntity())
                    {
                        case "enemy":
                            if (Map.GodMode == false)
                                GameContainer.EndGame();
                            else 
                                GameContainer.NonPlayerList.RemoveAt(i);
                            break;
                        case "pu":
                            GameContainer.PU.GainPU();
                            GameContainer.NonPlayerList.RemoveAt(i);
                            return true;
                        case "score":
                            Map.UpdateScore();
                            GameContainer.NonPlayerList.RemoveAt(i);
                            return true;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Remove funktionen suddar ut player objektet på kartan.
        /// </summary>
        /// <param name="x">
        /// X koordinaterna där player objektet ska suddas ut på.
        /// </param>
        /// <param name="y">
        /// Y koordinaterna där player objektet ska suddas ut på.
        /// </param>
        public void Remove(int x, int y)
        {
            Console.BackgroundColor = Map.BackGroundColor;
            Console.SetCursorPosition(x, y);
            Console.Write(" ");
        }

        /// <summary>
        /// Draw ritar ut player objektet på kartan
        /// </summary>
        /// <param name="x">
        /// X koordinaten där player objektet ska ritas ut på.
        /// </param>
        /// <param name="y">
        /// Y koordinaten där player objektet ska ritas ut på.
        /// </param>
        public void Draw(int x, int y)
        {
            Console.BackgroundColor = Map.PlayerColor;
            Console.SetCursorPosition(x, y);
            Console.Write("P");
        }

    }
}