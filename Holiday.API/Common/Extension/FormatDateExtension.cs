namespace Holiday.API.Common.Extension;

public class FormatDateExtension
{
    public static string FormatDate(string inputDate)
    {
        if (inputDate.Length == 8)
        {
            string year = inputDate.Substring(0, 4);
            string month = inputDate.Substring(4, 2);
            string day = inputDate.Substring(6, 2);
            return $"{year}/{month}/{day}";
        }
        else
        {
            return "Invalid date format";
        }
    }
}

