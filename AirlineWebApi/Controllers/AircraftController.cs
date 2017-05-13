using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Airlines.Models;

namespace AirlineWebApi.Controllers
{
    public class AircraftController : ApiController
    {
        private AirlineDbContext db = new AirlineDbContext();

        public IEnumerable<Aircraft> Get()
        {
            IEnumerable<Aircraft> aircraft = db.Aircrafts.Include(a => a.Company);

            return aircraft.ToList();
        }

        public Aircraft Get(int id)
        {
            Aircraft aircraft = this.db.Aircrafts.Find(id);

            return aircraft;
        }

        public void Post([FromBody]Aircraft aircraft)
        {
            this.db.Aircrafts.Add(aircraft);
            this.db.SaveChanges();
        }

        public void Put([FromBody]Aircraft aircraft)
        {
            this.db.Entry(aircraft).State = EntityState.Modified;
            this.db.SaveChanges();
        }

        public void Delete(int id)
        {
            Aircraft aircraft = this.db.Aircrafts.Find(id);
            this.db.Aircrafts.Remove(aircraft);
            this.db.SaveChanges();
        }

        public HttpResponseMessage Options()
        {
            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            return resp;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
