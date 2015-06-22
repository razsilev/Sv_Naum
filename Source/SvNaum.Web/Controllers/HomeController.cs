namespace SvNaum.Web.Controllers
{
    using SvNaum.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            List<NewsSingle> news = new List<NewsSingle>();

            try
            {
                news = this.Context.News.FindAll().Reverse().ToList();
            }
            catch (System.Exception)
            {

            }

            return View(news);
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
            catch (System.Exception)
            {

            }


            return View(result);
        }

        public ActionResult Pictures()
        {
            return View();
        }

        public ActionResult Sermons()
        {
            List<Sermon> sermons = new List<Sermon>();

            try
            {
                sermons = this.Context.Sermons.FindAll().OrderByDescending(s => s.Date).ToList();
            }
            catch (System.Exception)
            {

            }

            return View(sermons);
        }

        public ActionResult Breviary()
        {
            List<Prayer> prayers = new List<Prayer>();

            try
            {
                prayers = this.Context.Prayers.FindAll().Reverse().ToList();
            }
            catch (System.Exception)
            {

            }

            return View(prayers);
        }
    }
}