using FluentAssertions;
using Log.Parser.BL.Interface;
using Log.Parser.BL.Service;
using Log.Parser.Tests.Helper;
using Log.Parser.Tests.TestData;

namespace Log.Parser.Tests.TestCases.v1
{
    [Collection("FileParser")]
    public class FileParserControllerTests
    {
        private IParserService _parserService;

        /// <summary>
        /// Class constructor
        /// </summary>
        public FileParserControllerTests()
        {
            _parserService = new FileParserService();
        }

        /// <summary>
        /// A test case to fetch number of accesses to webserver per host with a SINGLE entry in the log file        
        /// </summary>
        /// <param name="content"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        [Theory(DisplayName = "TC1 - Access Per Host - Single")]
        [MemberData(nameof(FileDataProvider.SingleLineFileContent), MemberType = typeof(FileDataProvider))]
        public async Task AccessSingleContentTest(string content, string filename)
        {
            var filePath = FileHandler.CreateTextFile(content, filename);

            var result = await _parserService.GetAccessPerHost(filePath);

            result.Should().NotBeNull();
            result.Any().Should().BeTrue();
            result.Count().Should().Be(1);
            result.First().Should().NotBeNull();
        }

        /// <summary>
        /// A test case to fetch number of accesses to webserver per host with a MULTIPLE entry in the log file
        /// </summary>
        /// <param name="content"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        [Theory(DisplayName = "TC2 - Access Per Host - Multiple")]
        [MemberData(nameof(FileDataProvider.MultipleLineFileContent), MemberType = typeof(FileDataProvider))]
        public async Task AccessMultipleContentTest(string content, string filename)
        {
            var filePath = FileHandler.CreateTextFile(content, filename);

            var result = await _parserService.GetAccessPerHost(filePath);

            result.Should().NotBeNull();
            result.Any().Should().BeTrue();
            result.Count().Should().Be(2);
            result.First().Should().NotBeNull();
        }

        /// <summary>
        /// A test case to fetch to fetch number of successful resource accesses by URI with a SINGLE entry in the log file
        /// </summary>
        /// <param name="content"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        [Theory(DisplayName = "TC3 - Success Per Uri - Single")]
        [MemberData(nameof(FileDataProvider.SingleLineFileContent), MemberType = typeof(FileDataProvider))]
        public async Task UriSingleContentTest(string content, string filename)
        {
            var filePath = FileHandler.CreateTextFile(content, filename);

            var result = await _parserService.GetSuccessPerUri(filePath);

            result.Should().NotBeNull();
            result.Any().Should().BeTrue();
            result.Count().Should().Be(1);
            result.First().Should().NotBeNull();
        }

        /// <summary>
        /// A test case to fetch to fetch number of successful resource accesses by URI with a MULTIPLE entry in the log file
        /// </summary>
        /// <param name="content"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        [Theory(DisplayName = "TC4 - Success Per Uri  - Multiple")]
        [MemberData(nameof(FileDataProvider.MultipleLineFileContent), MemberType = typeof(FileDataProvider))]
        public async Task UriMultipleContentTest(string content, string filename)
        {
            var filePath = FileHandler.CreateTextFile(content, filename);

            var result = await _parserService.GetSuccessPerUri(filePath);

            result.Should().NotBeNull();
            result.Any().Should().BeTrue();
            result.Count().Should().Be(2);
            result.First().Should().NotBeNull();
        }

        /// <summary>
        /// A test case to fetch number of accesses to webserver per host with empty/invalid data in the log file
        /// </summary>
        /// <param name="content"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        [Theory(DisplayName = "TC5 - Access Per Host  - Invalid")]
        [MemberData(nameof(FileDataProvider.InvalidFileContent), MemberType = typeof(FileDataProvider))]
        public async Task AccessInvalidTest(string content, string filename)
        {
            var filePath = FileHandler.CreateTextFile(content, filename);

            var result = await _parserService.GetAccessPerHost(filePath);

            result.Should().BeNullOrEmpty();
            result.Any().Should().BeFalse();
        }

        /// <summary>
        /// A test case to fetch to fetch number of successful resource accesses by URI with empty/invalid data in the log file
        /// </summary>
        /// <param name="content"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        [Theory(DisplayName = "TC6 - Success Per Uri  - Invalid")]
        [MemberData(nameof(FileDataProvider.InvalidFileContent), MemberType = typeof(FileDataProvider))]
        public async Task UriInvalidTest(string content, string filename)
        {
            var filePath = FileHandler.CreateTextFile(content, filename);

            var result = await _parserService.GetSuccessPerUri(filePath);

            result.Should().BeNullOrEmpty();
            result.Any().Should().BeFalse();
        }
    }
}
