using System;
using System.Collections.Generic;
using System.Text;

namespace AnswerAppBrayanRojas.Models
{
    public class AskStatus
    {
        public AskStatus()
        {
            Asks = new HashSet<Ask>();
        }

        public int AskStatusId { get; set; }
        public string AskStatus1 { get; set; }

        public virtual ICollection<Ask> Asks { get; set; }

    }
}
