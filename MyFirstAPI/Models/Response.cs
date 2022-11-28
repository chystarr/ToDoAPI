using System;
namespace ToDoAPI.Models
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public List<Category>? ReturnedCategories { get; set; }
        public List<ToDoTask>? ReturnedToDoTasks { get; set; }
        public List<Step>? ReturnedSteps { get; set; }
    }
}

