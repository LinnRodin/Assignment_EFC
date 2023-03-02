﻿using Assignment_EFC.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_EFC.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public TicketStatus Status { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<Comment> Comments { get; set; }

        public Ticket()
        {
            Comments = new List<Comment>();
        }
    }
}
