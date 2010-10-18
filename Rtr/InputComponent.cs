using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lampa.Objects;

namespace Lampa
{
    public class InputComponent
    {
        private const int MOVEMENT_ACCELERATION = 1;
        public void Update(Actor actor)
        {
            //switch (Controller.GetMovementDirection())
            //{
            //    case LEFT:
            //        actor.X -= MOVEMENT_ACCELERATION;
            //        break;
            //    case RIGHT:
            //        actor.X += MOVEMENT_ACCELERATION;
            //        break;
            //    case UP:
            //        actor.Y -= MOVEMENT_ACCELERATION;
            //        break;
            //    case DOWN:
            //        actor.Y += MOVEMENT_ACCELERATION;
            //        break;
            //}
        }
    }
}
