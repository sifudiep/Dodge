using System;
using System.Diagnostics;

namespace Dodge
{
    /// <summary>
    /// Spawner klassen spawnar NonPlayer objekt.
    /// </summary>
    public class Spawner
    {
        private Random _randomSpawn = new Random();

        private Stopwatch _enemySpawnTimer = new Stopwatch();
        private Stopwatch _pUSpawnTimer = new Stopwatch();
        private Stopwatch _scoreSpawnTimer = new Stopwatch();

        /// <summary>
        /// Check spawn kollar om NonPlayer objekt ska spawnas.
        /// </summary>
        public void CheckSpawn()
        {
            _enemySpawnTimer.Start();
            if (_enemySpawnTimer.ElapsedMilliseconds > Map.EnemySpawnSpeed)
            {
                Spawn("enemy");
                _enemySpawnTimer.Reset();
            }

            _pUSpawnTimer.Start();
            if (_pUSpawnTimer.ElapsedMilliseconds > Map.PUSpawnSpeed)
            {
                Spawn("pu");
                _pUSpawnTimer.Reset();
            }
            
            _scoreSpawnTimer.Start();
            if (_scoreSpawnTimer.ElapsedMilliseconds > Map.ScoreSpawnSpeed)
            {
                Spawn("score");
                _scoreSpawnTimer.Reset();
            }
        }

        /// <summary>
        /// Spawn funktionen används för att spawna NonPlayer objekt
        /// </summary>
        /// <param name="type">
        /// Vad för sorts subklass NonPlayer objektet är.
        /// </param>
        public void Spawn(string type)
        {
            switch (type)
            {
                case "enemy":
                    int enemyY = _randomSpawn.Next(0, Map.MaxSpawnY);
                    int EnemyType = _randomSpawn.Next(0, 18);
                    if (EnemyType == 1)
                    {
                        if (enemyY <= Map.MaxSpawnFiguresY)
                        {
                            SpawnEnemyOctagon(Map.WindowWidth-7, enemyY);
                        }
                        else
                        {
                            SpawnEnemyOctagon(Map.WindowWidth-7, Map.MaxSpawnY-enemyY);
                        }
                    }
                    else if (EnemyType == 2)
                    {
                        if (enemyY <= Map.MaxSpawnFiguresY)
                        {
                            SpawnEnemyLine(Map.WindowWidth-1, enemyY);
                        }
                        else
                        {
                            SpawnEnemyLine(Map.WindowWidth-1, Map.MaxSpawnY-enemyY);
                        }
                    }
                    else if (EnemyType == 3)
                    {
                        if (enemyY <= Map.MaxSpawnFiguresY)
                        {
                            SpawnEnemyHorizontalLine(Map.WindowWidth-7, enemyY);
                        }
                        else
                        {
                            SpawnEnemyHorizontalLine(Map.WindowWidth-7, Map.MaxSpawnY - enemyY);
                        }
                    }
                    else
                    {
                        GameContainer.NonPlayerList.Add(new Enemy(Map.StartX, enemyY));
                        Enemy.DrawSpawn(enemyY);
                    }
                    break;
                case "pu":
                    int puY = _randomSpawn.Next(0, Map.MaxSpawnY);
                    GameContainer.NonPlayerList.Add(new PU(Map.StartX, puY));
                    PU.DrawSpawn(puY);
                    break;
                case "score":
                    int scoreY = _randomSpawn.Next(0, Map.MaxSpawnY);
                    GameContainer.NonPlayerList.Add(new Score(Map.StartX, scoreY));
                    Score.DrawSpawn(scoreY);
                    break;
            }
        }

        /// <summary>
        /// Spawnar en enemy octagon.
        /// </summary>
        /// <param name="x">
        /// X koordinaten där enemy objektet ska spawnas.
        /// </param>
        /// <param name="y">
        /// Y koordinaten där enemy objektet ska spawnas.
        /// </param>
        public void SpawnEnemyOctagon(int x, int y)
        {
            SpawnDrawEnemy(x, y+2);
            SpawnDrawEnemy(x, y+3);

            SpawnDrawEnemy(x+1,y+1);
            SpawnDrawEnemy(x+1, y+4);

            SpawnDrawEnemy(x+2, y);
            SpawnDrawEnemy(x+2,y+5);

            SpawnDrawEnemy(x+3, y);
            SpawnDrawEnemy(x+3,y+5);

            SpawnDrawEnemy(x+4,y+1);
            SpawnDrawEnemy(x+4,y+4);

            SpawnDrawEnemy(x+5,y+2);
            SpawnDrawEnemy(x+5,y+3);
        }

        /// <summary>
        /// Spawnnar och ritar ut enemy objekt.
        /// </summary>
        /// <param name="x">
        /// Y koordinat där objektet ska ritas ut.
        /// </param>
        /// <param name="y">
        /// X koordinat där objektet ska ritas ut.
        /// </param>
        private void SpawnDrawEnemy(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write('*');
            GameContainer.NonPlayerList.Add(new Enemy(x, y));
        }

        /// <summary>
        /// SpawnEnemyLine spawnar en linje av enemy objekt.
        /// </summary>
        /// <param name="x">
        /// X koordinaten där första objektet ska spawnas.
        /// </param>
        /// <param name="y">
        /// Y koordinaten där första objektet ska spawnas.
        /// </param>
        public void SpawnEnemyLine(int x, int y)
        {
            for (int i = 0; i < 8; i++)
            {
                Console.SetCursorPosition(x, y+i);
                Console.Write('*');
                GameContainer.NonPlayerList.Add(new Enemy(x, y+i));
            }
        }

        /// <summary>
        /// Spawnar en sne linje av enemy objekt.
        /// </summary>
        /// <param name="x">
        /// X koordinaten där första objektet ska spawnas.
        /// </param>
        /// <param name="y">
        /// Y koordinaten där första objektet ska spawnas.
        /// </param>
        public void SpawnEnemyHorizontalLine(int x, int y)
        {
            for (int i = 0; i < 6; i++)
            {
                Console.SetCursorPosition(x + i, y + i);
                Console.Write('*');
                GameContainer.NonPlayerList.Add(new Enemy(x+i, y+i));
            }
        }
    }
}