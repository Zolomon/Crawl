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
            classItems = new string[] { "F", "R", "W" };
            classItemList.Add(classItems[classIndex], new MenuItem(classItems[classIndex++], "#A0F|ighter"));
            classItemList.Add(classItems[classIndex], new MenuItem(classItems[classIndex++], "#B0R|ogue"));
            classItemList.Add(classItems[classIndex], new MenuItem(classItems[classIndex++], "#C0W|izard"));
        }

        public override StateMachine.StateAction Enter(GameTime gameTime)
        {
            Console.Clear();
            return StateMachine.StateAction.Continue;
        }
        public override StateMachine.StateAction During(GameTime gameTime)
        {
            switch (creationStep)
            {
                case CreationStep.Name:
                    string name = "";
                    while (String.IsNullOrEmpty(name))
                    {
                        name = Console.ReadLine();
                        if (String.IsNullOrEmpty(name))
                            continue;
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
                            while (!classItems[ClassIndex].Equals(keyInfo.Key.ToString()))
                                ClassIndex++;

                        if (keyInfo.Key == ConsoleKey.UpArrow || keyInfo.Key == ConsoleKey.LeftArrow)
                            ClassIndex--;
                        if (keyInfo.Key == ConsoleKey.DownArrow || keyInfo.Key == ConsoleKey.RightArrow)
                            ClassIndex++;

                        selectedItem = classItemList[classItems[ClassIndex]];

                        if (keyInfo.Key == ConsoleKey.Enter)
                        {
                            selectedItem = classItemList[classItems[ClassIndex]];
                            creationStep = CreationStep.Stats;
                            //return StateMachine.StateAction.Continue;
                        }

                        return StateMachine.StateAction.Remain;
                    }
                    break;
                case CreationStep.Stats:
                    while (player.Class == Player.PlayerClass.None)
                    {
                        keyInfo = Console.ReadKey(true);

                        if (keyInfo.Key == ConsoleKey.Spacebar)
                            return StateMachine.StateAction.Continue;

                        if (classItemList.ContainsKey(keyInfo.Key.ToString().ToUpper()))
                            while (!classItems[ClassIndex].Equals(keyInfo.Key.ToString()))
                                ClassIndex++;

                        selectedItem = classItemList[classItems[ClassIndex]];

                        if (keyInfo.Key == ConsoleKey.Enter)
                        {
                            selectedItem = classItemList[classItems[ClassIndex]];
                            creationStep = CreationStep.Stats;
                            //return StateMachine.StateAction.Continue;
                        }

                        return StateMachine.StateAction.Remain;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (keyInfo.Key == ConsoleKey.X)
                return StateMachine.StateAction.Continue;
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
                        RenderClassMenu(selectedItem);
                        break;
                    case CreationStep.Stats:
                        RenderStatsMenu();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void RenderStatsMenu()
        {
            IO.Print("(#B0R|)eroll stats, (#C0S)elect current stats: \n\n");
        }

        private void RenderClassMenu(MenuItem selectedMenuItem)
        {
            IO.Print("Please enter your #D0class|: \n\n");

            foreach (KeyValuePair<string, MenuItem> kvp in classItemList)
            {
                //IO.Print(String.Format("\t[]{0}\n", kvp.Value));

                IO.Print(selectedMenuItem == kvp.Value
                             ? String.Format("\t[{1}] [{0}\t]\n", kvp.Value.Text, kvp.Value.Key)
                             : String.Format("\t[{1}]  {0}\n", kvp.Value.Text, kvp.Value.Key));
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
