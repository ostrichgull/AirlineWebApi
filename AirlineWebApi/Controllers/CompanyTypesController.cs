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
    public class CompanyTypesController : ApiController
    {
        private AirlineDbContext db = new AirlineDbContext();

        public IEnumerable<CompanyType> GetCompanyTypes()
        {
            return db.CompanyTypes;
        }

        public CompanyType GetCompanyType(int id)
        {
            CompanyType companyType = this.db.CompanyTypes.Find(id);

            return companyType;
        }

        public void Post([FromBody]CompanyType companyType)
        {
            this.db.CompanyTypes.Add(companyType);
            this.db.SaveChanges();
        }

        public void Put([FromBody]CompanyType companyType)
        {
            this.db.Entry(companyType).State = EntityState.Modified;
            this.db.SaveChanges();
        }

        public void Delete(int id)
        {
            CompanyType companyType = this.db.CompanyTypes.Find(id);
            this.db.CompanyTypes.Remove(companyType);
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
