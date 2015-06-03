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

    public class AdminController : BaseController
    {
        public ActionResult DeleteMinistration(string id)
        {
            this.DeleteMinistrationBy(id);
            
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

                try
                {
                    var tokens = inputMinistration.Time.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    int hour = int.Parse(tokens[0]);
                    int minutes = int.Parse(tokens[1]);

                    inputMinistration.Date = new DateTime(inputMinistration.Date.Year, inputMinistration.Date.Month,
                                                    inputMinistration.Date.Day, hour, minutes, 0);
                }
                catch (Exception)
                {
                    this.ModelState.AddModelError("Time", "The time field mast be valid time in format HH:mm");

                    return View(inputMinistration);
                }

                ministration.Date = inputMinistration.Date;
                ministration.DayName = dayName;
                ministration.Description = inputMinistration.Description;

                this.Context.Ministration.Insert(ministration);

                return RedirectToAction("Timetable", "Home");
            }

            return View(inputMinistration);
        }

        [HttpGet]
        public ActionResult MinistrationEdit(string id)
        {
            MongoDB.Driver.IMongoQuery query = Query.EQ("_id", ObjectId.Parse(id));

            var ministration = this.Context.Ministration.Find(query).FirstOrDefault();

            if(ministration != null)
            {
                var minViewModel = new MinistrationUpdateInputModel()
                {
                    Id = ministration.Id,
                    Date = ministration.Date,
                    Description = ministration.Description,
                    Time = ministration.Date.Hour.ToString() + ":" + ministration.Date.Minute.ToString()
                };

                return View(minViewModel);
            }


            return RedirectToAction("Timetable", "Home");
        }

        [HttpPost]
        public ActionResult MinistrationEdit(MinistrationUpdateInputModel inputMinistration)
        {
            if (this.ModelState.IsValid)
            {
                var ministration = new Ministration();
                var dayName = inputMinistration.Date.ToString("dddd", new System.Globalization.CultureInfo("bg-BG", false));

                dayName = dayName.Replace(dayName[0], char.ToUpper(dayName[0]));

                try
                {
                    var tokens = inputMinistration.Time.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    int hour = int.Parse(tokens[0]);
                    int minutes = int.Parse(tokens[1]);

                    inputMinistration.Date = new DateTime(inputMinistration.Date.Year, inputMinistration.Date.Month,
                                                    inputMinistration.Date.Day, hour, minutes, 0);
                }
                catch (Exception)
                {
                    this.ModelState.AddModelError("Time", "The time field mast be valid time in format HH:mm");

                    return View(inputMinistration);
                }

                ministration.Date = inputMinistration.Date;
                ministration.DayName = dayName;
                ministration.Description = inputMinistration.Description;

                this.Context.Ministration.Insert(ministration);

                this.DeleteMinistrationBy(inputMinistration.Id);

                return RedirectToAction("Timetable", "Home");
            }

            return View(inputMinistration);
        }

        private void DeleteMinistrationBy(string id)
        {
            MongoDB.Driver.IMongoQuery query = Query.EQ("_id", ObjectId.Parse(id));

            this.Context.Ministration.Remove(query);
        }
    }
}