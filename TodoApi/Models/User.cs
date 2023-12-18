using System;
using System.Collections.Generic;

namespace TodoApi.Models
{
    public partial class User
    {
        public User()
        {
            Todos = new HashSet<Todo>();
        }

        public int Id { get; set; }
        public string? Username { get; set; }
        public string? PasswordSlat { get; set; }
        public string? PasswordHash { get; set; }

        public virtual ICollection<Todo> Todos { get; set; }
    }
}
