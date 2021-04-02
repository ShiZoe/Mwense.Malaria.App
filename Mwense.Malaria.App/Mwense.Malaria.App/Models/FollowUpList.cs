using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mwense.Malaria.App.Models
{
    public class FollowUpList
    {
        [AutoIncrement, PrimaryKey]
        public int ID { get; set; }
        public int OPDNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string HouseNumber { get; set; }
        public string Village { get; set; }
        public string PhoneNumber { get; set; }
        public string MalariaResults { get; set; }
        public DateTime TestDate { get; set; }
        public bool IsReviewed { get; set; }
    }
}
