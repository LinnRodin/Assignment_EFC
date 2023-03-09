using Assignment_EFC.Models;
using Assignment_EFC.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_EFC.Services
{
    public class MenuService
    {
        public async Task CreateNewTicketAsync()
        {
            var ticket = new Ticket();

            Console.Write("Enter description of the ticket: ");
            ticket.Description = Console.ReadLine() ?? "";

            Console.Write("Enter firstname of the customer: ");
            ticket.FirstName = Console.ReadLine() ?? "";

            Console.Write("Enter lastname of the customer: ");
            ticket.LastName = Console.ReadLine() ?? "";

            Console.Write("Enter email of the customer: ");
            ticket.Email = Console.ReadLine() ?? "";

            Console.Write("Enter phonenumber of the customer: ");
            ticket.PhoneNumber = Console.ReadLine() ?? "";
            
            Console.Write("Enter streetname of the customer: ");
            ticket.StreetName = Console.ReadLine() ?? "";

            Console.Write("Enter postal code of the customer: ");
            ticket.PostalCode = Console.ReadLine() ?? "";

            Console.Write("Enter city of the customer: ");
            ticket.City = Console.ReadLine() ?? "";

            Console.Write("Status (open, ongoing, closed): ");
            Enum.TryParse(Console.ReadLine(), out TicketStatus status);
            ticket.Status = status;

            ticket.CreatedAt = DateTime.Now;


            //Saves ticket to the database

            await TicketService.SaveChangesAsync(ticket);

        }

        public async Task ListAllTicketsAsync()
        {   

            var tickets = await TicketService.GetAllAsync();

            if (tickets.Any())
            {
                foreach (Ticket ticket in tickets)
                {
                    Console.WriteLine($"Ticket ID: {ticket.Id}");
                    Console.WriteLine($"Description: {ticket.Description}");
                    Console.WriteLine($"Status: {ticket.Status}");
                    Console.WriteLine($"Customer ID: {ticket.CustomerId}");
                    Console.WriteLine($"Created At: {ticket.CreatedAt}");

                    Console.WriteLine("Comments:");

                    var comments = await CommentService.GetAllCommentsAsync(ticket.Id);

                    if (comments.Any())
                    {   
                        foreach (Comment comment in comments)
                        {
                            Console.WriteLine($"  Comment ID: {comment.Id}");
                            Console.WriteLine($"  Timestamp: {comment.Timestamp}");
                            Console.WriteLine($"  Customer ID: {comment.CustomerId}");
                            Console.WriteLine($"  Ticket ID: {comment.TicketId}");
                            Console.WriteLine($"  Text: {comment.Text}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No comments for this ticket.");
                    }

                    Console.WriteLine("");
                }
            }
            else
            {
                Console.WriteLine("No tickets in the database.");
                Console.WriteLine("");
            }
        }


        public async Task ShowSpecificTicketAsync()
        {
            Console.Write("Please enter ticket ID: ");
            var ticketId = int.Parse(Console.ReadLine() ?? "0");

            if (ticketId != 0)
            {
                var ticket = await TicketService.GetAsync(ticketId);

                if (ticket != null)
                {
                    Console.WriteLine($"Ticket ID: {ticket.Id}");
                    Console.WriteLine($"Description: {ticket.Description}");
                    Console.WriteLine($"Status: {ticket.Status}");
                    Console.WriteLine($"Customer ID: {ticket.CustomerId}");
                    Console.WriteLine($"Created At: {ticket.CreatedAt}");

                    Console.WriteLine("Comments:");

                    Console.Write("Please enter comment ID: ");
                    int commentId;
                    if (!int.TryParse(Console.ReadLine(), out commentId))
                    {
                        Console.WriteLine("Invalid comment ID. Please enter a valid integer.");
                        return;
                    }

                    var comments = await CommentService.GetCommentByIdAsync(ticketId, commentId);

                    if (comments != null)
                    {
                        Console.WriteLine($"  Comment ID: {comments.Id}");
                        Console.WriteLine($"  Timestamp: {comments.Timestamp}");
                        Console.WriteLine($"  Customer ID: {comments.CustomerId}");
                        Console.WriteLine($"  Ticket ID: {comments.TicketId}");
                        Console.WriteLine($"  Text: {comments.Text}");
                    }
                    else
                    {
                        Console.WriteLine($"No comment with ID {commentId} was found for ticket {ticketId}.");
                    }

                    Console.WriteLine("");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"No ticket with ID {ticketId} was found.");
                    Console.WriteLine("");
                }
            }
            else
            {
                Console.WriteLine("No ticket ID was entered.");
                Console.WriteLine("");
            }
        }


        public async Task UpdateSpecificTicketAsync()
        {
            Console.Write("Enter ticket ID: ");
            var ticketIdStr = Console.ReadLine();
            if (!int.TryParse(ticketIdStr, out var ticketId))
            {
                Console.WriteLine($"Invalid ticket ID.");
                return;
            }

            var ticket = await TicketService.GetAsync(ticketId);
            if (ticket != null)
            {
                Console.WriteLine("Please enter the information in the fields you want to update. \n");

                Console.Write("Description of ticket: ");
                ticket.Description = Console.ReadLine()!;

                Console.Write("Additional Comments for changes: ");
                var commentText = Console.ReadLine()!;

                var comment = new Comment
                {
                    Text = commentText,
                    TicketId = ticketId
                };

                await CommentService.AddCommentAsync(ticketId, comment);

                Console.Write("Status (Open, Ongoing, Closed): ");
                if (!Enum.TryParse(Console.ReadLine(), out TicketStatus status))
                {
                    Console.WriteLine($"Invalid status.");
                    return;
                }
                ticket.Status = status;

                await TicketService.UpdateAsync(ticket);
                Console.WriteLine($"Ticket {ticket.Id} has been updated.");
            }
            else
            {
                Console.WriteLine($"Ticket with ID {ticketId} not found.");
            }
        }



        public async Task DeleteSpecificTicketAsync()
        {
            Console.Write("Enter ticket ID: ");
            var ticketIdStr = Console.ReadLine();
            if (!int.TryParse(ticketIdStr, out var ticketId))
            {
                Console.WriteLine($"Invalid ticket ID.");
                return;
            }

            var ticket = await TicketService.GetAsync(ticketId);
            if (ticket != null)
            {
                Console.WriteLine($"Are you sure you want to delete the following ticket? (Y/N) \n");
                Console.WriteLine($"Ticket ID: {ticket.Id}");
                Console.WriteLine($"Description: {ticket.Description}");
                Console.WriteLine($"Status: {ticket.Status}");
                Console.WriteLine($"Created: {ticket.CreatedAt}");
                Console.WriteLine($"Customer ID: {ticket.CustomerId}\n");

                var confirmation = Console.ReadLine()!.ToLower();
                if (confirmation == "y")
                {
                    await TicketService.DeleteAsync(ticketId);
                    Console.WriteLine($"Ticket {ticketId} has been deleted.\n");
                }
                else
                {
                    Console.WriteLine($"Ticket {ticketId} was not deleted.\n");
                }
            }
            else
            {
                Console.WriteLine($"Ticket {ticketId} not found.");
            }
        }


    }
}
