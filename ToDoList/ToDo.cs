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
            string[] options = { "Add task", "Edit task", "Delete task", "View to do list", "Exit" };
            Menu mainMenu = new Menu(prompt, options);
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

            //WriteLine("\n\nPress Enter to back to menu.");
            //ConsoleKeyInfo keyInfo = ReadKey(true);
            //ConsoleKey keyPressed = keyInfo.Key;

            //if (keyPressed == ConsoleKey.Enter)
            //{
            //    RunMainMenu();
            //}

            string[] options = { "Mark task as done", "Back to menu" };
            Menu viewMenu = new Menu("\nMenu", options);
            int selectedIndex = viewMenu.RunWithPrompt();

            switch (selectedIndex)
            {
                case 0:
                    MarkAsDone();
                    break;
                case 1:
                    RunMainMenu();
                    break;

            }

        }

        private void EditTask()
        {
            Clear();
            WriteLine("There are your tasks.\n");
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
            //for (int i = tasksList.Count - 1; i >= 0; i--)
            //{
            //    if (tasksList[i].GetId() == idToDelete)
            //    {
            //        tasksList
            //    }
            //}
            WriteLine("\nTask with ID = " + idToEdit + " has been succesfully edited");

            WriteLine("\n\nPress Enter to back to menu.");
            ConsoleKeyInfo keyInfo = ReadKey(true);
            ConsoleKey keyPressed = keyInfo.Key;

            if (keyPressed == ConsoleKey.Enter)
            {
                RunMainMenu();
            }
        }

        private void MarkAsDone()
        {
            Clear();
            WriteLine("There are your tasks.\n");
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
