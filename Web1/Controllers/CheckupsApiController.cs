using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Web1.Models;

namespace Web1.Controllers
{
    public class CheckupsApiController : ApiController
    {
        private WebContext db = new WebContext();
        // GET: api/CheckupsApi
        public IEnumerable<Checkup> Get()
        {
            return db.Checkups.ToList();
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
