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
        private int strength { get; set; }
        private int intelligence { get; set; }
        private int dexterity { get; set; }
        private int constitution { get; set; }
        private int charisma { get; set; }
        private int wisdom { get; set; }
        private Random rand;

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

            ClassIndex = 0;
            selectedItem = classItemList[classItems[ClassIndex]];

            rand = new Random();
            strength = rand.Next(8, 21);
            dexterity = rand.Next(8, 21);
            constitution = rand.Next(8, 21);
            intelligence = rand.Next(8, 21);
            wisdom = rand.Next(8, 21);
            charisma = rand.Next(8, 21);
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
                            player.Class = (Player.PlayerClass)(ClassIndex + 1);

                            creationStep = CreationStep.Stats;
                            //return StateMachine.StateAction.Continue;
                        }

                        return StateMachine.StateAction.Remain;
                    }
                    break;
                case CreationStep.Stats:
                    while (player.Strength == 0)
                    {
                        keyInfo = Console.ReadKey(true);

                        if (keyInfo.Key == ConsoleKey.Spacebar)
                            return StateMachine.StateAction.Continue;

                        if (keyInfo.Key == ConsoleKey.R)
                        {
                            strength = rand.Next(8, 21);
                            dexterity = rand.Next(8, 21);
                            constitution = rand.Next(8, 21);
                            intelligence = rand.Next(8, 21);
                            wisdom = rand.Next(8, 21);
                            charisma = rand.Next(8, 21);
                        }

                        if (keyInfo.Key == ConsoleKey.Enter)
                        {
                            player.Strength = strength;
                            player.Dexterity = dexterity;
                            player.Constitution = constitution;
                            player.Intelligence = intelligence;
                            player.Wisdom = wisdom;
                            player.Charisma = charisma;
                            return StateMachine.StateAction.Continue;
                        }

                        if (keyInfo.Key == ConsoleKey.Enter)
                        {
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

        private string StatToColour(int stat)
        {
            // 8-11     == RED      | BAD
            // 12-15    == YELLOW   | MEDIOCRE
            // 16-18    == GREEN    | GOOD
            // 19-20    == WHITE    | FRIGGIN AWESOME

            if (stat <= 11)
                return String.Format("#C0{0}|", stat);
            if (stat <= 15)
                return String.Format("#E0{0}|", stat);
            if (stat <= 18)
                return String.Format("#A0{0}|", stat);
            return String.Format("#F0{0}|", stat);
        }

        private string TotalStatToColour(int stat)
        {
            // 48-67     == RED      | BAD
            // 68-83    == YELLOW   | MEDIOCRE
            // 84-101    == GREEN    | GOOD
            // 102-120    == WHITE    | FRIGGIN AWESOME

            if (stat <= 67)
                return String.Format("#C0{0}|", stat);
            if (stat <= 83)
                return String.Format("#E0{0}|", stat);
            if (stat <= 101)
                return String.Format("#A0{0}|", stat);
            return String.Format("#F0{0}|", stat);
        }

        private void RenderClassMenu(MenuItem selectedMenuItem)
        {
            IO.Print(String.Format("Your name is #F0{0}|.\n", player.Name));
            IO.Print("Your is #D0class| is: \n\n");

            foreach (KeyValuePair<string, MenuItem> kvp in classItemList)
            {
                IO.Print(selectedMenuItem == kvp.Value
                             ? String.Format("\t[{1}] [{0}\t]\n", kvp.Value.Text, kvp.Value.Key)
                             : String.Format("\t[{1}]  {0}\n", kvp.Value.Text, kvp.Value.Key));
            }
        }

        private void RenderStatsMenu()
        {
            IO.Print(String.Format("Your name is #F0{0}|.\n", player.Name));
            IO.Print(String.Format("Your is #D0class| is {0}\n\n", player.Class.ToString()));
            IO.Print("(#B0R|)eroll stats, (#C0S|)elect current stats: \n\n");

            IO.Print(String.Format("\tStr: {0:d2}\t\tInt: {1:d2}\n\tDex: {2:d2}\t\tWis: {3:d2}\n\tCon: {4:d2}\t\tCha: {5:d2}\n\n",
                StatToColour(strength), StatToColour(intelligence), StatToColour(dexterity), StatToColour(wisdom), StatToColour(constitution), StatToColour(charisma)));
            IO.Print(String.Format("\tTotal: {0}", TotalStatToColour(strength + dexterity + constitution + intelligence + wisdom + charisma)));
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
