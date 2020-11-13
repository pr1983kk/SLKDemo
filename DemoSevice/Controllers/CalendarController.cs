using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Models;
using CommonAPI;

namespace DemoSevice.Controllers
{
    public class CalendarController : ApiController
    {
        
        [Route("api/Calendar/{noofdays?}")]
        [HttpGet]
        // GET: api/Calendar/5
        public HttpResponseMessage Get(int noofdays = -1)
      {
            var weekdays = new CalendarAPI().DaysOfWeek(noofdays);
            if (weekdays.Count < 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(weekdays);
        }

        [Route("api/Calendar/WeekDay")]
        [HttpPost]
        public HttpResponseMessage Post(List<DateTime> dateList)
        {    
            var weekdays = new CalendarAPI().PostDaysOfWeek(dateList);
            var response = Request.CreateResponse(HttpStatusCode.Created, weekdays);
            return response;
        }
    }

}
