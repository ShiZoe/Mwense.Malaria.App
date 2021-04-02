using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mwense.Malaria.App.Models
{
    public class FollowUp
    {
        [PrimaryKey, AutoIncrement]
        public int DBId { get; set; }
        public int OPDNumber { get; set; }
        public string TreatmentStatus { get; set; }
        public string CordLong { get; set; }
        public string CordLang { get; set; }
        public DateTime FollowupDate { get; set; }
    }
}
