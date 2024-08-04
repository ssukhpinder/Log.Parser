namespace Log.Parser.Tests.Helper
{
    public static class FileHandler
    {
        /// <summary>
        /// File handler used to create files during test cases with provided content
        /// </summary>
        /// <param name="content"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string CreateTextFile(string content, string fileName)
        {
            string directoryPath = Path.Combine(Environment.CurrentDirectory, "Files");
            string filePath = Path.Combine(directoryPath, fileName);


            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            File.WriteAllText(filePath, content);

            return filePath;
        }
    }
}
