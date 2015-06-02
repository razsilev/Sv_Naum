namespace SvNaum.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using SvNaum.Data;
    using SvNaum.Models;
    using SvNaum.Web.Models;
    using MongoDB.Driver.Builders;
    using MongoDB.Bson;

    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
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

        [HttpGet]
        public ActionResult Timetable()
        {
            List<Ministration> result = new List<Ministration>();

            try
            {
                result = this.Context.Ministration.FindAll().OrderBy(m => m.Date).ToList();
            }
            catch (Exception ex)
            {
                this.ViewBag.Ex = ex.ToString();
            }
            
            return View(result);
        }

        public ActionResult Pictures()
        {
            return View();
        }

        public ActionResult Sermons()
        {
            return View();
        }
    }
}