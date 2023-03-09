using Assignment_EFC.Contexts;
using Assignment_EFC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_EFC.Services
{
    public class CommentService
    {
        private static DataContext _context = new DataContext();

        public static async Task AddCommentAsync(int ticketId, Comment comment)
        {
            var ticket = await TicketService.GetAsync(ticketId);

            if (ticket == null)
            {
                throw new InvalidOperationException("Ticket not found.");
            }

            ticket.Comments.Add(comment);
            await TicketService.UpdateAsync(ticket);
        }

        public static async Task<List<Comment>> GetAllCommentsAsync(int ticketId)
        {
            var ticket = await TicketService.GetAsync(ticketId);

            if (ticket == null)
            {
                throw new InvalidOperationException("Ticket not found.");
            }

            return ticket.Comments.ToList();
        }

        public static async Task<Comment?> GetCommentByIdAsync(int ticketId, int commentId)
        {
            var ticket = await TicketService.GetAsync(ticketId);

            if (ticket == null)
            {
                throw new InvalidOperationException("Ticket not found.");
            }

            return ticket.Comments.FirstOrDefault(c => c.Id == commentId);
        }

        public static async Task UpdateCommentAsync(int ticketId, Comment comment)
        {
            var ticket = await TicketService.GetAsync(ticketId);

            if (ticket == null)
            {
                throw new InvalidOperationException("Ticket not found.");
            }

            var existingComment = ticket.Comments.FirstOrDefault(c => c.Id == comment.Id);

            if (existingComment == null)
            {
                throw new InvalidOperationException("Comment not found.");
            }

            existingComment.Text = comment.Text;
            existingComment.Timestamp = comment.Timestamp;

            await TicketService.UpdateAsync(ticket);
        }

        public static async Task DeleteCommentAsync(int ticketId, int commentId)
        {
            var ticket = await TicketService.GetAsync(ticketId);

            if (ticket == null)
            {
                throw new InvalidOperationException("Ticket not found.");
            }

            var commentToRemove = ticket.Comments.FirstOrDefault(c => c.Id == commentId);

            if (commentToRemove == null)
            {
                throw new InvalidOperationException("Comment not found.");
            }

            ticket.Comments.Remove(commentToRemove);
            await TicketService.UpdateAsync(ticket);
        }

        public static async Task AddCommentToTicketAsync(int ticketId, string text)
        {
            var comment = new Comment()
            {
                Text = text,
                Timestamp = DateTime.UtcNow
            };

            await AddCommentAsync(ticketId, comment);
        }
    }

}
