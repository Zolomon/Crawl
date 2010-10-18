using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lampa.Objects
{
    public class Player : Actor
    {
        public PlayerClass Class { get; set; }
        public enum PlayerClass
        {
            None,
            Fighter,
            Rogue,
            Wizard
        }
        public Player()
        {
            Class = PlayerClass.None;
        }
    }
}
