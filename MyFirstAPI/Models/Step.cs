using System;
namespace ToDoAPI.Models
{
    public class Step
    {
        public int StepId { get; set; }
        public string StepText { get; set; }
        public bool IsComplete { get; set; }
        public int ToDoTaskId { get; set; }
    }
}

