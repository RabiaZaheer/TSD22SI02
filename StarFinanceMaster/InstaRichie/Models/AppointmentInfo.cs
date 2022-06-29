using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartFinance.Models
{
    class AppointmentInfo
    {
        public int AppointmentID { get; set; }

        public string EventName { get; set; }

        public string Location { get; set; }

        public string EventDate { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }
    }
}
