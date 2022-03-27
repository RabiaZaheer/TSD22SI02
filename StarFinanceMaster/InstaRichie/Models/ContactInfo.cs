using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace StartFinance.Models
{
    class ContactInfo
    {
        [PrimaryKey, AutoIncrement]
        public int ContactlID { get; set; }

        [NotNull]
        public string FirstName { get; set; }

        [NotNull]
        public string LastName { get; set; }
        [NotNull]
        public string CompanyName { get; set; }
        [NotNull]
        public string MobileNo { get; set; }
    }
}
