using Assignment_EFC.Contexts;
using Assignment_EFC.Models;
using Assignment_EFC.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_EFC.Services
{
    public class TicketService
    {
        private static DataContext _context = new DataContext();


        public static async Task SaveChangesAsync(Ticket ticket, Customer customer)
        {
            var _addressEntity = new AddressEntity
            {
                StreetName = ticket.StreetName,
                PostalCode = ticket.PostalCode,
                City = ticket.City
            };

            _context.Add(_addressEntity);
            await _context.SaveChangesAsync();

            var _customerEntity = new CustomerEntity
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                AddressId = _addressEntity.Id,

            };

            _context.Add(_customerEntity);
            await _context.SaveChangesAsync();

            var _ticketEntity = new TicketEntity
            {
                Description = ticket.Description,
                Status = ticket.Status,
                CustomerId = _customerEntity.Id,
                CreatedAt = ticket.CreatedAt,
                UpdatedAt = ticket.UpdatedAt,
                Comments = new List<CommentEntity>()
            };

            _context.Add(_ticketEntity);
            await _context.SaveChangesAsync();

            // Save comments for the ticket
            foreach (var comment in ticket.Comments)
            {
                var _commentEntity = new CommentEntity
                {
                    Text = comment.Text,
                    Timestamp = comment.Timestamp,
                    TicketId = _ticketEntity.Id,
                   
                };

                _context.Add(_commentEntity);

                // Associate the comment entity with the ticket entity
                _ticketEntity.Comments.Add(_commentEntity);

                // Save the comment to the CommentEntity if it doesn't already exist
                if (_context.Comments.FirstOrDefault(c => c.Id == _commentEntity.Id) == null)
                {
                    _context.Comments.Add(_commentEntity);
                    await _context.SaveChangesAsync();
                }
            }

            await _context.SaveChangesAsync();
        }



        public static async Task<IEnumerable<Ticket>> GetAllAsync()
        {
            var _tickets = new List<Ticket>();

            foreach (var _ticket in await _context.Tickets.Include(x => x.Comments).Include(x => x.Customer).ToListAsync())
                _tickets.Add(new Ticket
                {
                    Id = _ticket.Id,
                    Description = _ticket.Description,
                    Status = Enum.Parse<TicketStatus>(_ticket.Status.ToString()),
                    CustomerId = _ticket.CustomerId,
                    CreatedAt = _ticket.CreatedAt,
                    UpdatedAt = _ticket.UpdatedAt,
                    Customer = new Customer
                    {
                        Id = _ticket.Customer.Id,
                        FirstName = _ticket.Customer.FirstName,
                        LastName = _ticket.Customer.LastName,
                        Email = _ticket.Customer.Email,
                        PhoneNumber = _ticket.Customer.PhoneNumber
                    },
                    Comments = _ticket.Comments.Select(x => new Comment
                    {
                        Id = x.Id,
                        Text = x.Text,
                        Timestamp = x.Timestamp,
                        TicketId = x.TicketId,
                    }).ToList()
                });

            return _tickets;
        }


        public static async Task<Ticket> GetAsync(int id)
        {
            var _ticket = await _context.Tickets
                .Include(x => x.Comments)
                .Include(x => x.Customer)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (_ticket != null)
            {
                return new Ticket
                {
                    Id = _ticket.Id,
                    Description = _ticket.Description,
                    Status = (TicketStatus)_ticket.Status,
                    CustomerId = _ticket.CustomerId,
                    CreatedAt = _ticket.CreatedAt,
                    UpdatedAt = _ticket.UpdatedAt,
                    Customer = new Customer
                    {
                        Id = _ticket.Customer.Id,
                        FirstName = _ticket.Customer.FirstName,
                        LastName = _ticket.Customer.LastName,
                        Email = _ticket.Customer.Email,
                        PhoneNumber = _ticket.Customer.PhoneNumber
                    },
                    Comments = _ticket.Comments.Select(x => new Comment
                    {
                        Id = x.Id,
                        Text = x.Text,
                        Timestamp = x.Timestamp,
                        TicketId = x.TicketId,
                    }).ToList()
                };
            }
            else
            {
                return null!;
            }
        }



        public static async Task UpdateAsync(Ticket ticket)
        {

            // Get the ticket entity from the database, including its comments
            var _ticketEntity = await _context.Tickets.Include(x => x.Comments).FirstOrDefaultAsync(x => x.Id == ticket.Id);

            // Check if the ticket exists
            if (_ticketEntity != null)
            {
                // Update the ticket properties if they're not null or default
                if (!string.IsNullOrEmpty(ticket.Description))
                    _ticketEntity.Description = ticket.Description;

                if(ticket.Status != TicketStatus.NotStarted)
                    _ticketEntity.Status = ticket.Status;

                if (ticket.CustomerId != 0)
                    _ticketEntity.CustomerId = ticket.CustomerId;

                if (ticket.UpdatedAt != DateTime.MinValue)
                    _ticketEntity.UpdatedAt = ticket.UpdatedAt;

                // Update comments for the ticket
                foreach (var comment in ticket.Comments)
                {
                    // Get the comment entity from the database
                    var _commentEntity = await _context.Comments.FirstOrDefaultAsync(x => x.Id == comment.Id);

                    // Check if the comment exists
                    if (_commentEntity != null)
                    {
                        // Update the comment properties if they're not null or default
                        if (!string.IsNullOrEmpty(comment.Text))
                            _commentEntity.Text = comment.Text;

                        if (comment.Timestamp != DateTime.MinValue)
                            _commentEntity.Timestamp = comment.Timestamp;

                        // Mark the comment as modified in the context
                        _context.Update(_commentEntity);
                    }
                    else
                    {
                        // If the comment doesn't exist, create a new comment entity
                        _commentEntity = new CommentEntity
                        {
                            Text = comment.Text,
                            Timestamp = DateTime.Now,
                            TicketId = _ticketEntity.Id
                        };

                        // Add the comment entity to the context
                        _context.Comments.Add(_commentEntity);
                    }
                }

                // Save the changes to the database
                await _context.SaveChangesAsync();
            }
        }


        public static async Task DeleteAsync(int ticketId)
        {
            var ticket = await _context.Tickets
                .Include(x => x.Comments)
                .FirstOrDefaultAsync(x => x.Id == ticketId);

            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();
            }
        }



    }
}
