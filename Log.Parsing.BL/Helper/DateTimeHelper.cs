namespace Log.Parser.BL.Helper
{
    public static class DateTimeHelper
    {
        /// <summary>
        /// A method is used to parse the date and time span
        /// </summary>
        /// <param name="logDateTimeString"></param>
        /// <returns></returns>
        public static DateTime? ParseLogDateTime(string logDateTimeString)
        {
            // Extract DD, HH, MM, SS
            var parts = logDateTimeString.Split(':');
            if (parts.Length != 4)
            {
                Console.WriteLine($"Invalid date format: {logDateTimeString}");
                return null;
            }

            var day = int.Parse(parts[0]);
            var hour = int.Parse(parts[1]);
            var minute = int.Parse(parts[2]);
            var second = int.Parse(parts[3]);

            var year = DateTime.Now.Year;
            var month = 8;
            var date = new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc);

            return date;
        }
    }
}
