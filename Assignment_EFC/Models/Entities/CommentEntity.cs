using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Assignment_EFC.Models.Entities
{
    internal class CommentEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(500)")]
        public string Text { get; set; } = null!;

        [Required]
        public DateTime Timestamp { get; set; }

        [Required]
        public int TicketId { get; set; }
        public TicketEntity Ticket { get; set; } = null!;

        [Required]
        public int CustomerId { get; set; }
        public CustomerEntity Customer { get; set; } = null!;
    }




}
