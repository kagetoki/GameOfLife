using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Game;

namespace TestConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var game = new ConwayGame())
            {
                game.Field[0, 0].IsAlive = true;
                game.Field[0, 1].IsAlive = true;
                game.Field[1, 0].IsAlive = true;
                game.Field[1, 1].IsAlive = true;

                //game.GenerationChanged += (s, e) =>
                //{
                //    for (int x = 0; x < e.Field.Width; x++)
                //    {
                //        for (int y = 0; y < e.Field.Height; y++)
                //        {
                //            if (game.Field[x, y].IsAlive)
                //            {
                //                Console.WriteLine($"({x}, {y})");
                //            }
                //        }
                //    }
                //    Console.WriteLine("---------");
                //};
                game.Start();
                Console.ReadLine();
            }
        }
    }
}
