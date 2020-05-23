using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ExpirateDate { get; set; }
        public string Description { get; set; }
        public int PercentageComplete { get; set; }
    }

    public class CreateTodoDto
        {
            [Required]
            public string Title { get; set; }
            [Required]
            public string ExpirateDate { get; set; }
            [Required]
            public string Description { get; set; }
            [Required]
            public int PercentageComplete { get; set; }
        }

    public class GetTodoDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ExpirateDate { get; set; }
        public string Description { get; set; }
        public int PercentageComplete { get; set; }
    }
}


