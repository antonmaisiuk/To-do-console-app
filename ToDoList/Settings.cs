using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    class Settings
    {
        public static void SettingsMenu(ConsoleColor logoColor)
        {
            ConsoleClear.Clear();
            Console.SetCursorPosition(0, 10);

            string[] options = { "Menu background color", "Logo color", "Back" };
            Menu mainMenu = new Menu(" Settings", options);
            int selectedIndex = mainMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    ChangeMenuBgColor();
                    break;
                case 1:
                    ChangeLogoColor(logoColor);
                    break;
                case 2:
                    ToDo.RunMainMenu();
                    break;
            }
        }

        public static void ChangeMenuBgColor()
        {
            ConsoleClear.Clear();
            Console.SetCursorPosition(0, 10);

            string[] options = { "White", "Blue", "Red" };
            Menu mainMenu = new Menu(" Choose color", options);
            int selectedIndex = mainMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    Menu.MenuBgColor = ConsoleColor.White;
                    Menu.TextColor = ConsoleColor.Black;
                    ToDo.RunMainMenu();
                    break;
                case 1:
                    Menu.MenuBgColor = ConsoleColor.Blue;
                    Menu.TextColor = ConsoleColor.White;
                    ToDo.RunMainMenu();
                    break;
                case 2:
                    Menu.MenuBgColor = ConsoleColor.Red;
                    Menu.TextColor = ConsoleColor.White;
                    ToDo.RunMainMenu();
                    break;
            }
        }

        public static void ChangeLogoColor(ConsoleColor logoColor)
        {
            ConsoleClear.Clear();
            Console.SetCursorPosition(0, 10);

            string[] options = { "White", "Blue", "Red" };
            Menu mainMenu = new Menu(" Choose color", options);
            int selectedIndex = mainMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    logoColor = ConsoleColor.White;
                    ToDo.Start(logoColor);
                    break;
                case 1:
                    logoColor = ConsoleColor.Blue;
                    ToDo.Start(logoColor);
                    break;
                case 2:
                    logoColor = ConsoleColor.Red;
                    ToDo.Start(logoColor);
                    break;
            }
        }
    }
}
