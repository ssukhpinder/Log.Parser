using Log.Parser.BL.Constants;
using Log.Parser.BL.Helper;
using Log.Parser.BL.Interface;
using Microsoft.AspNetCore.Http;

namespace Log.Parser.BL.Service
{
    public class FileManageService : IFileManageService
    {
        /// <summary>
        /// The method takes in input as the uploaded file and performs validations using ValidateFile method
        /// to check if the file has .txt extension or not
        /// If not throws a BadHttpRequestException exception which will be handled by Global Exception Handler
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        /// <exception cref="Microsoft.AspNetCore.Http.BadHttpRequestException"></exception>
        public async Task<string> Upload(IFormFile formFile)
        {

            FileInfo fileInfo = new FileInfo(formFile.FileName);
            string fileName = formFile.FileName + "_" + DateTime.Now.Ticks.ToString() + fileInfo.Extension;
            if (ValidateFile(fileName))
            {
                var filePath = DirectoryFileHelper.GetFilePath(fileName);
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }
                return filePath;
            }
            else
            {
                throw new Microsoft.AspNetCore.Http.BadHttpRequestException(message: CommonExceptionConstants.InvalidFileError);
            }
        }

        /// <summary>
        /// Method to check if the file has .txt extension or not
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool ValidateFile(string filePath)
        {
            string extension = Path.GetExtension(filePath);
            if (extension.Equals(".txt", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }
    }
}
