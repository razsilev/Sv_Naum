﻿namespace SvNaum.Web.Controllers
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

        public ActionResult Sermons(int page = 0)
        {
            if (page < 0)
            {
                page = 0;
            }

            this.ViewBag.PrevPage = page - 1;
            this.ViewBag.NextPage = page + 1;

            Sermon sermon = new Sermon();

            try
            {
                sermon = this.Context.Sermons.FindAll().OrderByDescending(s => s.Date).Skip(page).FirstOrDefault();

                if (sermon == null)
                {
                    sermon = this.Context.Sermons.FindAll().OrderByDescending(s => s.Date).FirstOrDefault();
                    this.ViewBag.NextPage = 1;
                    this.ViewBag.PrevPage = -1;
                }
            }
            catch (System.Exception)
            {

            }

            return View(sermon);
        }

        public ActionResult Breviary(int page = 0)
        {
            if (page < 0)
            {
                page = 0;
            }

            this.ViewBag.PrevPage = page - 1;
            this.ViewBag.NextPage = page + 1;
            Prayer prayer = new Prayer();

            try
            {
                prayer = this.Context.Prayers.FindAll().Reverse().Skip(page).FirstOrDefault();

                if (prayer == null)
                {
                    prayer = this.Context.Prayers.FindAll().Reverse().FirstOrDefault();
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