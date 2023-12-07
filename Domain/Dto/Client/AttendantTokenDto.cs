namespace Domain.Dto.Client
{
    public class AttendantTokenDto
    {
        public long Id { get; set; }
        public string Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public long ClientId { get; set; }
    }
}
