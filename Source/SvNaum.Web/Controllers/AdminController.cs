namespace SvNaum.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using MongoDB.Bson;
    using MongoDB.Driver.Builders;
    
    using SvNaum.Data;
    using SvNaum.Models;
    using SvNaum.Web.Models;

    public class AdminController : Controller
    {
        private SvNaumDbContext context;

        public AdminController()
        {
            var conString = System.Configuration.ConfigurationManager.AppSettings["MONGOLAB_URI"].ToString();

            this.context = new SvNaumDbContext(conString);
        }

        public AdminController(SvNaumDbContext contex)
        {
            this.context = contex;
        }

        public ActionResult DeleteMinistration(string id)
        {

            MongoDB.Driver.IMongoQuery query = Query.EQ("_id", ObjectId.Parse(id));

            this.context.Ministration.Remove(query);


            return RedirectToAction("Timetable", "Home");
        }

        [HttpGet]
        public ActionResult TimetableAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TimetableAdd(MinistrationInputModel inputMinistration)
        {
            if (this.ModelState.IsValid)
            {
                var ministration = new Ministration();
                var dayName = inputMinistration.Date.ToString("dddd", new System.Globalization.CultureInfo("bg-BG", false));

                dayName = dayName.Replace(dayName[0], char.ToUpper(dayName[0]));

                ministration.Date = inputMinistration.Date;
                ministration.DayName = dayName;
                ministration.Description = inputMinistration.Description;

                this.context.Ministration.Insert(ministration);

                return RedirectToAction("Timetable", "Home");
            }

            return View(inputMinistration);
        }
    }
}