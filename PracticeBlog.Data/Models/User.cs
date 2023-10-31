﻿using System.Data;

namespace PracticeBlog.Data.Models
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public ICollection<Role> Roles { get; set; } = new List<Role>();
    }
}