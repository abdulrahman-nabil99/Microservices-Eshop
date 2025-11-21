namespace BuildingBlocks.Exceptions
{
    public abstract class BadRequestException : Exception
    {
        public string Details { get; set; } = string.Empty;
        public BadRequestException(string message) : base(message) { }
        public BadRequestException(string message, string details) : base(message)
        {
            Details = details;
        }
    }
}
