using System.Collections.Generic;

namespace Rooms
{
    public class Room
    {
        public Exit Exit { get; set; }
        public string Name { get; set; }
        public Character Enemy { get; set; }

        public Room()
        {
            Exit = new Exit();
        }

        public bool HasEnemy()
        {
            return this.Enemy == null || this.Enemy.health == 0 ? false : true;
        }
    }
}
