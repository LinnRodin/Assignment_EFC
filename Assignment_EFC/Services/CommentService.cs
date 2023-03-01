﻿using Assignment_EFC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_EFC.Services
{
    public class CommentService
    {
        public async Task AddCommentAsync(int ticketId, Comment comment)
        {
            var ticket = await TicketService.GetAsync(ticketId);

            if (ticket != null)
            {
                ticket.Comments.Add(comment);
                await TicketService.UpdateAsync(ticket);
            }
        }

        public async Task UpdateCommentAsync(int ticketId, Comment comment)
        {
            var ticket = await TicketService.GetAsync(ticketId);

            if (ticket != null)
            {
                var existingComment = ticket.Comments.FirstOrDefault(c => c.Id == comment.Id);

                if (existingComment != null)
                {
                    existingComment.Text = comment.Text;
                    existingComment.Timestamp = comment.Timestamp;

                    await TicketService.UpdateAsync(ticket);
                }
            }
        }

        public async Task DeleteCommentAsync(int ticketId, int commentId)
        {
            var ticket = await TicketService.GetAsync(ticketId);

            if (ticket != null)
            {
                var commentToRemove = ticket.Comments.FirstOrDefault(c => c.Id == commentId);

                if (commentToRemove != null)
                {
                    ticket.Comments.Remove(commentToRemove);

                    await TicketService.UpdateAsync(ticket);
                }
            }
        }
    }

}