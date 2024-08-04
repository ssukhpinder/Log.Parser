using Log.Parser.BL.Models.Response;

namespace Log.Parser.BL.Interface
{
    public interface IParserService
    {
        public Task<List<KeyValueResponse>> GetAccessPerHost(string filePath, DateTime? startDate = null, DateTime? endDate = null);
        public Task<List<KeyValueResponse>> GetSuccessPerUri(string filePath, DateTime? startDate = null, DateTime? endDate = null);
        public Task<List<LogData>> GetFileContent(string filePath);
    }
}
