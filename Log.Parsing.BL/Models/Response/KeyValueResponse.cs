namespace Log.Parser.BL.Models.Response
{
    /// <summary>
    /// The model class corresponds to the data returned in the API response for Access Host and Success URI
    /// </summary>
    public class KeyValueResponse
    {
        public string? Key { get; set; }
        public int? Value { get; set; }
    }
}
