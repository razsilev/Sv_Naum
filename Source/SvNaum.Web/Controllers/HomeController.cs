namespace SvNaum.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    
    using MongoDB.Bson;
    
    using SvNaum.Models;

    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            List<NewsSingle> news = new List<NewsSingle>();

            news = this.Repo.FindAll<NewsSingle>(this.Context.News).Reverse().ToList();

            return View(news);
        }

        [HttpGet]
        public ActionResult Timetable()
        {
            List<Ministration> result = new List<Ministration>();

            try
            {
                result = this.Repo.FindAll<Ministration>(this.Context.Ministration).OrderBy(m => m.Date).ToList();
            }
            catch (System.Exception)
            {

            }

            return View(result);
        }

        public ActionResult Pictures()
        {
            var imagesGroups = new List<ImagesGroup>();
            
            try
            {
                imagesGroups = this.Repo.FindAll<ImagesGroup>(this.Context.ImagesGroups).Reverse().ToList();
            }
            catch (System.Exception)
            {

            }

            return View(imagesGroups);
        }

        public ActionResult Sermons(int page = 0, string id = null)
        {
            if (page < 0)
            {
                page = 0;
            }

            this.ViewBag.PrevPage = page - 1;
            this.ViewBag.NextPage = page + 1;

            long sermonsCount = this.Context.Sermons.Count();
            this.ViewBag.SermonsCount = sermonsCount;

            if (!string.IsNullOrEmpty(id))
            {
                var ids = this.Repo.FindAll(this.Context.Sermons).SetFields(new string[] { "_id", "Date" }).OrderByDescending(s => s.Date).ToList();

                for (int i = 0; i < ids.Count; i++)
                {
                    if (ids[i].Id == id)
                    {
                        this.ViewBag.PrevPage = i - 1;
                        this.ViewBag.NextPage = i + 1;

                        break;
                    }
                }

                var sermonById = this.Repo.FindOneById<Sermon>(this.Context.Sermons, id);

                return View(sermonById);
            }

            Sermon sermon = new Sermon();

            try
            {
                sermon = this.Repo.FindAll<Sermon>(this.Context.Sermons).OrderByDescending(s => s.Date).Skip(page).FirstOrDefault();

                if (sermon == null)
                {
                    sermon = this.Repo.FindAll<Sermon>(this.Context.Sermons).OrderByDescending(s => s.Date).FirstOrDefault();
                    this.ViewBag.NextPage = 1;
                    this.ViewBag.PrevPage = -1;
                }
            }
            catch (System.Exception)
            {

            }

            return View(sermon);
        }

        public ActionResult Breviary(int page = 0, string id = null)
        {
            if (page < 0)
            {
                page = 0;
            }

            this.ViewBag.PrevPage = page - 1;
            this.ViewBag.NextPage = page + 1;

            long prayersCount = this.Context.Prayers.Count();
            this.ViewBag.PrayersCount = prayersCount;

            if (!string.IsNullOrEmpty(id))
            {
                var ids = this.Repo.FindAll(this.Context.Prayers).SetFields(new string[] { "_id" }).Reverse().ToList();

                for (int i = 0; i < ids.Count; i++)
                {
                    if (ids[i].Id == id)
                    {
                        this.ViewBag.PrevPage = i - 1;
                        this.ViewBag.NextPage = i + 1;

                        break;
                    }
                }

                var prayerById = this.Repo.FindOneById(this.Context.Prayers, id);

                return View(prayerById);
            }

            Prayer prayer = new Prayer();

            try
            {
                prayer = this.Repo.FindAll(this.Context.Prayers).Reverse().Skip(page).FirstOrDefault();

                if (prayer == null)
                {
                    prayer = this.Repo.FindAll(this.Context.Prayers).Reverse().FirstOrDefault();
                    this.ViewBag.NextPage = 1;
                    this.ViewBag.PrevPage = -1;
                }
            }
            catch (System.Exception)
            {

            }

            return View(prayer);
        }
    }
}