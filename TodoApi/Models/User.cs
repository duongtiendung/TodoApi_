﻿using System;
using System.Collections.Generic;

namespace TodoApi.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? PasswordSlat { get; set; }
        public string? PasswordHash { get; set; }
    }
}
