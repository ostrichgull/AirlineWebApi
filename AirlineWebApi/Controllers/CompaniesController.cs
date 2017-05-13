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
    public class CompaniesController : ApiController
    {
        private AirlineDbContext db = new AirlineDbContext();
        
        public IEnumerable<Company> Get()
        {
            var companies = db.Companies
                                .Include(c => c.Country)
                                .Include(c => c.State)
                                .Include(c => c.CompanyType);

            return companies;
        }

        [Route("api/companies/details/{id}")]
        public Company Get(int id)
        {
            Company company = this.db.Companies.Find(id);

            return company;
        }

        [Route("api/companies/{typeId}")]
        public IEnumerable<Company> Get(CompanyTypeValue typeId)
        {
            var companies = db.Companies
                                .Include(c => c.Country)
                                .Include(c => c.State)
                                .Include(c => c.CompanyType)
                                .Where(c => c.CompanyTypeID == (int)typeId);

            return companies;
        }

        [HttpPost]
        [Route("api/companies/")]
        public void Post([FromBody]Company company)
        {
            this.db.Companies.Add(company);
            this.db.SaveChanges();
        }

        [Route("api/companies/update/")]
        public void Put([FromBody]Company company)
        {
            //this.db.Entry(company).State = EntityState.Modified;
            IQueryable<Company> updated = db.Companies.Where(c => c.ID == company.ID);

            updated.FirstOrDefault().Name = company.Name;
            updated.FirstOrDefault().Description = company.Description;
            updated.FirstOrDefault().CountryID = company.CountryID;
            updated.FirstOrDefault().StateID = company.StateID;
            updated.FirstOrDefault().CompanyTypeID = company.CompanyTypeID;
            updated.FirstOrDefault().Logo = company.Logo;

            this.db.SaveChanges();
        }

        [HttpDelete]
        [Route("api/companies/delete/{id}")]
        public void Delete(int id)
        {
            Company company = this.db.Companies.Find(id);
            this.db.Companies.Remove(company);
            this.db.SaveChanges();
        }

        [HttpOptions]
        [Route("api/companies/")]
        [Route("api/companies/update/")]
        [Route("api/companies/delete/{id}")]
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