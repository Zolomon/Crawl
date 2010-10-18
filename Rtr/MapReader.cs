using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace Lampa
{
    public class MapReader
    {
        private Color ColorWall = Color.Black;
        private Color ColorGrass = Color.Green;
        private Color ColorChestOnGrass = Color.FromArgb(0x80, 0x40, 0x00);
        private Color ColorEnemyOnGrass = Color.Red;
        private Color ColorWater = Color.FromArgb(0x00, 0x80, 0xFF);

        public MapReader()
        {

        }

        public Map Read(string mapFileName)
        {
            Bitmap bmp = new Bitmap(mapFileName);
            Map map = new Map(bmp.Size.Width, bmp.Size.Height);
            Room.RoomType type = Room.RoomType.Wall;
            List<Item> items = new List<Item>();
            Random rand = new Random();
            
            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    items.Clear();
                    if (bmp.GetPixel(x, y).Equals(ColorWall))
                    {
                        type = Room.RoomType.Wall;
                    }
                    else if (bmp.GetPixel(x,y).Equals(ColorGrass))
                    {
                        type = Room.RoomType.Grass;
                    }
                    else if (bmp.GetPixel(x, y).Equals(ColorChestOnGrass))
                    {
                        type = Room.RoomType.GrassChest;
                        items.Add(Item.RandomItem());
                    }
                    else if (bmp.GetPixel(x, y).Equals(ColorEnemyOnGrass))
                    {
                        type = Room.RoomType.GrassEnemy;
                    }
                    else if (bmp.GetPixel(x, y).Equals(ColorWater))
                    {
                        type = Room.RoomType.Water;
                    }

                    map.SetRoom(x, y, new Room(type, items));
                }
            }

            return map;
        }
    }
}
