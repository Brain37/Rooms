using System;
using System.Media;

namespace Rooms
{
    class Program
    {
        public static void Main(string[] args)
        {
            //SoundPlayer soundPlayer = new SoundPlayer(@"C:\Users\Brian Emo\Documents\Visual Studio 2015\Projects\Rooms\Rooms\Sounds\evilLaugh.wav");

            Console.WriteLine("Do you know how to play?");
            if(YesOrNo())
            {
                Core.TheMap = new Map(GetName(), GetDifficulty());
            }
            else
            {
                Console.WriteLine("Options will be presented to you.");
                Console.WriteLine("Please enter the number of your response and then hit enter");
                Console.WriteLine("The goal of the game is to reach the final room alive.");
                Console.WriteLine("Press Enter when you are ready to start the game.");
                Console.ReadLine();
                Core.TheMap = new Map(GetName(), GetDifficulty());
            }
        }

        public static bool YesOrNo()
        {
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");
            string response = Console.ReadLine();
            return string.Equals(response, "yes", StringComparison.OrdinalIgnoreCase) || string.Equals(response, "1", StringComparison.OrdinalIgnoreCase);
        }

        public static string GetName()
        {
            Console.WriteLine("What is your name?");
            return Console.ReadLine();
        }

        public static Difficulty GetDifficulty()
        {
            Console.WriteLine("What size map would you like to play?");
            Console.WriteLine("1. Small");
            Console.WriteLine("2. Medium");
            Console.WriteLine("3. Large");
            string response = Console.ReadLine();
            int responseInt;
            Int32.TryParse(response, out responseInt);
            return responseInt > 0  && responseInt < 4 ? (Difficulty)responseInt : Difficulty.easy;
        }
    }
}
