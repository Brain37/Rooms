using System;

namespace Rooms
{
    public class Enemy : Character
    {
        public Enemy()
        {
            this.Name = "Enemy";
            this.health = 1;
        }

        public RockPaperScissors RockPaperScissors()
        {
            Random r = new Random();
            return (RockPaperScissors)r.Next(1, 4);
        }

        public override void TakeDamage()
        {
            this.health--;
            if (this.health > 0)
            {
                Console.WriteLine($"Enemy health is at {this.health}");
            }
            else
            {
                Console.WriteLine("You have killed the enemy");
                Console.WriteLine();
            }
        }
    }
}
