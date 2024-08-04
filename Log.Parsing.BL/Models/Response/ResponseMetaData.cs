using System.Net;

namespace Log.Parser.BL.Models.Response
{
    /// <summary>
    /// A generic response model will be returned for each API to make it consistent response across app
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseMetaData<T>
    {
        public HttpStatusCode Status { get; set; }
        public string? Message { get; set; }
        public bool IsError { get; set; }
        public string? ErrorDetails { get; set; }
        public string? CorrealtionId { get; set; }
        public T? Result { get; set; }
    }
}
