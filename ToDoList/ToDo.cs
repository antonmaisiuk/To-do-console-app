using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace ToDoList
{
    class ToDo
    {
        private static ConsoleColor logoColor = ForegroundColor;
        private static string fileLocation = "todo-items.txt";
        private static List<Task> tasksList = new List<Task>();
        ConsoleClear consoleClear = new ConsoleClear();
        public static string prompt = @"
    ___________          .___       .__  .__          __   
    \__    ___/___     __| _/____   |  | |__| _______/  |_ 
      |    | /  _ \   / __ |/  _ \  |  | |  |/  ___/\   __\
      |    |(  <_> ) / /_/ (  <_> ) |  |_|  |\___ \  |  |  
      |____| \____/  \____ |\____/  |____/__/____  > |__|  
                      \/                     \/        
    (use UpArrow and DownArrow to select)
";

        public static void Start(ConsoleColor logoColor)
        {
            Title = "Todo List - Productivity App!";
            Clear();
            tasksList.Clear();
            LoadItems();

            Console.ForegroundColor = logoColor;
            Write(prompt);
            Console.ForegroundColor = ConsoleColor.Gray;


            RunMainMenu();
        }

        public static void RunMainMenu()
        {
            //Console.BackgroundColor = ConsoleBgColor;
            
            string[] options = { "Add task", "Edit task", "Delete task", "View to do list", "Settings","Exit" };
            Menu mainMenu = new Menu(" Main menu", options);
            ConsoleClear.Clear();
            Console.SetCursorPosition(0, 10);
            int selectedIndex = mainMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    TaskCRUD.AddTask(tasksList);
                    break;
                case 1:
                    TaskCRUD.EditTask(tasksList);
                    break;
                case 2:
                    TaskCRUD.DeleteTask(tasksList);
                    break;
                case 3:
                    ViewTodoList();
                    break;
                case 4:
                    Settings.SettingsMenu(logoColor);
                    break;
                case 5:
                    ExitApp();
                    break;
            }
        }

        private static void ViewTodoList()
        {
            ConsoleClear.Clear();
            Console.SetCursorPosition(0, 10);

            if (tasksList.Count() == 0)
            {
                WriteLine(" You don't have task to do.\n");

                string[] options = { "Add task", "Back to menu" };
                Menu viewMenu = new Menu(" Menu", options);
                int selectedIndex = viewMenu.RunWithPrompt();

                switch (selectedIndex)
                {
                    case 0:
                        TaskCRUD.AddTask(tasksList);
                        break;
                    case 1:
                        RunMainMenu();
                        break;

                }
            }
            else
            {
                WriteLine(" There are your tasks.\n");
                WriteLine("{0,5} {1,-50}", "ID", "Task\n");
                tasksList.ForEach(task =>
                {
                    WriteLine("{0,5} {1,-50}", task.GetId(), task.GetTaskText());
                });

                string[] options = { "Add task", "Mark task as done", "Back to menu" };
                Menu viewMenu = new Menu(" Menu", options);
                int selectedIndex = viewMenu.RunWithPrompt();

                switch (selectedIndex)
                {
                    case 0:
                        TaskCRUD.AddTask(tasksList); ;
                        break;
                    case 1:
                        TaskCRUD.MarkAsDone(tasksList);
                        break;
                    case 2:
                        RunMainMenu();
                        break;

                }
            }

        }       

        private static void ExitApp()
        {
            WriteLine("\n Press any key to exit...");
            ReadKey(true);
            SaveItems();
            Environment.Exit(0);
        }

        private static void LoadItems()
        {
            if (File.Exists(fileLocation))
            {
                string[] lines = File.ReadAllLines(fileLocation);

                foreach (string line in lines)
                {
                    int id = int.Parse(line.Substring(0).Split(',')[0]);
                    string taskText = line.Substring(1).Split(',')[1];
                    Task newTask = new Task(id, taskText);
                    tasksList.Add(newTask);
                }
            }
        }
        private static void SaveItems()
        {
            List<string> allTasks = new List<string>();
            foreach (Task task in tasksList)
            {
                allTasks.Add(task.GetId() + "," + task.GetTaskText());
            }

            File.WriteAllLines(fileLocation, allTasks);
        }

    }
}
