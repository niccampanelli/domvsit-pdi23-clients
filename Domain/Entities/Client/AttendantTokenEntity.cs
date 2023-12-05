using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Client
{
    [Table("attendant_token")]
    public class AttendantTokenEntity
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime ExpiresAt { get; set; }

        [Required]
        public long ClientId { get; set; }

        public ClientEntity Client { get; set; }
    }
}
