using System;
namespace ToDoAPI.Models
{
    public class ToDoTask
    {
        public int ToDoTaskId { get; set; }
        public string ToDoTaskText { get; set; }
        public bool IsComplete { get; set; }
        public Category Category { get; set; }
    }
}

