using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Client
{
    [Table("client")]
    public class ClientEntity
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public long ConsultorId { get; set; }

        public AttendantTokenEntity AttendantToken { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
