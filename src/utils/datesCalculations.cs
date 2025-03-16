namespace TecnogadgedWin7
{
    public class DatesCalculations
    {
        static public string DateNowCalculate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        static public string DateNearSunday()
        {
            DateTime today = DateTime.Now;
            int daysUntilSunday = ((int)DayOfWeek.Sunday - (int)today.DayOfWeek + 7) % 7;
            DateTime nextSunday = today.AddDays(daysUntilSunday);
            return nextSunday.ToString("yyyy-MM-dd");
        }

        static public string DateNearMonday()
        {
            DateTime today = DateTime.Now;
            int daysSinceMonday = (int)today.DayOfWeek - (int)DayOfWeek.Monday;
            if (daysSinceMonday < 0)
            {
                daysSinceMonday += 7;
            }
            DateTime thisMonday = today.AddDays(-daysSinceMonday);
            return thisMonday.ToString("yyyy-MM-dd");
        }
        static public string GetNowDayName()
        {
            DateTime today = DateTime.Now;
            string dayName = today.ToString("dddd", new System.Globalization.CultureInfo("es-ES"));
            return dayName;
        }
    }
}