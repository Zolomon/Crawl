using System;
using System.Collections.Generic;

namespace Lampa.States
{
    public class TitleScreen : State
    {
        private ConsoleKeyInfo key;
        private Dictionary<string, MenuItem> MenuItemList { get; set; }
        private string[] items { get; set; }
        private int selectedItemIndex;
        private MenuItem selectedItem;
        public string TitleScreenText { get; set; }

        public TitleScreen()
        {
            // Do initialization here
            key = new ConsoleKeyInfo();
            // Create the menu list

            items = new string[] { "N", "C", "O", "I", "X" };

            MenuItemList = new Dictionary<string, MenuItem>();
            MenuItemList.Add(items[selectedItemIndex], new MenuItem(items[selectedItemIndex++], "#E0New Game|", new CharacterCreation()));
            MenuItemList.Add(items[selectedItemIndex], new MenuItem(items[selectedItemIndex++], "#D0Continue|", new State()));
            MenuItemList.Add(items[selectedItemIndex], new MenuItem(items[selectedItemIndex++], "#F0Options|", new State()));
            MenuItemList.Add(items[selectedItemIndex], new MenuItem(items[selectedItemIndex++], "#A0Credits|", new State()));
            MenuItemList.Add(items[selectedItemIndex], new MenuItem(items[selectedItemIndex++], "#B0Exit|", new State()));

            SelectedItemIndex = 0;
            selectedItem = MenuItemList[items[selectedItemIndex]];
            
        }

        public override StateMachine.StateAction Enter(GameTime gameTime)
        {
            return StateMachine.StateAction.Continue;
        }

        public override StateMachine.StateAction During(GameTime gameTime)
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Spacebar)
                return StateMachine.StateAction.Continue;

            if (MenuItemList.ContainsKey(key.Key.ToString().ToUpper()))
                while (!items[SelectedItemIndex].Equals(key.Key.ToString()))
                {
                    SelectedItemIndex++;
                }
            
            if (key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.LeftArrow)
                SelectedItemIndex--;
            if (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.RightArrow)
                SelectedItemIndex++;

            selectedItem = MenuItemList[items[selectedItemIndex]];

            if (key.Key == ConsoleKey.Enter)
            {
                selectedItem = MenuItemList[items[SelectedItemIndex]];
                return StateMachine.StateAction.Continue;
            }
            
            return StateMachine.StateAction.Remain;
        }

        public override StateMachine.StateAction Exit(GameTime gameTime)
        {
            return StateMachine.StateAction.Continue;
        }

        public int SelectedItemIndex
        {
            get { return selectedItemIndex; }
            set
            {
                if (value < 0)
                    selectedItemIndex = items.Length - 1;
                else if (value > items.Length - 1)
                    selectedItemIndex = 0;
                else
                    selectedItemIndex = value;
            }
        }
   
        public override void Draw(StateMachine.Stage stage)
        {
            if (stage == StateMachine.Stage.During || stage == StateMachine.Stage.Exit)
            {
                Console.Clear();
                RenderMenu(selectedItem);
                if (key.Key != ConsoleKey.Enter && selectedItem != null) 
                    IO.Print(String.Format("You selected item: {0}", selectedItem.Text));
                else if (key.Key == ConsoleKey.Enter && selectedItem != null)
                {
                    Console.WriteLine("You pressed enter. Now we should go to the selected State.");
                    Console.Write("The selected state was: "); 
                    IO.Print(selectedItem.Text, ConsoleColor.White, ConsoleColor.Blue);

                }
            }
        }
        public override State OverrideGetNext()
        {
            if (selectedItem != null)
            {
                return selectedItem.State;
            }
            return null;
        }

        private void RenderMenu(MenuItem selectedMenuItem)
        {
            ConsoleColor color = ConsoleColor.Gray;
            Console.WriteLine("Menu: ");

            foreach (KeyValuePair<string, MenuItem> kvp in MenuItemList)
            {
                if (selectedMenuItem == kvp.Value)
                    IO.Print(String.Format("\t[{1}] [{0}\t]\n", kvp.Value.Text, kvp.Value.Key));
                else
                    IO.Print(String.Format("\t[{1}]  {0}\n", kvp.Value.Text, kvp.Value.Key));
                
            }
        }
    }
}
