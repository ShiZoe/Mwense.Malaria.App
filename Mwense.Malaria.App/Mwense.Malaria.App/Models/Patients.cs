using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mwense.Malaria.App.Models
{
    public class Patients
    {
        [AutoIncrement, PrimaryKey]
        public int DBId { get; set; }
        [Indexed]
        public int OPDNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string HouseNumber { get; set; }
        public string Village { get; set; }
        public string PhoneNumber { get; set; }

    }
}
