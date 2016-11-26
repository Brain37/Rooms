using System;
using System.Collections.Generic;

namespace Rooms
{
    public class Map
    {
        public Room[] Rooms { get; set; }
        public Room WinningRoom { get; set; } = new Room
        {
            Name = "Winning Room"
        };

        Room StartingRoom = new Room
        {
            Name = "Starting Room"
        };

        Random r = new Random();

        public Map(string name, Difficulty difficulty)
        {
            Player player = new Player(name);
            CreateMap(player, difficulty);
        }

        public void CreateMap(Player player, Difficulty difficulty)
        {
            int size = (int)difficulty * 5;
            this.Rooms = new Room[size + 2];
            player.CurrentRoom = this.StartingRoom;

            for(int i = size; i > 0; i--)
            {
                this.Rooms[i] = (new Room
                {
                    Name = $"Room {i}",                  
                });
            }

            this.Rooms[0] = this.StartingRoom;
            this.Rooms[this.Rooms.Length - 1] = this.WinningRoom;

            //create exits
            for(int i = 0; i < this.Rooms.Length - 1; i++)
            {
                this.Rooms[i].Exit = new Exit
                {
                    Destination = this.Rooms[i + 1].Name,
                    Source = this.Rooms[i].Name
                };

                //if(i < this.Rooms.Length - 1)
                //{
                //    this.Rooms[i].Exit = new Exit
                //    {
                //        Destination = this.Rooms[i].Name,
                //        Source = this.Rooms[i + 1].Name
                //    };
                //}
            }
            Core.TheMap = this;
            AddMonstersToAllRoomsExceptStartAndEnd();
            player.Play();
        } 

        public void AddMonstersToAllRoomsExceptStartAndEnd()
        {
            foreach(Room room in Core.TheMap.Rooms)
            {
                if(!string.Equals(room.Name, this.StartingRoom.Name) && !string.Equals(room.Name, this.WinningRoom.Name))
                {
                    room.Enemy = new Enemy();
                }
            }
            return;
        }
    }
}
