using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Dodge
{
    /// <summary>
    /// The non player.
    /// NonPlayer är en abstract klass som Enemy, Score och PowerUp deriverar från, dess 
    /// funktion är att tillåta oss uppna polymorphism, dvs att vi kan loopa igenom enemy, score och powerup för att sedan kalla på deras metoder
    /// </summary>
    public abstract class NonPlayer
    {
        public abstract void Draw(int x, int y);
        public abstract void Remove(int x, int y);
        public abstract bool CheckCollision(int x, int y);
        public abstract int FetchX();
        public abstract int FetchY();
        public abstract string FetchEntity();
    }

    /// <summary>
    /// The enemy.
    /// Enemy classens primära användning är att tillåta oss skapa flera enemy objekt på kartan.
    /// Enemyobjekt skiljer sig åt från de andra subklasserna på det sättet att man förlorar spelet då spelaren kolliderar med ett enemy objekt.
    /// </summary>
    public class Enemy : NonPlayer
    {
        /// <summary>
        /// Enemy konstrukturn används för att initialisera dess koordinater
        /// </summary>
        /// <param name="X">
        /// Objektets X koordinat på kartan
        /// </param>
        /// <param name="Y">
        /// Objektets Y koordinat på kartan
        /// </param>
        public Enemy(int X, int Y)
        {
            _x = X;
            _y = Y;
        }

        private int _x;
        private int _y;

        /// <summary>
        /// The draw spawn.
        /// DrawSpawn ritar ut enemy objektet på kartan
        /// </summary>
        /// <param name="y">
        /// Eftersom att endast Y koordinaten varieras för enemyobjektets funktion så behöver vi en parameter för det. 
        /// </param>
        public static void DrawSpawn(int y)
        {
            Console.BackgroundColor = Map.EnemyColor;
            Console.SetCursorPosition(Map.StartX-1, y);
            Console.Write("¤");
        }

        /// <summary>
        /// The draw.
        /// Draw funktionen ritar ut enemy objektet på specifika x och y koordinater.
        /// </summary>
        /// <param name="x">
        /// X koordinaten där objektet befinner sig
        /// </param>
        /// <param name="y">
        /// Y Koordinaten där objektet befinner sig
        /// </param>
        public override void Draw(int x, int y)
        {
            x--;
            Console.SetCursorPosition(x, y);
            Console.BackgroundColor = Map.EnemyColor;
            Console.Write("¤");
            _x = x;
        }

        /// <summary>
        /// The remove.
        /// Remove funktionen suddar ut enemy objektet från kartan.
        /// </summary>
        /// <param name="x">
        /// X koordinaten där enemy objektet befinner sig.
        /// </param>
        /// <param name="y">
        /// Y koordinaten där enemy objektet befinner sig.
        /// </param>
        public override void Remove(int x, int y)
        {
            Console.BackgroundColor = Map.BackGroundColor;
            Console.SetCursorPosition(x, y);
            Console.Write(" ");
        }

        /// <summary>
        /// The check collision.
        /// CheckCollision funktionen kollar av om enemy objektets koordinater matchar spelaren koordinater.
        /// </summary>
        /// <param name="x">
        /// X Koordinaten som enemyobjektet befinner sig i
        /// </param>
        /// <param name="y">
        /// Y koordinaten som enemyobjektet befinner sig i 
        /// </param>
        /// <returns>
        /// Beroende på om funktionen returnerar true så förlorar spelaren.
        /// </returns>
        public override bool CheckCollision(int x, int y)
        {
            if (x - 1 >= 0)
            {
                if (x++ == Player.X && y == Player.Y)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// The fetch x.
        /// Används för att få ta på objektets x koordinat.
        /// </summary>
        /// <returns>
        /// Returnerar objektets x koordinat. 
        /// </returns>
        public override int FetchX()
        {
            return _x;
        }

        /// <summary>
        /// The fetch y.
        /// Används för att få tag på objektets y koordinat.
        /// </summary>
        /// <returns>
        /// Returnar objektets y koordinat.
        /// </returns>
        public override int FetchY()
        {
            return _y;
        }

        /// <summary>
        /// The fetch entity.
        /// Används för att få reda på vad för subobjekt som det är.
        /// </summary>
        /// <returns>
        /// Returnerar string som berättar vad det är för objekt.
        /// </returns>
        public override string FetchEntity()
        {
            return "enemy";
        }
    }

    /// <summary>
    /// The pu.
    /// PU classen ärver metoder från NonPlayer klassen, det som skiljer PUs metoder är att metoderna använder specifika bakgrundfärger och symboler för PU
    /// </summary>
    public class PU : NonPlayer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PU"/> class.
        /// Initializera X och Y koordinaterna för PU.
        /// </summary>
        /// <param name="x">
        /// X koordinaten för PU objektet
        /// </param>
        /// <param name="y">
        /// Y Koordinaten för PU objektet
        /// </param>
        public PU(int x, int y)
        {
            _x = x;
            _y = y;
        }

        private int _x;
        private int _y;

        /// <summary>
        /// DrawSpawn ritar ut PU objektet på kartan vid startpunkten för x, y koordinaten kan dock variera då den slump genereras.
        /// </summary>
        /// <param name="y">
        /// Y koordinaten där objektet ska ritas
        /// </param>
        public static void DrawSpawn(int y)
        {
            Console.SetCursorPosition(Map.StartX, y);
            Console.BackgroundColor = Map.PowerUpColor;
            Console.Write("P");
        }

        /// <summary>
        /// Draw ritar ut PU objektet på kartan vid specifika koordinater.
        /// </summary>
        /// <param name="x">
        /// X koordinaten där objektet ska ritas ut till
        /// </param>
        /// <param name="y">
        /// Y koordinaten där objektet ska ritas ut till
        /// </param>
        public override void Draw(int x, int y)
        {
            x--;
            Console.BackgroundColor = Map.PowerUpColor;
            Console.SetCursorPosition(x, y);
            Console.Write("P");
            _x = x;
        }

        /// <summary>
        /// Remove suddar ut objektet från kartan vid specifika koordinater.
        /// </summary>
        /// <param name="x">
        /// X koordinaten där objektet ska ritas ut till
        /// </param>
        /// <param name="y">
        /// Y koordinaten där objektet ska ritas ut till
        /// </param>
        public override void Remove(int x, int y)
        {
            Console.BackgroundColor = Map.BackGroundColor;
            Console.SetCursorPosition(x, y);
            Console.Write(" ");
        }

        /// <summary>
        /// CheckCollision används för att kolla om PU objektet har kolliderat med spelaren eller om den åkt utanför kartan.
        /// </summary>
        /// <param name="x">
        /// X koordinaten där objektet befinnner sig på.
        /// </param>
        /// <param name="y">
        /// Y koordinaten där objektet befinner sig på.
        /// </param>
        /// <returns>
        /// Returnerar true då PU inte kolliderar med player, vid kollidering så returnerar metoden false.
        /// </returns>
        public override bool CheckCollision(int x, int y)
        {
            if (x - 1 >= 0)
            {
                if (x++ == Player.X && y == Player.Y)
                {
                    return false;
                }
                return true;
            }

            return false;
        }

        /// <summary>
        /// The fetch x.
        /// Används för att få ta på objektets x koordinat.
        /// </summary>
        /// <returns>
        /// Returnerar objektets x koordinat. 
        /// </returns>
        public override int FetchX()
        {
            return _x;
        }

        /// <summary>
        /// The fetch y.
        /// Används för att få tag på objektets y koordinat.
        /// </summary>
        /// <returns>
        /// Returnar objektets y koordinat.
        /// </returns>
        public override int FetchY()
        {
            return _y;
        }

        /// <summary>
        /// The fetch entity.
        /// Används för att få reda på vad för subobjekt som det är.
        /// </summary>
        /// <returns>
        /// Returnerar string som berättar vad det är för objekt.
        /// </returns>
        public override string FetchEntity()
        {
            return "pu";
        }

        private Random rndPU = new Random();

        /// <summary>
        /// Blinkar texten för att signalera att PU perioden snart är över.
        /// </summary>
        /// <param name="text">
        /// Text är vilken text som ska blinkas, i våra fall så är det vilket powerup namn som ska blinkas.
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        public async Task EndSignal(string text)
        {
            await Task.Delay(TimeSpan.FromSeconds(4));
            Map.RemovePUText();
            await Task.Delay(TimeSpan.FromMilliseconds(200));
            Map.UpdatePU(text);
            await Task.Delay(TimeSpan.FromMilliseconds(200));
            Map.RemovePUText();
            await Task.Delay(TimeSpan.FromMilliseconds(200));
            Map.UpdatePU(text);
            await Task.Delay(TimeSpan.FromMilliseconds(200));
            Map.RemovePUText();
            await Task.Delay(TimeSpan.FromMilliseconds(200));
        }

        /// <summary>
        /// GainPU används för att rita ut power up namnet, signalera avslutning av pu och sudda pu texten.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task GainPU()
        {
            switch (rndPU.Next(0,2))
            {
                case 0:
                    Map.UpdatePU("God Mode");
                    Map.GodMode = true;
                    await EndSignal("God Mode");
                    Map.UpdatePU("God Mode");
                    Map.GodMode = false;
                    Map.RemovePUText();
                    break;

                case 1:
                    Map.UpdatePU("Slow Time");
                    Map.SlowTime = true;
                    await EndSignal("Slow Time");
                    Map.UpdatePU("Slow Time");
                    Map.SlowTime = false;
                    Map.RemovePUText();
                    break;

                case 2:
                    Map.UpdatePU("Cleared Board!");
                    for (int i = 0; i < GameContainer.NonPlayerList.Count; i++)
                    {
                        var nonPlayer = GameContainer.NonPlayerList[i];
                        nonPlayer.Remove(nonPlayer.FetchX(), nonPlayer.FetchY());
                        GameContainer.NonPlayerList.RemoveAt(i);
                    }
                    break;
            }
        }
    }

    /// <summary>
    /// Score används för att skapa objekt som ger poäng till spelaren när de kolliderar. 
    /// </summary>
    public class Score : NonPlayer
    {
        /// <summary>
        /// Initializerar ett nytt Score objekt med X och Y koordinater
        /// </summary>
        /// <param name="x">
        /// Objektets X koordinat
        /// </param>
        /// <param name="y">
        /// Objektets Y koordinat
        /// </param>
        public Score(int x, int y)
        {
            _x = x;
            _y = y;
        }

        private int _x;
        private int _y;

        /// <summary>
        /// DrawSpawn ritar ut Score objektet på kartan vid startpunkten för x, y koordinaten kan dock variera då den slump genereras.
        /// </summary>
        /// <param name="y">
        /// Y koordinaten där objektet ska ritas
        /// </param>
        public static void DrawSpawn(int y)
        {
            Console.BackgroundColor = Map.ScoreColor;
            Console.SetCursorPosition(Map.StartX, y);
            Console.Write("$");
        }

        /// <summary>
        /// The draw.
        /// Draw funktionen ritar ut score objektet på specifika x och y koordinater.
        /// </summary>
        /// <param name="x">
        /// X koordinaten där objektet befinner sig
        /// </param>
        /// <param name="y">
        /// Y Koordinaten där objektet befinner sig
        /// </param>
        public override void Draw(int x, int y)
        {
            x--;
            Console.SetCursorPosition(x, y);
            Console.BackgroundColor = Map.ScoreColor;
            Console.Write("$");
            _x = x;
        }

        /// <summary>
        /// The remove.
        /// Remove funktionen suddar ut enemy objektet från kartan.
        /// </summary>
        /// <param name="x">
        /// X koordinaten där enemy objektet befinner sig.
        /// </param>
        /// <param name="y">
        /// Y koordinaten där enemy objektet befinner sig.
        /// </param>
        public override void Remove(int x, int y)
        {
            Console.BackgroundColor = Map.BackGroundColor;
            Console.SetCursorPosition(x, y);
            Console.Write(" ");
        }

        /// <summary>
        /// The check collision.
        /// CheckCollision funktionen kollar av om score objektets koordinater matchar spelaren koordinater.
        /// </summary>
        /// <param name="x">
        /// X Koordinaten som enemyobjektet befinner sig i
        /// </param>
        /// <param name="y">
        /// Y koordinaten som enemyobjektet befinner sig i 
        /// </param>
        /// <returns>
        /// Beroende på om funktionen returnerar true så förlorar spelaren.
        /// </returns>
        public override bool CheckCollision(int x, int y)
        {
            if (x - 1 >= 0)
            {
                if (x++ == Player.X && y == Player.Y)
                {
                    return false;
                }
                return true;
            }

            return false;
        }

        /// <summary>
        /// The fetch x.
        /// Används för att få ta på objektets x koordinat.
        /// </summary>
        /// <returns>
        /// Returnerar objektets x koordinat. 
        /// </returns>
        public override int FetchX()
        {
            return _x;
        }

        /// <summary>
        /// The fetch y.
        /// Används för att få tag på objektets y koordinat.
        /// </summary>
        /// <returns>
        /// Returnar objektets y koordinat.
        /// </returns>
        public override int FetchY()
        {
            return _y;
        }

        /// <summary>
        /// The fetch entity.
        /// Används för att få reda på vad för subobjekt som det är.
        /// </summary>
        /// <returns>
        /// Returnerar string som berättar vad det är för objekt.
        /// </returns>
        public override string FetchEntity()
        {
            return "score";
        }
    }
}