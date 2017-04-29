using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Drawor.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            Mappers.MapperConfig mapper = new Mappers.MapperConfig();

            mapper.TestConection();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}