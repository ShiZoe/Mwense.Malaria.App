using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mwense.Malaria.App.Models
{
    public class LabResults
    {
        [PrimaryKey, AutoIncrement]
        public int DBId { get; set; }
        public int OPDNumber { get; set; }
        public string MalariaResults { get; set; }
        public DateTime TestDate { get; set; }
    }
}
