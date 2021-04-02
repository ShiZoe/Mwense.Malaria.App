using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mwense.Malaria.App.Models
{
    class NotFollowedUp
    {
        [PrimaryKey, AutoIncrement]
        public int DBId { get; set; }
        public int OPDNumber { get; set; }
        public DateTime FollowUpDate { get; set; }
    }
}
