using System;

namespace Rooms
{
    public class Player : Character
    {
        public Room CurrentRoom { get; set; }
        public int Sneaking { get; set; }

        public Player(string name)
        {
            this.Name = name;
            this.health = 3;
            this.Sneaking = 4;
        }

        public void Play()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine($"You are in {this.CurrentRoom.Name}");
                    if (string.Equals(this.CurrentRoom.Name, Core.TheMap.WinningRoom.Name))
                    {
                        YouWin();
                        return;
                    }
                    else
                    {
                        //you haven't won yet and need to keep playing 
                        if (this.CurrentRoom.HasEnemy())
                        {
                            RoomHasEnemy();
                        }
                        else
                        {
                            int responseInt = DisplayExits();
                            switch (responseInt)
                            {
                                case 1:
                                    {
                                        Go(this.CurrentRoom.Exit);
                                        break;
                                    }
                                case 2:
                                    {
                                        GiveUp();
                                        return;
                                    }
                            }
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Brian is bad at coding, here's why: " + e);
            }
        }

        public void Go(Exit exit)
        {
            foreach(Room room in Core.TheMap.Rooms)
            {
                if(string.Equals(room.Name, exit.Destination, StringComparison.OrdinalIgnoreCase))
                {
                    this.CurrentRoom = room;
                    return;
                }
            }
            return;
        }

        public void RoomHasEnemy()
        {
            Console.WriteLine();
            Console.WriteLine("There is in an enemy in this room.");
            Console.WriteLine("1. Fight enemy");
            Console.WriteLine("2. Give up");
            Console.WriteLine("3. Attempt to sneak past enemy");
            int responseInt;
            string response = Console.ReadLine();
            Int32.TryParse(response, out responseInt);

            switch(responseInt)
            {
                case 1:
                    {
                        FightEnemy();
                        break;
                    }
                case 2:
                    {
                        GiveUp();
                        break;
                    }
                case 3:
                    {
                        SneakBy();
                        break;
                    }
            }
            return;
        }

        public void SneakBy()
        {
            Random r = new Random();
            int chance = r.Next(1, this.Sneaking);
            if(chance == 1)
            {
                Console.WriteLine();
                Console.WriteLine("You have successfully gotten by the enemy!");
                if(this.Sneaking > 3)
                {
                    this.Sneaking--;
                    Console.WriteLine($"Your sneaking level has increased.");
                    Console.WriteLine($"Your chance of successfully sneaking is now 1/{Sneaking - 1}");
                }
                else
                {
                    Console.WriteLine("Your sneaking level cannot get any higher!");
                }

                Go(this.CurrentRoom.Exit);
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("The enemy caught you and hurt you as you tried to sneak by.");
                this.TakeDamage();
                FightEnemy();
            }
        }

        public void FightEnemy()
        {
            Enemy currentEnemy = (Enemy)this.CurrentRoom.Enemy;
            Console.WriteLine("Let the battle begin.");
            while (currentEnemy.health > 0)
            {
                Console.WriteLine();
                Console.WriteLine("1. Rock");
                Console.WriteLine("2. Paper");
                Console.WriteLine("3. Scissors");
                var response = Console.ReadLine();
                int responseInt;
                Int32.TryParse(response, out responseInt);
                this.choice = (RockPaperScissors)responseInt;
                CompareResponses(currentEnemy);
            }
            this.health = 3;
        }

        public int DisplayExits()
        {
            int responseInt = 0;
            while (responseInt == 0 || responseInt > 2)
            {
                Console.WriteLine();
                Console.WriteLine("1. Go to next room");
                Console.WriteLine("2. Give up");
                string response = Console.ReadLine();
                Int32.TryParse(response, out responseInt);
            }
            return responseInt;
        }

        public void YouWin()
        {
            Console.WriteLine($"{this.Name} is the champ. W0000000T!");
            Console.ReadLine();
            Environment.Exit(0);
        }

        public bool CompareResponses(Enemy enemy)
        {
            enemy.choice = enemy.RockPaperScissors();
            Console.WriteLine($"Enemy Choice is {enemy.choice}");

            if (this.choice == enemy.choice)
            {
                Console.WriteLine("This round is a tie.");
                return false;
            }
            else if (this.choice == RockPaperScissors.Rock && enemy.choice == RockPaperScissors.Paper)
            {
                Console.WriteLine("You lose this round.");
                this.TakeDamage();
            }
            else if (this.choice == RockPaperScissors.Scissors && enemy.choice == RockPaperScissors.Paper)
            {
                Console.WriteLine("You win this round.");
                enemy.TakeDamage();
            }
            else if (this.choice == RockPaperScissors.Rock && enemy.choice == RockPaperScissors.Scissors)
            {
                Console.WriteLine("You win this round.");
                enemy.TakeDamage();
            }
            else if (this.choice == RockPaperScissors.Paper && enemy.choice == RockPaperScissors.Scissors)
            {
                Console.WriteLine("You lose this round.");
                this.TakeDamage();
            }
            else if (this.choice == RockPaperScissors.Scissors && enemy.choice == RockPaperScissors.Rock)
            {
                Console.WriteLine("You lose this round.");
                this.TakeDamage();
            }
            else if (this.choice == RockPaperScissors.Paper && enemy.choice == RockPaperScissors.Rock)
            {
                Console.WriteLine("You win this round.");
                enemy.TakeDamage();
            }
            return true;
        }

        public void GiveUp()
        {
            Console.WriteLine();
            Console.WriteLine("What a weenie...");
            Console.WriteLine("You Lose.");
            Console.WriteLine(":P");
            Console.ReadLine();
            Environment.Exit(0);
        }

        public override void TakeDamage()
        {
            this.health--;
            if (this.health > 0)
            {
                Console.WriteLine($"{this.Name}'s health is {this.health}");
            }
            else
            {
                Console.WriteLine($"Oh dear, you are dead ;(");
                GiveUp();
            }
            return;
        }
    }
}
