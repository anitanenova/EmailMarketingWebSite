namespace BusinessManagement.WebSite.Helpers
{
    using System;
    using System.Globalization;

    public class DateHelper
    {
        public static DateTime GetShortFormatedDate(DateTime date)
        {
            var cultureName = CultureInfo.CurrentCulture.Name;
            CultureInfo cultureInfo = new CultureInfo(cultureName);

            return date;
        }

        public static string GetCurrentCultureName()
        {
            var cultureName = CultureInfo.CurrentCulture.Name;
            return cultureName;
        }
    }
}
