using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LoginApi.Controllers
{
    public class MianController : ApiController
    {
        [HttpGet]
        public IEnumerable<login> LogInDetails()
        {

            using (studentdbEntities entities = new studentdbEntities())
            {
                return entities.logins.ToList();
            }

        }

        [HttpGet]

        public IHttpActionResult LoginAuth(string user, string pass)
        {

            using (studentdbEntities entity = new studentdbEntities())
            {

                var std = entity.logins.FirstOrDefault(e => e.username == user && e.password == pass);

                if (std != null)
                {
                    return Ok(std);
                }
                else
                {
                    return NotFound();
                }
            }

        }
    }
}
