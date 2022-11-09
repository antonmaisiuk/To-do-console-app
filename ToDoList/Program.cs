using System;
using static System.Console;

namespace ToDoList
{
    class Program
    {
        

        static void Main(string[] args)
        {
            ToDo todo = new ToDo();        

            

            ToDo.Start(ConsoleColor.Gray);
        }
    }
}
