using System;
using System.Collections.Generic;
using static TodoApi.Models.Enums;

namespace TodoApi.Models
{
    public partial class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public Status Status { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
