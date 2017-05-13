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
    public class StatesController : ApiController
    {
        private AirlineDbContext db = new AirlineDbContext();

        public IEnumerable<State> GetStates()
        {
            return db.States.Include(s => s.Country);
        }

        public State GetState(int id)
        {
            State state = this.db.States.Find(id);

            return state;
        }

        public void Post([FromBody]State state)
        {
            this.db.States.Add(state);
            this.db.SaveChanges();
        }

        public void Put([FromBody]State state)
        {
            this.db.Entry(state).State = EntityState.Modified;
            this.db.SaveChanges();
        }

        public void Delete(int id)
        {
            State state = this.db.States.Find(id);
            this.db.States.Remove(state);
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
