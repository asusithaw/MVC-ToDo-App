using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcTodoApp.Entities;

namespace MvcTodoApp.ViewModels
{
    public class TaskViewModel
    {
        public IEnumerable<Task> Tasks { get; set; }
        public int TotalTasks { get; set; }
    }
}