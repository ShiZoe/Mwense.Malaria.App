using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mwense.Malaria.App.Models
{
    public class Users
    {
        [AutoIncrement, PrimaryKey] 
        public int Id { get; set; }
        [Unique, NotNull]
        public string Username { get; set; }
        [NotNull]
        public string Password { get; set; }
        [NotNull]
        public string Firstname { get; set; } 
        [NotNull]
        public string Lastname { get; set; }
        public string Phonenumber { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
