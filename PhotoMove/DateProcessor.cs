using System.Globalization;

namespace PhotoMove
{
    internal static class DateProcessor
    {
        internal static DateTime GetDate(string dateText)
        {
            if (TryGetExifDate(dateText, out var date))
            {
                return date;
            }
            else if (TryGetAppleDate(dateText, out date))
            {
                return date;
            }

            return DateTime.MinValue;
        }

        private static bool TryGetExifDate(string dateText, out DateTime date)
        {
            string[] dateTimeParts = dateText.Split(" ");
            string[] dateParts = dateTimeParts[0].Split(":");
            date = DateTime.MinValue;
            try
            {
                date = DateTime.Parse($"{dateParts[0]}/{dateParts[1]}/{dateParts[2]}");
            }
            catch
            {
                return false;
            }
            return true;
        }

        private static bool TryGetAppleDate(string dateText, out DateTime date)
        {
            date = DateTime.MinValue;
            try
            {
                date = DateTime.ParseExact(dateText, "ddd MMM dd HH:mm:ss K yyyy", CultureInfo.InvariantCulture);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
