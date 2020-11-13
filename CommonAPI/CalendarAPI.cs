using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using System.Threading.Tasks;

namespace CommonAPI
{
    public class CalendarAPI
    {
        public List<WeekDays> DaysOfWeek(int noofdays)
        {
            bool flag = false;
            if (noofdays < 0 || noofdays > 7)
                noofdays = 0;
            else
                flag = true;
            var daysOfWeek = new List<WeekDays>();
            DateTime startday = DateTime.Now.AddDays(noofdays);
            for (int i = 0; i < 7; i++)
            {
                daysOfWeek.Add(new WeekDays
                {
                    DayIndex = i,
                    Day = startday.AddDays(i).DayOfWeek.ToString(),
                    URLParamFlag = flag,
                    DateTimeOfDay = startday.AddDays(i)
                });
            }
            return daysOfWeek;
        }
        public List<WeekDays> PostDaysOfWeek(List<DateTime> dateTimeList)
        {
            var daysOfWeek = new List<WeekDays>();
            int i = 0;
            foreach (var d in dateTimeList)
            {
                daysOfWeek.Add(new WeekDays
                {
                    DayIndex = i++,
                    Day = d.DayOfWeek.ToString(),
                    URLParamFlag = true,
                    DateTimeOfDay = d
                });
            }
            return daysOfWeek;
        }

    }
}
