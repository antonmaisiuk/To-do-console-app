using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    class ConsoleClear
    {
        private static int ConsoleHeight = Console.WindowHeight;
        private static int ConsoleWidth = Console.WindowWidth;
        private static string ClearString = "";


        public static void Clear()
        {
            //Console.Write(ConsoleHeight + " " + ConsoleWidth);
            for (int i = 0; i < ConsoleWidth; i++)
            {
                ClearString += " ";
            }

            for (int i = 8; i < ConsoleHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(ClearString);
                
            }
            ClearString = "";
        }
        

    }
}
