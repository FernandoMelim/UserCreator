using System.Net;

namespace UserCreator.Domain.DTOs.Responses
{
    public class ApiBaseResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public List<KeyValuePair<string, List<string>>>? Errors { get; set; } = new List<KeyValuePair<string, List<string>>>();
    }
}
