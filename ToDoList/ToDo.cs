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
        public void Start()
        {
            Title = "Todo List - Productivity App!";
            LoadItems();
            RunMainMenu();
        }

        private void RunMainMenu()
        {
            string prompt = @"
 /$$$$$$$$              /$$                 /$$       /$$             /$$    
|__  $$__/             | $$                | $$      |__/            | $$    
   | $$  /$$$$$$   /$$$$$$$  /$$$$$$       | $$       /$$  /$$$$$$$ /$$$$$$  
   | $$ /$$__  $$ /$$__  $$ /$$__  $$      | $$      | $$ /$$_____/|_  $$_/     
   | $$| $$  | $$| $$  | $$| $$  | $$      | $$      | $$ \____  $$  | $$ /$$
   | $$|  $$$$$$/|  $$$$$$$|  $$$$$$/      | $$$$$$$$| $$ /$$$$$$$/  |  $$$$/
   |__/ \______/  \_______/ \______/       |________/|__/|_______/    \___/
    (use UpArrow and DownArrow to select)
";
            string[] options = { "Add Task", "Delete Task", "View todo list", "Exit" };
            Menu mainMenu = new Menu(prompt, options);
            int selectedIndex = mainMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    AddTask();
                    break;
                case 1:
                    DeleteTask();
                    break;
                case 2:
                    ViewTodoList();
                    break;
                case 3:
                    ExitApp();
                    break;
            }
        }
        private void AddTask()
        {
            Clear();
            WriteLine("Please, write you task below.");
            string newTaskText = ReadLine();
            while (newTaskText == "")
            {
                Clear();
                WriteLine("Sorry, task cannot be empty. Write you task below.");
                newTaskText = ReadLine();
            }

            int newId = tasksList.Last().id + 1;
            tasksList.Add(new Task(newId,newTaskText));

            string[] options = { "Add another task", "Back" };            
            Menu addMenu = new Menu("Task has been succesfully added", options );
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
            WriteLine("There are your tasks.\n");
            WriteLine("{0,5} {1,-50}", "ID", "Task\n");
            tasksList.ForEach(task =>
            {
                WriteLine("{0,5} {1,-50}", task.GetId(), task.GetTaskText());
            });


            Write("Write ID of task to delete: ");
            int idToDelete = Int32.Parse(ReadLine());            

            while (!tasksList.Exists(task => task.id == idToDelete))
            {
                Write("Wrong ID. Try ID of task to delete again: ");
                idToDelete = Int32.Parse(ReadLine());
            } 
            
            for (int i = tasksList.Count - 1; i >= 0; i--)
            {
                if (tasksList[i].GetId() == idToDelete)
                {
                   tasksList.Remove(tasksList[i]);                    
                }
            }
            WriteLine("\nTask with ID = " + idToDelete + " has been succesfully deleted");

            WriteLine("\n\nPress Enter to back to menu.");
            ConsoleKeyInfo keyInfo = ReadKey(true);
            ConsoleKey keyPressed = keyInfo.Key;

            if (keyPressed == ConsoleKey.Enter)
            {
                RunMainMenu();
            }


            //tasksList.ForEach(task =>
            //{
            //    if (task.GetId() == idToDelete)
            //    {
            //        tasksList.Remove(task);
            //        isDeleted = 1;
            //        break;
            //    }
            //});

            //Write("Write ID of task to delete: ");
            //int idToDelete = Int32.Parse(ReadLine());            
            //tasksList.ForEach(task =>
            //{
            //    if (task.GetId() == idToDelete)
            //    {
            //        tasksList.Remove(task);
            //        isDeleted = 1;
            //    }
            //});
            //if (isDeleted == 1)
            //{
            //    WriteLine("Task with ID = " + idToDelete + " has been succesfully deleted");
            //}
            //else
            //{
            //    WriteLine("Wrong ID. Try again");
            //}
        }

        private void ViewTodoList()
        {
            Clear();
            WriteLine("There are your tasks.\n");
            WriteLine("{0,5} {1,-50}", "ID", "Task\n");
            tasksList.ForEach(task =>
            {
                WriteLine("{0,5} {1,-50}", task.GetId(), task.GetTaskText());
            });

            WriteLine("\n\nPress Enter to back to menu.");
            ConsoleKeyInfo keyInfo = ReadKey(true);
            ConsoleKey keyPressed = keyInfo.Key;

            if (keyPressed == ConsoleKey.Enter)
            {
                RunMainMenu();
            }

            //string[] options = { "Back to menu", "Exit" };
            //Menu viewMenu = new Menu("Task has been added", options);
            //int selectedIndex = viewMenu.Run();

            //switch (selectedIndex)
            //{
            //    case 0:
            //        RunMainMenu();
            //        break;
            //    case 1:
            //        ExitApp();
            //        break;

            //}

        }

        private void ExitApp()
        {
            WriteLine("\nPress any key to exit...");
            ReadKey(true);
            SaveItems();
            Environment.Exit(0);
        }


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
