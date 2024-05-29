using APIInsideExistingMVCProject.Models;
using APIInsideExistingMVCProject.Models.APIEFs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIInsideExistingMVCProject.Controllers
{
    [RoutePrefix("api/customers")]
    public class API_CustomerController : ApiController
    {
        APIEFs db;

        [Route("customers")]
        [HttpGet]
        [Authorize]
        public IHttpActionResult Customers()
        {
            db = new APIEFs();
            var customers = db.Customers.Take(50).ToList();
            return Json(customers);

        }
    }
}