using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DemoUI
{
    public class ServiceCall
    {
        private string serviceBaseAddress = string.Empty;
        public ServiceCall()
        {
            serviceBaseAddress = ConfigurationManager.AppSettings["ServiceBaseAddress"];
        }
        private void SetBaseAddress(HttpClient client)
        {
            client.BaseAddress = new Uri(serviceBaseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public List<WeekDays> SetWeekDays()
        {
            var data = new List<WeekDays>();
            try
            {
                using (var client = new HttpClient())
                {
                    SetBaseAddress(client);
                    var response = client.GetStringAsync("Calendar");
                    response.Wait();
                    data = JsonConvert.DeserializeObject<List<WeekDays>>(response.Result);
                }
            }
            catch (Exception ex)
            {
                //log exception
            }
            return data;
        }


        public List<WeekDays> ResetWeekDays(int index)
        {
            var data = new List<WeekDays>();

            try
            {
                using (var client = new HttpClient())
                {
                    SetBaseAddress(client);
                    var url = "Calendar?noofdays=" + index;
                    var response = client.GetStringAsync(url);
                    response.Wait();
                    data = JsonConvert.DeserializeObject<List<WeekDays>>(response.Result);
                }
            }
            catch(Exception ex)
            {
                //log exception
            }
            return data;
        }

        public List<WeekDays> PostWeekDays(List<DateTime> dateTimeList)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                SetBaseAddress(client);
                var weekDays = new List<WeekDays>();
                var postTask = client.PostAsJsonAsync("Calendar/WeekDay", dateTimeList);
                postTask.Wait();
                var result = postTask.Result.Content.ReadAsAsync<List<WeekDays>>();
                var data = result.Result;
                return data;
            }
        }

    }
}