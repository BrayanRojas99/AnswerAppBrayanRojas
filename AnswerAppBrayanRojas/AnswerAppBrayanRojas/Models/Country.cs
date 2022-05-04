using System;
using System.Collections.Generic;
using System.Text;

namespace AnswerAppBrayanRojas.Models
{
    public class Country
    {
        public Country()
        {
            Users = new HashSet<User>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; }

        public virtual ICollection<User> Users { get; set; }

    }
}
