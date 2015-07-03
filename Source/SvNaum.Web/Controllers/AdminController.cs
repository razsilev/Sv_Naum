namespace SvNaum.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using MongoDB.Bson;
    using MongoDB.Driver;
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

            if (ministration != null)
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

        // crut prayers
        [HttpGet]
        public ActionResult PrayerAdd()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PrayerAdd(PrayerInputModel inputPrayer)
        {
            if (this.ModelState.IsValid)
            {
                var prayerDb = new Prayer();

                prayerDb.Author = inputPrayer.Author;
                prayerDb.Text = this.sanitizer.Sanitize(inputPrayer.Text);
                prayerDb.Title = inputPrayer.Title;
                prayerDb.TitleSecond = inputPrayer.TitleSecond ?? string.Empty;

                this.Context.Prayers.Insert(prayerDb);

                return RedirectToAction("Breviary", "Home");
            }

            return View(inputPrayer);
        }

        [HttpGet]
        public ActionResult PrayerEdit(string id)
        {
            IMongoQuery query = this.CreateQueryById(id);
            var prauerDb = this.Context.Prayers.Find(query).FirstOrDefault();

            if (prauerDb != null)
            {
                var prayerViewModel = new PrayerUpdateInputModel()
                {
                    Id = prauerDb.Id,
                    Text = prauerDb.Text,
                    Author = prauerDb.Author,
                    Title = prauerDb.Title,
                    TitleSecond = prauerDb.TitleSecond
                };

                return View(prayerViewModel);
            }


            return RedirectToAction("Breviary", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PrayerEdit(PrayerUpdateInputModel prayerInput)
        {
            if (this.ModelState.IsValid)
            {
                var PrayerDb = new Prayer();

                PrayerDb.Title = prayerInput.Title;
                PrayerDb.TitleSecond = prayerInput.TitleSecond;
                PrayerDb.Text = this.sanitizer.Sanitize(prayerInput.Text);
                PrayerDb.Author = prayerInput.Author;

                this.Context.Prayers.Insert(PrayerDb);

                this.DeletePrayerBy(prayerInput.Id);

                return RedirectToAction("Breviary", "Home");
            }

            return View(prayerInput);
        }

        public ActionResult PrayerDelete(string id)
        {
            this.DeletePrayerBy(id);

            return RedirectToAction("Breviary", "Home");
        }

        // News CRUT
        [HttpGet]
        public ActionResult NewsAdd()
        {
            return View("NewsAdd");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewsAdd(NewsInputModel inputNews)
        {
            if (this.ModelState.IsValid)
            {
                var newsSingleDb = new NewsSingle();

                newsSingleDb.Content = this.sanitizer.Sanitize(inputNews.Content);

                this.Context.News.Insert(newsSingleDb);

                return RedirectToAction("Index", "Home");
            }

            return View(inputNews);
        }

        [HttpGet]
        public ActionResult NewsEdit(string id)
        {
            IMongoQuery query = this.CreateQueryById(id);
            var newsDb = this.Context.News.Find(query).FirstOrDefault();

            if (newsDb != null)
            {
                var newsViewModel = new NewsUpdateModel()
                {
                    Id = newsDb.Id,
                    Content = newsDb.Content
                };

                return View(newsViewModel);
            }


            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewsEdit(NewsUpdateModel newsInput)
        {
            if (this.ModelState.IsValid)
            {
                var newsDb = new NewsSingle();
                newsDb.Content = this.sanitizer.Sanitize(newsInput.Content);

                this.Context.News.Insert(newsDb);

                this.DeleteNewsBy(newsInput.Id);

                return RedirectToAction("Index", "Home");
            }

            return View(newsInput);
        }

        public ActionResult NewsDelete(string id)
        {
            this.DeleteNewsBy(id);

            return RedirectToAction("Index", "Home");
        }

        // Paterik CRUT
        [HttpGet]
        public ActionResult PaterikAdd()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PaterikAdd(PaterikModel inputPaterik)
        {
            if (this.ModelState.IsValid)
            {
                var paterikDb = new Paterik();

                paterikDb.Text = this.sanitizer.Sanitize(inputPaterik.Text);
                paterikDb.Author = inputPaterik.Author;

                this.Context.Pateriks.Insert(paterikDb);

                return RedirectToAction("Index", "Home");
            }

            return View(inputPaterik);
        }

        [HttpGet]
        public ActionResult PaterikEdit(string id)
        {
            IMongoQuery query = this.CreateQueryById(id);
            var paterikDb = this.Context.Pateriks.Find(query).FirstOrDefault();

            if (paterikDb != null)
            {
                var paterikViewModel = new PaterikModel()
                {
                    Id = paterikDb.Id,
                    Text = paterikDb.Text,
                    Author = paterikDb.Author
                };

                return View(paterikViewModel);
            }


            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PaterikEdit(PaterikModel paterikInput)
        {
            if (this.ModelState.IsValid)
            {
                var paterikDb = new Paterik();

                paterikDb.Text = this.sanitizer.Sanitize(paterikInput.Text);
                paterikDb.Author = paterikInput.Author;

                this.Context.Pateriks.Insert(paterikDb);

                this.DeletePaterikBy(paterikInput.Id);

                return RedirectToAction("Index", "Home");
            }

            return View(paterikInput);
        }

        public ActionResult PaterikDelete(string id)
        {
            this.DeletePaterikBy(id);

            return RedirectToAction("Index", "Home");
        }

        private void DeleteMinistrationBy(string id)
        {
            IMongoQuery query = this.CreateQueryById(id);

            this.Context.Ministration.Remove(query);
        }

        private void DeleteSermonBy(string id)
        {
            IMongoQuery query = this.CreateQueryById(id);

            this.Context.Sermons.Remove(query);
        }

        private void DeletePrayerBy(string id)
        {
            IMongoQuery query = this.CreateQueryById(id);

            this.Context.Prayers.Remove(query);
        }

        private void DeleteNewsBy(string id)
        {
            IMongoQuery query = this.CreateQueryById(id);

            this.Context.News.Remove(query);
        }

        private void DeletePaterikBy(string id)
        {
            IMongoQuery query = this.CreateQueryById(id);

            this.Context.Pateriks.Remove(query);
        }

        private IMongoQuery CreateQueryById(string id)
        {
            MongoDB.Driver.IMongoQuery query = Query.EQ("_id", ObjectId.Parse(id));

            return query;
        }
    }
}