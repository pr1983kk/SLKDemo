using System;

namespace Models
{
    public class WeekDays
    {
        public int DayIndex { get; set; }
        public string Day { get; set; }
        public bool URLParamFlag { get; set; }
        public DateTime DateTimeOfDay { get; set; }

        private string dayIndexDay;

        public string DayIndexDay
        {
            get { return dayIndexDay = DayIndex + "." + Day; }

        }
    }
}
