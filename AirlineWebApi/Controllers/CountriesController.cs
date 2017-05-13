using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Airlines.Models;

namespace AirlineWebApi.Controllers
{
    public class CountriesController : ApiController
    {
        private AirlineDbContext db = new AirlineDbContext();

        public IEnumerable<Country> GetCountries()
        {
            return db.Countries;
        }

        public Country GetCountry(int id)
        {
            Country country = this.db.Countries.Find(id);

            return country;
        }

        public void Post([FromBody]Country country)
        {
            this.db.Countries.Add(country);
            this.db.SaveChanges();
        }

        public void Put([FromBody]Country country)
        {
            this.db.Entry(country).State = EntityState.Modified;
            this.db.SaveChanges();
        }

        public void Delete(int id)
        {
            Country country = this.db.Countries.Find(id);
            this.db.Countries.Remove(country);
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