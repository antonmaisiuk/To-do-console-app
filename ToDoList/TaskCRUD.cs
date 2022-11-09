using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace ToDoList
{
    class TaskCRUD
    {
        
        public static void AddTask(List<Task> tasksList)
        {

            ConsoleClear.Clear();
            Console.SetCursorPosition(0, 10);
            WriteLine(" Please, write you task below.");

            string newTaskText = ReadLine();
            while (newTaskText == "")
            {
                //Clear();
                WriteLine(" Sorry, task cannot be empty. Write you task below.");

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
            tasksList.Add(new Task(newId, newTaskText));


            string[] options = { "Add another task", "Back" };

            Menu addMenu = new Menu(" Task has been succesfully added", options);
            ConsoleClear.Clear();
            Console.SetCursorPosition(0, 10);

            int selectedIndex = addMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    AddTask(tasksList);
                    break;
                case 1:
                    ToDo.RunMainMenu();
                    break;

            }
        }


        public static void EditTask(List<Task> tasksList)
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
                        AddTask(tasksList);
                        break;
                    case 1:
                        ToDo.RunMainMenu();
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

                Write("\n Write ID of task to edit: ");
                int idToEdit = Int32.Parse(ReadLine());

                while (!tasksList.Exists(task => task.id == idToEdit))
                {
                    Write(" Wrong ID. Try ID of task to edit again: ");
                    idToEdit = Int32.Parse(ReadLine());
                }

                WriteLine("\n Please, write new task text below");
                string newTaskText = ReadLine();

                tasksList.ForEach(task =>
                {
                    if (task.GetId() == idToEdit)
                    {
                        task.SetTaskText(newTaskText);
                    }
                });


                WriteLine("\n Task with ID = " + idToEdit + " has been succesfully edited");

                WriteLine("\n\n Press Enter to back to menu.");
                ConsoleKeyInfo keyInfo = ReadKey(true);
                ConsoleKey keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.Enter)
                {
                    ToDo.RunMainMenu();
                }
            }

        }


        public static void DeleteTask(List<Task> tasksList)
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
                        AddTask(tasksList);
                        break;
                    case 1:
                        ToDo.RunMainMenu();
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

                WriteLine("\n\n Press Enter to back to menu.");
                ConsoleKeyInfo keyInfo = ReadKey(true);
                ConsoleKey keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.Enter)
                {
                    ToDo.RunMainMenu();
                }

            }

        }

        public static void MarkAsDone(List<Task> tasksList)
        {
            ConsoleClear.Clear();
            SetCursorPosition(0, 10);

            if (tasksList.Count() == 0)
            {
                WriteLine(" You don't have task to do.\n");

                string[] options = { "Add task", "Back to menu" };
                Menu viewMenu = new Menu(" Menu", options);
                int selectedIndex = viewMenu.RunWithPrompt();

                switch (selectedIndex)
                {
                    case 0:
                        AddTask(tasksList);
                        break;
                    case 1:
                        ToDo.RunMainMenu();
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

                Write(" Write ID of done task: ");
                int idToDone = Int32.Parse(ReadLine());

                while (!tasksList.Exists(task => task.id == idToDone))
                {
                    Write(" Wrong ID. Try write ID of task again: ");
                    idToDone = Int32.Parse(ReadLine());
                }


                for (int i = tasksList.Count - 1; i >= 0; i--)
                {
                    if (tasksList[i].GetId() == idToDone)
                    {
                        tasksList.Remove(tasksList[i]);
                    }
                }                
                WriteLine("\n Task with ID = " + idToDone + " has been succesfully done");

                WriteLine("\n\n Press Enter to back to menu.");
                ConsoleKeyInfo keyInfo = ReadKey(true);
                ConsoleKey keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.Enter)
                {
                    ToDo.RunMainMenu();
                }
            }

        }
    }
}
