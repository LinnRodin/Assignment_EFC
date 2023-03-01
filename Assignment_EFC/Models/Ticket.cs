using Assignment_EFC.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_EFC.Models
{
    internal class Ticket
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public TicketStatus Status { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public IEnumerable<Comment> Comments { get; set; } = null!;
    }
}
