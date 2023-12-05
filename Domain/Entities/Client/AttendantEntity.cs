using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Client
{
    [Table("attendant")]
    public class AttendantEntity
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public long ClientId { get; set; }

        public ClientEntity Client { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
