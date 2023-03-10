using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Assignment_EFC.Models.Entities
{
    public class TicketEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(500)")]
        public string Description { get; set; } = null!;

        [Required]
        public TicketStatus Status { get; set; }

        public ICollection<CommentEntity> Comments { get; set; } = new HashSet<CommentEntity>();

        [Required]
        public int CustomerId { get; set; }
        public CustomerEntity Customer { get; set; } = null!;

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }

    public enum TicketStatus
    {
        NotStarted,
        Started,
        Closed
    }



}
