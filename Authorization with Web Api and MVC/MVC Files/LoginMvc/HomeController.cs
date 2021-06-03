using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoginMVC.Models;
using System.Net.Http;


namespace LoginMVC.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LoginModel model)
        {
           

            using(var client=new HttpClient())
            {
                string url = "http://localhost/api/Mian?user="+model.username+"&pass="+model.password;
                
                var response =client.GetAsync(url);

                response.Wait();


                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    return View();
                }
                else {
                    return View("LoginFail");
                }
            }
        }
	}
}