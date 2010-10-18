using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lampa
{
    public class Map
    {
        public Map(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            Rooms = new Room[this.Width * this.Height];
        }

        public Room SetRoom(int x, int y, Room room)
        {
            Room tempRoom = null;
            if ((x >= 0 && x < Width) && 
                (y >= 0 && y < Height))
            {
                if (Rooms[(Width * y) + x] != null)
                {
                    tempRoom = Rooms[(Width * y) + x];
                }
                Rooms[(Width * y) + x] = room;
            }    
            return tempRoom;
        }

        public Room GetRoom(int x, int y)
        {
            if (x < 0 || x > Width || y < 0 || y > Height)
                throw new IndexOutOfRangeException("Parameters out of bounds.");
            return Rooms[(Width * y) + x];
        }
        public int Height { get; set; }
        public int Width { get; set; }
        public Room[] Rooms { get; set; }
    }
}
