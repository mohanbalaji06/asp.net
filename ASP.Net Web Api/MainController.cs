using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;





namespace WebApplicationLogin.Controllers
{
    //[RoutePrefix("api/MainController")]
    public class MainController: ApiController
    {

        [HttpGet]
        public IEnumerable<login> LogInDetails(){

            using (studentdbEntities1 entities = new studentdbEntities1())
            {
                return entities.logins.ToList();
            }
            
        }

        [HttpGet]

        public bool LoginAuth(string user, string pass) {

            using (studentdbEntities1 entity = new studentdbEntities1()) {

                var std = entity.logins.FirstOrDefault(e => e.username == user && e.password == pass);

                if (std != null) {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        [HttpPost]
        public bool Index()
        {

            //login validation goes here..
            return true;
        }

    }
}