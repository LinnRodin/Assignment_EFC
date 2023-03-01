using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Assignment_EFC.Models.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    internal class CustomerEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; } = null!;

        [StringLength(50)]
        public string LastName { get; set; } = null!;

        [StringLength(100)]
        public string Email { get; set; } = null!;

        [Column(TypeName = "char(13)")]
        public string? PhoneNumber { get; set; }

        [Required]
        public int AddressId { get; set; }
        public AddressEntity Address { get; set; } = null!;

        public ICollection<TicketEntity> Tickets { get; set; } = new HashSet<TicketEntity>();

        public ICollection<CommentEntity> Comments { get; set; } = new HashSet<CommentEntity>();

    }




}
