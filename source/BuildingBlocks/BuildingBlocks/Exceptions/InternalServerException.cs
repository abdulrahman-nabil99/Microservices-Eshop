namespace BuildingBlocks.Exceptions
{
    public abstract class InternalServerException : Exception
    {
        public string Details { get; set; } = string.Empty;
        public InternalServerException(string message) : base(message) { }
        public InternalServerException(string message, string details) : base(message) 
        {
            Details = details;
        }
    }
}
