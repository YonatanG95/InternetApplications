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
    public class CheckupsApiController : ApiController
    {
        private WebContext db = new WebContext();
        // GET: api/CheckupsApi
        public HttpResponseMessage Get()
        {
            Dictionary<string, int> testscount = new Dictionary<string, int>();
            foreach (Checkup checkup in db.Checkups.ToList())
            {
                if (!testscount.ContainsKey(checkup.Type))
                {
                    testscount.Add(checkup.Type, 0);
                }
                if (checkup.Result)
                {
                    testscount[checkup.Type]++;
                }
            }
            var new_testscount = from key in testscount.Keys
                                 select new { Type = key, PositiveResults = testscount[key] };
            var json = JsonConvert.SerializeObject(new_testscount);
            return Request.CreateResponse(HttpStatusCode.OK, new_testscount, "application/json");
        }

        // GET: api/CheckupsApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/CheckupsApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/CheckupsApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CheckupsApi/5
        public void Delete(int id)
        {
        }
    }
}
