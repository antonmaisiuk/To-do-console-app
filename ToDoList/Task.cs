using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    class Task
    {
        public int id;
        private string taskText;
        public Task(int id, string taskText)
        {
            this.id = id;
            this.taskText = taskText;
        }
        //public void SetId(int id)
        //{
        //    this.id = id;
        //}
        public int GetId()
        {
            return id;
        }
        public string GetTaskText()
        {
            return taskText;
        }
        public void SetTaskText(string taskText)
        {
            this.taskText = taskText;
        }

    }
}
