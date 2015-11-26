namespace SvNaum.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using SvNaum.Models;

    using SvNaum.Web.Infrastructure;
    using SvNaum.Web.Models;

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

                this.Repo.Insert<Ministration>(this.Context.Ministration, ministration);

                return RedirectToAction("Timetable", "Home");
            }

            return View(inputMinistration);
        }

        [HttpGet]
        public ActionResult MinistrationEdit(string id)
        {
            var ministration = this.Repo.FindOneById(this.Context.Ministration, id);

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

                this.Repo.Update(this.Context.Ministration, ministration, inputMinistration.Id);

                return RedirectToAction("Timetable", "Home");
            }

            return View(inputMinistration);
        }

        public ActionResult DeleteMinistration(string id)
        {
            this.Repo.Delete(this.Context.Ministration, id);

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
                
                this.Repo.Insert(this.Context.Sermons, sermon);

                return RedirectToAction("Sermons", "Home");
            }

            return View(inputSermon);
        }

        [HttpGet]
        public ActionResult SermonEdit(string id)
        {
            var sermon = this.Repo.FindOneById(this.Context.Sermons, id);

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
                
                this.Repo.Update(this.Context.Sermons, sermon, inputSermon.Id);

                return RedirectToAction("Sermons", "Home");
            }

            return View(inputSermon);
        }

        public ActionResult SermonDelete(string id)
        {
            this.Repo.Delete(this.Context.Sermons, id);

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
                
                this.Repo.Insert(this.Context.Prayers, prayerDb);

                return RedirectToAction("Breviary", "Home");
            }

            return View(inputPrayer);
        }

        [HttpGet]
        public ActionResult PrayerEdit(string id)
        {
            var prauerDb = this.Repo.FindOneById(this.Context.Prayers, id);

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
                var prayerDb = new Prayer();

                prayerDb.Title = prayerInput.Title;
                prayerDb.TitleSecond = prayerInput.TitleSecond;
                prayerDb.Text = this.sanitizer.Sanitize(prayerInput.Text);
                prayerDb.Author = prayerInput.Author;
                
                this.Repo.Update(this.Context.Prayers, prayerDb, prayerInput.Id);

                return RedirectToAction("Breviary", "Home");
            }

            return View(prayerInput);
        }

        public ActionResult PrayerDelete(string id)
        {
            this.Repo.Delete(this.Context.Prayers, id);

            return RedirectToAction("Breviary", "Home");
        }

        // News CRUT
        [HttpGet]
        public ActionResult NewsAdd()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewsAdd(NewsInputModel inputNews)
        {
            if (this.ModelState.IsValid)
            {
                var newsSingleDb = new NewsSingle();

                newsSingleDb.Content = this.sanitizer.Sanitize(inputNews.Content);
                
                this.Repo.Insert(this.Context.News, newsSingleDb);

                return RedirectToAction("Index", "Home");
            }

            return View(inputNews);
        }

        [HttpGet]
        public ActionResult NewsEdit(string id)
        {
            var newsDb = this.Repo.FindOneById<NewsSingle>(this.Context.News, id);

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

                this.Repo.Update(this.Context.News, newsDb, newsInput.Id);

                return RedirectToAction("Index", "Home");
            }

            return View(newsInput);
        }

        public ActionResult NewsDelete(string id)
        {
            this.Repo.Delete(this.Context.News, id);

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
                
                this.Repo.Insert(this.Context.Pateriks, paterikDb);

                return RedirectToAction("Index", "Home");
            }

            return View(inputPaterik);
        }

        [HttpGet]
        public ActionResult PaterikEdit(string id)
        {
            var paterikDb = this.Repo.FindOneById(this.Context.Pateriks, id);

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
                
                this.Repo.Update(this.Context.Pateriks, paterikDb, paterikInput.Id);

                return RedirectToAction("Index", "Home");
            }

            return View(paterikInput);
        }

        public ActionResult PaterikDelete(string id)
        {
            this.Repo.Delete(this.Context.Pateriks, id);

            return RedirectToAction("Index", "Home");
        }

        // ImagesGroup CRUT
        [HttpGet]
        public ActionResult ImagesGroupAdd()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ImagesGroupAdd(ImagesGroupAddModel inputImagesGroup)
        {
            if (this.ModelState.IsValid)
            {
                var ImagesGroupDb = new ImagesGroup();

                ImagesGroupDb.Title = inputImagesGroup.Title;
                ImagesGroupDb.ImagesUrls = inputImagesGroup.ImagesUrls;
                
                this.Repo.Insert(this.Context.ImagesGroups, ImagesGroupDb);

                return RedirectToAction("Pictures", "Home");
            }

            return View(inputImagesGroup);
        }

        [HttpGet]
        public ActionResult ImagesGroupEdit(string id)
        {
            var imagesGroupDb = this.Repo.FindOneById(this.Context.ImagesGroups, id);

            if (imagesGroupDb != null)
            {
                var imagesGroupViewModel = new ImagesGroupAddModel()
                {
                    Id = imagesGroupDb.Id,
                    Title = imagesGroupDb.Title,
                    ImagesUrls = imagesGroupDb.ImagesUrls
                };

                return View(imagesGroupViewModel);
            }


            return RedirectToAction("Pictures", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ImagesGroupEdit(ImagesGroupAddModel imagesGroupInput)
        {
            if (this.ModelState.IsValid)
            {
                var imagesGroupDb = new ImagesGroup();

                imagesGroupDb.Title = imagesGroupInput.Title;
                imagesGroupDb.ImagesUrls = new List<TwoSizeImageUrls>();

                foreach (var item in imagesGroupInput.ImagesUrls)
                {
                    if (item != null && !string.IsNullOrWhiteSpace(item.BigImageUrl) && !string.IsNullOrWhiteSpace(item.SmallImageUrl))
                    {
                        imagesGroupDb.ImagesUrls.Add(item);
                    }
                }
                
                this.Repo.Update(this.Context.ImagesGroups, imagesGroupDb, imagesGroupInput.Id);

                return RedirectToAction("Pictures", "Home");
            }

            return View(imagesGroupInput);
        }

        public ActionResult ImagesGroupDelete(string id)
        {
            this.Repo.Delete(this.Context.ImagesGroups, id);

            return RedirectToAction("Pictures", "Home");
        }
    }
}