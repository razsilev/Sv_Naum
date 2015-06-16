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
    using SvNaum.Web.Infrastructure;

    [Authorize]
    public class AdminController : BaseController
    {
        private readonly ISanitizer sanitizer;

        public AdminController()
        {
            this.sanitizer = new HtmlSanitizerAdapter();
        }

        public AdminController(ISanitizer sanitizer)
        {
            this.sanitizer = sanitizer;
        }

        [HttpGet]
        public ActionResult TimetableAdd()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

                ministration.Title = inputMinistration.Title;
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
                ministration.Date = ministration.Date.ToLocalTime();

                var minViewModel = new MinistrationUpdateInputModel()
                {
                    Id = ministration.Id,
                    Title = ministration.Title,
                    Date = ministration.Date,
                    Description = ministration.Description,
                    Time = ministration.Date.Hour.ToString() + ":" + ministration.Date.Minute.ToString()
                };

                return View(minViewModel);
            }


            return RedirectToAction("Timetable", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

                ministration.Title = inputMinistration.Title;
                ministration.Date = inputMinistration.Date;
                ministration.DayName = dayName;
                ministration.Description = inputMinistration.Description;

                this.Context.Ministration.Insert(ministration);

                this.DeleteMinistrationBy(inputMinistration.Id);

                return RedirectToAction("Timetable", "Home");
            }

            return View(inputMinistration);
        }

        public ActionResult DeleteMinistration(string id)
        {
            this.DeleteMinistrationBy(id);

            return RedirectToAction("Timetable", "Home");
        }

        // crut sermons
        [HttpGet]
        public ActionResult SermonAdd()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SermonAdd(SermonInputModel inputSermon)
        {
            if (this.ModelState.IsValid)
            {
                var sermon = new Sermon();
                
                sermon.Date = DateTime.Now;
                sermon.Author = inputSermon.Author;
                sermon.Text = this.sanitizer.Sanitize(inputSermon.Text);
                sermon.Theme = inputSermon.Theme;
                sermon.Title = inputSermon.Title;
                sermon.ImageUrl = string.Empty;

                this.Context.Sermons.Insert(sermon);

                return RedirectToAction("Sermons", "Home");
            }

            return View(inputSermon);
        }

        [HttpGet]
        public ActionResult SermonEdit(string id)
        {
            MongoDB.Driver.IMongoQuery query = Query.EQ("_id", ObjectId.Parse(id));

            var sermon = this.Context.Sermons.Find(query).FirstOrDefault();

            if (sermon != null)
            {
                var sermonViewModel = new SermonUpdateInputModel()
                {
                    Id = sermon.Id,
                    Date = sermon.Date,
                    Text = sermon.Text,
                    Theme = sermon.Theme,
                    Author = sermon.Author,
                    Title = sermon.Title
                };

                return View(sermonViewModel);
            }


            return RedirectToAction("Sermons", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SermonEdit(SermonUpdateInputModel inputSermon)
        {
            if (this.ModelState.IsValid)
            {
                var sermon = new Sermon();

                sermon.Author = inputSermon.Author;
                sermon.ImageUrl = string.Empty;
                sermon.Text = this.sanitizer.Sanitize(inputSermon.Text);

                sermon.Theme = inputSermon.Theme;
                sermon.Title = inputSermon.Title;
                sermon.Date = inputSermon.Date;

                this.Context.Sermons.Insert(sermon);

                this.DeleteSermonBy(inputSermon.Id);

                return RedirectToAction("Sermons", "Home");
            }

            return View(inputSermon);
        }

        public ActionResult SermonDelete(string id)
        {
            this.DeleteSermonBy(id);

            return RedirectToAction("Sermons", "Home");
        }

        private void DeleteMinistrationBy(string id)
        {
            MongoDB.Driver.IMongoQuery query = Query.EQ("_id", ObjectId.Parse(id));

            this.Context.Ministration.Remove(query);
        }

        private void DeleteSermonBy(string id)
        {
            MongoDB.Driver.IMongoQuery query = Query.EQ("_id", ObjectId.Parse(id));

            this.Context.Sermons.Remove(query);
        }
    }
}