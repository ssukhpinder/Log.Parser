using Log.Parser.BL.Helper;
using Log.Parser.BL.Interface;
using Log.Parser.BL.Models.Response;
using System.Text.RegularExpressions;

namespace Log.Parser.BL.Service
{
    public class FileParserService : IParserService
    {
        /// <summary>
        /// Method to fetch number of accesses to webserver per host
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<List<KeyValueResponse>> GetAccessPerHost(string filePath, DateTime? startDate = null, DateTime? endDate = null)
        {
            var logs = await GetFileContent(filePath);
            var fileContents = logs.Where(logItem =>
                               (!startDate.HasValue || logItem.DateTime >= startDate.Value) &&
                               (!endDate.HasValue || logItem.DateTime <= endDate.Value));

            var result = fileContents.GroupBy(logItem => logItem.Host)
                                     .OrderByDescending(group => group.Count())
                                     .Select(group => new KeyValueResponse()
                                     {
                                         Key = group.Key,
                                         Value = group.Count()
                                     });

            return result.ToList();
        }

        /// <summary>
        /// Method to convert the log file records to the LogData Model using Regular Expression
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public async Task<List<LogData>> GetFileContent(string filePath)
        {
            var logEntries = new List<LogData>();

            var lines = await File.ReadAllLinesAsync(filePath);

            var logEntryPattern = new Regex(@"^(?<host>\S+) \[(?<datetime>\d+:\d+:\d+:\d+)\] ""(?<request>.+?)"" (?<returncode>\d{3}) (?<returnszie>\d+|-)$");

            foreach (var line in lines)
            {
                var match = logEntryPattern.Match(line);
                if (match.Success)
                {
                    try
                    {
                        var host = match.Groups["host"].Value;
                        var dateTime = DateTimeHelper.ParseLogDateTime(match.Groups["datetime"].Value);
                        var request = match.Groups["request"].Value;
                        var returnCodeString = match.Groups["returncode"].Value;
                        var returnSizeString = match.Groups["returnszie"].Value;


                        if (!int.TryParse(returnCodeString, out var returnCode))
                        {
                            Console.WriteLine($"Skipping log entry with invalid return code: {line}");
                            continue;
                        }

                        if (!int.TryParse(returnSizeString, out var returnSize))
                        {
                            returnSize = 0;
                        }

                        logEntries.Add(new LogData
                        {
                            Host = host,
                            DateTime = dateTime,
                            Request = request,
                            ReturnCode = returnCode,
                            ReturnSize = returnSize
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing log entry: {line}. Exception: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine($"Skipping malformed log entry: {line}");
                }
            }

            return logEntries;
        }

        /// <summary>
        /// Method to fetch number of successful resource accesses by URI. Only “GET” accesses to each URI are to be
        /// counted. Only requests which resulted in the HTTP reply code 200 (“OK”) are to be counted.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<List<KeyValueResponse>> GetSuccessPerUri(string filePath, DateTime? startDate = null, DateTime? endDate = null)
        {
            var fileContents = (await GetFileContent(filePath)).Where(logItem =>
                logItem.Request.StartsWith("GET") &&
                logItem.ReturnCode == 200 &&
                (!startDate.HasValue || logItem.DateTime >= startDate.Value) &&
                (!endDate.HasValue || logItem.DateTime <= endDate.Value));

            return fileContents.GroupBy(entry => entry.Request.Split(' ')[1])
                .OrderByDescending(group => group.Count())
                .Select(group => new KeyValueResponse()
                {
                    Key = group.Key,
                    Value = group.Count()
                }).ToList();
        }
    }
}
