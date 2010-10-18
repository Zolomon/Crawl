using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lampa;
using Lampa.Objects;

namespace Lampa.States
{
    public class CharacterCreation : State
    {
        private ConsoleKeyInfo keyInfo;
        private CreationStep creationStep;
        private Player player;
        private Dictionary<string, MenuItem> classItemList;
        private string[] classItems;
        private int classIndex;
        private MenuItem selectedItem;

        private enum CreationStep
        {
            Name,
            Class,
            Stats
        }

        public CharacterCreation()
        {
            this.player = new Player();
            keyInfo = new ConsoleKeyInfo();

            creationStep = CreationStep.Name;

            classItemList = new Dictionary<string, MenuItem>();
            classItems = new string[] {"F", "R", "W"};
            classItemList.Add(classItems[classIndex], new MenuItem(classItems[classIndex++], "Fighter"));
            classItemList.Add(classItems[classIndex], new MenuItem(classItems[classIndex++], "Rogue"));
            classItemList.Add(classItems[classIndex], new MenuItem(classItems[classIndex++], "Wizard"));
        }

        public override StateMachine.StateAction Enter(GameTime gameTime)
        {
            Console.Clear();
            return StateMachine.StateAction.Continue;
        }
        public override StateMachine.StateAction During(GameTime gameTime)
        {
            //keyInfo = Console.ReadKey(true);

            switch (creationStep)
            {
                case CreationStep.Name:
                    string name = "";
                    while (String.IsNullOrEmpty(name))
                    {
                        name = Console.ReadLine();
                        if (String.IsNullOrEmpty(name))
                        {
                            continue;
                        }
                        player.Name = name;
                        creationStep = CreationStep.Class;
                    }
                    break;
                case CreationStep.Class:
                    while (player.Class == Player.PlayerClass.None)
                    {
                        keyInfo = Console.ReadKey(true);

                        if (keyInfo.Key == ConsoleKey.Spacebar)
                            return StateMachine.StateAction.Continue;

                        if (classItemList.ContainsKey(keyInfo.Key.ToString().ToUpper()))
                            selectedItem = classItemList[keyInfo.Key.ToString()];

                        if (keyInfo.Key == ConsoleKey.UpArrow || keyInfo.Key == ConsoleKey.LeftArrow)
                            ClassIndex--;
                        if (keyInfo.Key == ConsoleKey.DownArrow || keyInfo.Key == ConsoleKey.RightArrow)
                            ClassIndex++;

                        selectedItem = classItemList[classItems[ClassIndex]];

                        if (keyInfo.Key == ConsoleKey.Enter)
                        {
                            selectedItem = classItemList[classItems[ClassIndex]];
                            return StateMachine.StateAction.Continue;
                        }

                        return StateMachine.StateAction.Remain;
                    }
                    

                    break;
                case CreationStep.Stats:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (keyInfo.Key == ConsoleKey.X)
            {
                return StateMachine.StateAction.Continue;
            }
            return StateMachine.StateAction.Remain;
        }
        public override StateMachine.StateAction Exit(GameTime gameTime)
        {
            return StateMachine.StateAction.Continue;
        }
        public override void Draw(StateMachine.Stage stage)
        {
            if (stage == StateMachine.Stage.During)
            {
                Console.Clear();
                switch (creationStep)
                {
                    case CreationStep.Name:
                        IO.Print("Please enter your #C0name|: ");
                        break;
                    case CreationStep.Class:
                        {
                            IO.Print("Please enter your #B0class|: ");
                            
                        }
                        break;
                    case CreationStep.Stats:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void RenderClassMenu(MenuItem selectedMenuItem)
        {
            ConsoleColor color = ConsoleColor.Gray;
            Console.Write("Please select a ");
            IO.Print("class", ConsoleColor.Yellow);
            Console.Write(":\n");

            foreach (KeyValuePair<string, MenuItem> kvp in classItemList)
            {
                if (selectedMenuItem.Key == "F")
                    color = ConsoleColor.Green;
                else if (selectedMenuItem.Key == "R")
                    color = ConsoleColor.Yellow;
                else if (selectedMenuItem.Key == "W")
                    color = ConsoleColor.Blue;
                else
                    color = ConsoleColor.Gray;

                IO.Print(String.Format("\t{0}\n", kvp.Value), color);
            }
        }

        public int ClassIndex
        {
            get { return classIndex; }
            set
            {
                if (value < 0)
                    classIndex = classItems.Length - 1;
                else if (value > classItems.Length - 1)
                    classIndex = 0;
                else
                    classIndex = value;
            }
        }
    }
}
