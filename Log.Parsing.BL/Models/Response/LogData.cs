namespace Log.Parser.BL.Models.Response
{
    /// <summary>
    /// The model class corresponds to the log file data.
    /// </summary>
    public class LogData
    {
        public string? Host { get; set; }
        public DateTime? DateTime { get; set; }
        public string? Request { get; set; }
        public int? ReturnCode { get; set; }
        public int? ReturnSize { get; set; }
    }
}
