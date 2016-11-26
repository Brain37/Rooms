namespace Rooms
{
    public abstract class Character
    {
        public string Name { get; set; }
        public int health { get; set; }
        public RockPaperScissors choice { get; set; }

        public abstract void TakeDamage();

    }
}
