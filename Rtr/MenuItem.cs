using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lampa
{
    public class MenuItem
    {
        public MenuItem(string key, string text)
        {
            Text = text;
            Key = key;
            State = null;
        }
        public MenuItem(string key, string text, State state)
        {
            Text = text;
            Key = key;
            State = state;
        }

        public override string ToString()
        {
            return String.Format("[{0}] {1}", Key, Text);
        }

        public State State { get; set; }
        public string Text { get; set; }
        public string Key { get; set; }
    }
}
