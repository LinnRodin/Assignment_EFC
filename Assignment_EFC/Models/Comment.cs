using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_EFC.Models
{
    internal class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public DateTime Timestamp { get; set; }
        public int CustomerId { get; set; }
        public int TicketId { get; set; }
    }
}
