using Microsoft.AspNetCore.Http;

namespace BuildingBlocks.Exceptions
{
    public class ExceptionDetails
    {
        public string Details { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public PathString Instance { get; set; }
        public Dictionary<string, object> Extensions = [];
    }
}
