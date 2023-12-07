namespace Domain.Dto.Attendant
{
    public class AttendantDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public long ClientId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
