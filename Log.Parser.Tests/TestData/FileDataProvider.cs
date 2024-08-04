namespace Log.Parser.Tests.TestData
{
    public static class FileDataProvider
    {
        private static string _singleLineFileContent = "141.243.1.172 [29:23:53:25] \"GET /Software.html HTTP/1.0\" 200 1497\r\n";
        private static string _multipleLineFileContent = "141.243.1.172 [29:23:53:25] \"GET /Software.html HTTP/1.0\" 200 1497\r\nquery2.lycos.cs.cmu.edu [29:23:53:36] \"GET /Consumer.html HTTP/1.0\" 200 1325\r\n";
        private static string _testFilename = "test.txt";

        private static string _invalidContent = "invalid";
        public static IEnumerable<object[]> SingleLineFileContent()
        {
            yield return new object[] { _singleLineFileContent, _testFilename };
        }

        public static IEnumerable<object[]> MultipleLineFileContent()
        {
            yield return new object[] { _multipleLineFileContent, _testFilename };
        }
        public static IEnumerable<object[]> InvalidFileContent()
        {
            yield return new object[] { _invalidContent, _testFilename };
        }
    }
}
