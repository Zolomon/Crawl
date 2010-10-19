using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lampa.Objects;
using Lampa.States;
namespace Lampa
{
    public class Game
    {
        private GameTime gameTime = null;
        private StateMachine mainStateMachine;

        public Game()
        {

            mainStateMachine = new StateMachine(true);
            mainStateMachine.AddState(new States.TitleScreen());

            //player = new Actor();
            //MapReader mr = new MapReader();
            //Map map = mr.Read("resources/map_0_0.bmp");

            //for (int y = 0; y < map.Height; y++)
            //{
            //    for (int x = 0; x < map.Width; x++)
            //    {
            //        if (map.GetRoom(x, y).Type == Room.RoomType.Wall)
            //        {
            //            //Print("#", ConsoleColor.DarkYellow);
            //            Console.Write("#");
            //        }
            //        else
            //        {
            //            Console.Write(" ");
            //        }
            //    }
            //    Console.WriteLine();
            //}
            mainStateMachine.SetLooped(false);
            while (!mainStateMachine.finished)
            {
                Update();
                Draw();
            }
            Console.ReadLine();
        }

        public void Update()
        {
            mainStateMachine.Update(gameTime);
        }

        public void Draw()
        {
            mainStateMachine.Draw();
        }
    }
}
