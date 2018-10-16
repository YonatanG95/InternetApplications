using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Web1.Models;
using Newtonsoft.Json;


namespace Web1.Controllers
{
    public class AppointmentsApiController : ApiController
    {
        private WebContext db = new WebContext();
        // GET: api/AppointmentsApi
        public HttpResponseMessage Get()
        {
            Dictionary<int, int> appmonth = new Dictionary<int, int>();
            foreach (Appointment appointment in db.Appointments.ToList())
            {
                if (!appmonth.ContainsKey(appointment.Date_Time.Month))
                {
                    appmonth.Add(appointment.Date_Time.Month, 0);
                }
                appmonth[appointment.Date_Time.Month]++;
            }
            var new_appmonth = from key in appmonth.Keys
                               select new { Month = key, AppointmentsCount = appmonth[key] };
            var json = JsonConvert.SerializeObject(new_appmonth);
            return Request.CreateResponse(HttpStatusCode.OK, new_appmonth, "application/json");
        }

        // GET: api/AppointmentsApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/AppointmentsApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/AppointmentsApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/AppointmentsApi/5
        public void Delete(int id)
        {
        }
    }
}
