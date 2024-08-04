using Microsoft.AspNetCore.Http;

namespace Log.Parser.BL.Interface
{
    public interface IFileManageService
    {
        Task<string> Upload(IFormFile formFile);
        bool ValidateFile(string filePath);
    }
}
