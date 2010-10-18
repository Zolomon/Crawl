using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lampa
{
    public class Room
    {

        public Room()
        {
            Type = RoomType.Grass;
        }

        public Room(RoomType type, List<Item> items)
        {
            this.Type = type;
            this.Items = items;
        }

        public RoomType Type { get; set; }
        public List<Item> Items { get; set; }
        public enum RoomType
        {
            Wall,
            Grass,
            GrassEnemy,
            GrassChest,
            Water
        }
    }
}
