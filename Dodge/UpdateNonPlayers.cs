using System;
using System.Diagnostics;

namespace Dodge
{
    /// <summary>
    /// Gör att NonPlayer objekt rör sig frammåt.
    /// </summary>
    public class UpdateNonPlayers
    {
        private Stopwatch _updateTimer = new Stopwatch();

        /// <summary>
        /// Uppdaterar NonPlayer objekt genom att sudda ut de, skriva ut de, och uppdatera koordinaterna.
        /// </summary>
        public void Update()
        {
            _updateTimer.Start();
            if (Map.SlowTime == true)
            {
                Map.MoveSpeed = Map.MoveSpeed + 200;
                Map.SlowTime = false;
            }
            if (_updateTimer.ElapsedMilliseconds > Map.MoveSpeed)
            {
                for (int i = 0; i < GameContainer.NonPlayerList.Count; i++)
                {
                    var nonPlayer = GameContainer.NonPlayerList[i];
                    if (nonPlayer.CheckCollision(nonPlayer.FetchX(), nonPlayer.FetchY()))
                    {
                        nonPlayer.Remove(nonPlayer.FetchX(), nonPlayer.FetchY());
                        nonPlayer.Draw(nonPlayer.FetchX(), nonPlayer.FetchY());
                        _updateTimer.Reset();
                    }
                    else
                    {
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
                                    break;
                                case "score":
                                    Map.UpdateScore();
                                    GameContainer.NonPlayerList.RemoveAt(i);
                                    break;
                            }
                        }
                        else if (nonPlayer.FetchX() == 0)
                        {
                            GameContainer.NonPlayerList.RemoveAt(i);
                            nonPlayer.Remove(nonPlayer.FetchX(), nonPlayer.FetchY());
                            i--;
                        }
                    }
                }
            }
        }
    }
}