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
        string fileLocation = "todo-items.txt";
        List<Task> tasksList = new List<Task>();
        public string prompt = @"
    ___________          .___       .__  .__          __   
    \__    ___/___     __| _/____   |  | |__| _______/  |_ 
      |    | /  _ \   / __ |/  _ \  |  | |  |/  ___/\   __\
      |    |(  <_> ) / /_/ (  <_> ) |  |_|  |\___ \  |  |  
      |____| \____/  \____ |\____/  |____/__/____  > |__|  
                      \/                     \/        
    (use UpArrow and DownArrow to select)
";

        public void Start()
        {
            Title = "Todo List - Productivity App!";
            LoadItems();
            RunMainMenu();
        }

        private void RunMainMenu()
        {
            //Console.BackgroundColor = ConsoleBgColor;
            
            string[] options = { "Add task", "Edit task", "Delete task", "View to do list", "Settings","Exit" };
            Menu mainMenu = new Menu(prompt + "\n Main menu", options);
            int selectedIndex = mainMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    AddTask();
                    break;
                case 1:
                    EditTask();
                    break;
                case 2:
                    DeleteTask();
                    break;
                case 3:
                    ViewTodoList();
                    break;
                case 4:
                    Settings();
                    break;
                case 5:
                    ExitApp();
                    break;
            }
        }
        private void AddTask()
        {
            Clear();
            WriteLine(prompt + "\n Please, write you task below.");
            string newTaskText = ReadLine();
            while (newTaskText == "")
            {
                Clear();
                WriteLine(prompt + "\n Sorry, task cannot be empty. Write you task below.");
                newTaskText = ReadLine();
            }

            int newId;
            if (tasksList.Count() == 0)
            {
                newId = 1;
            }
            else
            {
                newId = tasksList.Last().id + 1;
            }
            tasksList.Add(new Task(newId,newTaskText));

            string[] options = { "Add another task", "Back" };            
            Menu addMenu = new Menu(prompt + "\n Task has been succesfully added", options );
            int selectedIndex = addMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    AddTask();
                    break;
                case 1:
                    RunMainMenu();
                    break;
               
            }
        }

        private void DeleteTask()
        {
            Clear();

            if (tasksList.Count() == 0)
            {
                WriteLine(prompt + "\n You don't have task to do.\n");

                string[] options = { "Add task", "Back to menu" };
                Menu viewMenu = new Menu("\n Menu", options);
                int selectedIndex = viewMenu.RunWithPrompt();

                switch (selectedIndex)
                {
                    case 0:
                        AddTask();
                        break;
                    case 1:
                        RunMainMenu();
                        break;

                }
            }
            else
            {
                WriteLine(prompt + "\n There are your tasks.\n");
                WriteLine("{0,5} {1,-50}", "ID", "Task\n");
                tasksList.ForEach(task =>
                {
                    WriteLine("{0,5} {1,-50}", task.GetId(), task.GetTaskText());
                });


                Write("\n Write ID of task to delete: ");
                int idToDelete = Int32.Parse(ReadLine());            

                while (!tasksList.Exists(task => task.id == idToDelete))
                {
                    Write("\n Wrong ID. Try ID of task to delete again: ");
                    idToDelete = Int32.Parse(ReadLine());
                } 
            
                for (int i = tasksList.Count - 1; i >= 0; i--)
                {
                    if (tasksList[i].GetId() == idToDelete)
                    {
                       tasksList.Remove(tasksList[i]);                    
                    }
                }
                WriteLine("\n Task with ID = " + idToDelete + " has been succesfully deleted");

                WriteLine("\n\nPress Enter to back to menu.");
                ConsoleKeyInfo keyInfo = ReadKey(true);
                ConsoleKey keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.Enter)
                {
                    RunMainMenu();
                }            

            }

        }

        private void ViewTodoList()
        {
            Clear();

            if (tasksList.Count() == 0)
            {
                WriteLine(prompt + "\n You don't have task to do.\n");

                string[] options = { "Add task", "Back to menu" };
                Menu viewMenu = new Menu("\n Menu", options);
                int selectedIndex = viewMenu.RunWithPrompt();

                switch (selectedIndex)
                {
                    case 0:
                        AddTask();
                        break;
                    case 1:
                        RunMainMenu();
                        break;

                }
            }
            else
            {
                WriteLine(prompt + "\n There are your tasks.\n");
                WriteLine("{0,5} {1,-50}", "ID", "Task\n");
                tasksList.ForEach(task =>
                {
                    WriteLine("{0,5} {1,-50}", task.GetId(), task.GetTaskText());
                });

                string[] options = { "Add task", "Mark task as done", "Back to menu" };
                Menu viewMenu = new Menu("\n Menu", options);
                int selectedIndex = viewMenu.RunWithPrompt();

                switch (selectedIndex)
                {
                    case 0:
                        AddTask();
                        break;
                    case 1:
                        MarkAsDone();
                        break;
                    case 2:
                        RunMainMenu();
                        break;

                }
            }       

        }

        private void EditTask()
        {
            Clear();

            if (tasksList.Count() == 0)
            {
                WriteLine(prompt + "\n You don't have task to do.\n");

                string[] options = { "Add task", "Back to menu" };
                Menu viewMenu = new Menu("\n Menu", options);
                int selectedIndex = viewMenu.RunWithPrompt();

                switch (selectedIndex)
                {
                    case 0:
                        AddTask();
                        break;
                    case 1:
                        RunMainMenu();
                        break;

                }
            }
            else
            {
                WriteLine(prompt + "\n There are your tasks.\n");
                WriteLine("{0,5} {1,-50}", "ID", "Task\n");
                tasksList.ForEach(task =>
                {
                    WriteLine("{0,5} {1,-50}", task.GetId(), task.GetTaskText());
                });

                Write("\nWrite ID of task to edit: ");
                int idToEdit = Int32.Parse(ReadLine());

                while (!tasksList.Exists(task => task.id == idToEdit))
                {
                    Write("Wrong ID. Try ID of task to edit again: ");
                    idToEdit = Int32.Parse(ReadLine());
                }

                WriteLine("\nPlease, write new task text below");
                string newTaskText = ReadLine();

                tasksList.ForEach(task =>
                {
                    if (task.GetId() == idToEdit)
                    {
                        task.SetTaskText(newTaskText);
                    }
                });


                WriteLine("\nTask with ID = " + idToEdit + " has been succesfully edited");

                WriteLine("\n\nPress Enter to back to menu.");
                ConsoleKeyInfo keyInfo = ReadKey(true);
                ConsoleKey keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.Enter)
                {
                    RunMainMenu();
                }
            }
                
        }

        private void MarkAsDone()
        {
            Clear();

            if (tasksList.Count() == 0)
            {
                WriteLine(prompt + "\n You don't have task to do.\n");

                string[] options = { "Add task", "Back to menu" };
                Menu viewMenu = new Menu("\n Menu", options);
                int selectedIndex = viewMenu.RunWithPrompt();

                switch (selectedIndex)
                {
                    case 0:
                        AddTask();
                        break;
                    case 1:
                        RunMainMenu();
                        break;

                }
            }
            else
            {
                WriteLine(prompt + "\n There are your tasks.\n");
                WriteLine("{0,5} {1,-50}", "ID", "Task\n");
                tasksList.ForEach(task =>
                {
                    WriteLine("{0,5} {1,-50}", task.GetId(), task.GetTaskText());
                });

                Write("Write ID of done task: ");
                int idToDone = Int32.Parse(ReadLine());

                while (!tasksList.Exists(task => task.id == idToDone))
                {
                    Write("Wrong ID. Try write ID of task again: ");
                    idToDone = Int32.Parse(ReadLine());
                }

                //WriteLine("\n Please, write new task text below");
                //string newTaskText = ReadLine();

                for (int i = tasksList.Count - 1; i >= 0; i--)
                {
                    if (tasksList[i].GetId() == idToDone)
                    {
                        tasksList.Remove(tasksList[i]);
                    }
                }
                //for (int i = tasksList.Count - 1; i >= 0; i--)
                //{
                //    if (tasksList[i].GetId() == idToDelete)
                //    {
                //        tasksList
                //    }
                //}
                WriteLine("\nTask with ID = " + idToDone + " has been succesfully done");

                WriteLine("\n\nPress Enter to back to menu.");
                ConsoleKeyInfo keyInfo = ReadKey(true);
                ConsoleKey keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.Enter)
                {
                    RunMainMenu();
                }
            }
                
        }

        private void ExitApp()
        {
            WriteLine("\nPress any key to exit...");
            ReadKey(true);
            SaveItems();
            Environment.Exit(0);
        }


        private void Settings()
        {
            string[] options = { "Menu background color", "Back" };
            Menu mainMenu = new Menu(prompt + "\nSettings", options);
            int selectedIndex = mainMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    ChangeMenuBgColor();
                    break;
                case 1:                    
                    RunMainMenu();
                    break;
            }
        }

        private void ChangeMenuBgColor()
        {
            string[] options = { "White", "Blue", "Red" };
            Menu mainMenu = new Menu(prompt + "\n Choose color", options);
            int selectedIndex = mainMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    Menu.MenuBgColor = ConsoleColor.White;
                    Menu.TextColor = ConsoleColor.Black;
                    RunMainMenu();
                    break;
                case 1:
                    Menu.MenuBgColor = ConsoleColor.Blue;
                    Menu.TextColor = ConsoleColor.White;
                    RunMainMenu();
                    break;
                case 2:
                    Menu.MenuBgColor = ConsoleColor.Red;
                    Menu.TextColor = ConsoleColor.White;
                    RunMainMenu();
                    break;
            }
        }

        //private void ChangeConsoleBgColor()
        //{
        //    string[] options = { "White", "Blue", "Red" };
        //    Menu mainMenu = new Menu("Choose color", options);
        //    int selectedIndex = mainMenu.Run();

        //    switch (selectedIndex)
        //    {
        //        case 0:
        //            Console.BackgroundColor = ConsoleColor.White;
        //            Console.ForegroundColor = ConsoleColor.Black;
        //            Menu.MenuBgColor = ConsoleColor.Black;
        //            Menu.TextColor = ConsoleColor.White;
        //            RunMainMenu();
        //            break;
        //        case 1:
        //            Menu.MenuBgColor = ConsoleColor.Blue;
        //            Menu.TextColor = ConsoleColor.White;
        //            RunMainMenu();
        //            break;
        //        case 2:
        //            Menu.MenuBgColor = ConsoleColor.Red;
        //            Menu.TextColor = ConsoleColor.White;
        //            RunMainMenu();
        //            break;
        //    }
        //}

        void LoadItems()
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
        void SaveItems()
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
