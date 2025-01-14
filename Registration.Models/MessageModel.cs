namespace Registration.Models
{
    public record MessageModel
    {
        public DateTime DateTime { get; init; }

        public string? Email { get; init; }

        public string Code { get; init; }
    }
}
