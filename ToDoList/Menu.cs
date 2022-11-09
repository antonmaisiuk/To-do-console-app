using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace ToDoList
{
    class Menu
    {
        private string[] Options;
        private int SelectedIndex;
        private string Prompt;
        public static ConsoleColor MenuBgColor = ConsoleColor.White;
        public static ConsoleColor TextColor = ConsoleColor.Black;

        public Menu(string prompt, string[] options)
        {
            Prompt = prompt;
            Options = options;
            SelectedIndex = 0;
        }



        private void DisplayOptions()
        {
            WriteLine(Prompt);
            for (int i = 0; i < Options.Length; i++)
            {
                string currentOption = Options[i];


                if (i == SelectedIndex)
                {
                    //prefix = "*";
                    ForegroundColor = TextColor;
                    BackgroundColor = MenuBgColor;
                }
                else
                {
                    //prefix = " ";
                    BackgroundColor = ConsoleColor.Black;
                    ForegroundColor = ConsoleColor.White;
                }

                WriteLine($" << {currentOption} >>");
            }
            ResetColor();
        }

        public int Run()
        {
            ConsoleKey keyPressed;
            DisplayOptions();
            do
            {
                //DisplayOptions();
                //Clear();
                //DisplayOptions();

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;

                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = Options.Length - 1;
                    }

                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    SelectedIndex++;

                    if (SelectedIndex == Options.Length)
                    {
                        SelectedIndex = 0;
                    }
                }
                //WriteLine("Cursor: " + Console.CursorTop);
                Console.SetCursorPosition(0, 10);
                DisplayOptions();
            } while (keyPressed != ConsoleKey.Enter);

            return SelectedIndex;
        }
        public int RunWithPrompt()
        {
            ConsoleKey keyPressed;
            int cursorPos = Console.CursorTop;
            DisplayOptions();
            do
            {

                //DisplayOptions();

                //WriteLine("Cursor: " + Console.CursorTop);
                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;
                    //Console.SetCursorPosition(0, Console.CursorTop - 4);
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = Options.Length - 1;
                    }

                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    SelectedIndex++;

                    if (SelectedIndex == Options.Length)
                    {
                        SelectedIndex = 0;
                    }
                }
                //Console.SetCursorPosition(0, Console.CursorTop - (Options.Length + 4));
                Console.SetCursorPosition(0, cursorPos);
                DisplayOptions();
            } while (keyPressed != ConsoleKey.Enter);

            return SelectedIndex;
        }
    }
}
