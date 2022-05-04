using System;
using System.Collections.Generic;
using System.Text;

namespace AnswerAppBrayanRojas.Models
{
    public class Chat
    {
        public long ChatId { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }

        public virtual User Receiver { get; set; }
        public virtual User Sender { get; set; }

    }
}
