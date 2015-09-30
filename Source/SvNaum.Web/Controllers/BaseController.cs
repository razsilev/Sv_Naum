namespace SvNaum.Web.Controllers
{
    using System.Configuration;
    using System.Web.Mvc;
    using System.Linq;

    using MongoDB.Driver;

    using SvNaum.Data;
    using System.Collections.Generic;
    using SvNaum.Models;

    public abstract class BaseController : Controller
    {
        private SvNaumDbContext context;
        private SvNaum.Data.Repository repo;

        public BaseController()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("bg", false);

            var conString = this.GetMongoDbConnectionString();
            this.context = new SvNaumDbContext(conString);

            this.repo = new SvNaum.Data.Repository();
        }

        public SvNaumDbContext Context
        {
            get
            {
                return context;
            }
        }

        public SvNaum.Data.Repository Repo
        {
            get
            {
                return this.repo;
            }
        }

        [ChildActionOnly]
        public void PopulateLayoutData()
        {
            this.PopulateTopTenSermons();
            this.PopulateTopTenBreviaries();
            this.PopulatePateriks();
        }

        private string GetMongoDbConnectionString()
        {
            return ConfigurationManager.AppSettings.Get("MONGOLAB_URI") ??
                //"mongodb://localhost:27017";
                "mongodb://appharbor_1aca7d3f-ed85-4975-8b3f-56a35407755d:t9q99k30fvct2u51kt09jv1pqa@ds039231.mongolab.com:39231/appharbor_1aca7d3f-ed85-4975-8b3f-56a35407755d";
        }

        private void PopulateTopTenSermons()
        {
            var sermons = new List<Sermon>();

            try
            {
                sermons = this.Context.Sermons.FindAll().OrderByDescending(s => s.Date).Take(10).ToList();

                if (sermons == null)
                {
                    sermons = new List<Sermon>();
                }
            }
            catch (System.Exception)
            {

            }

            this.TempData["TopTenSermons"] = sermons;
        }

        private void PopulateTopTenBreviaries()
        {

            var prayers = new List<Prayer>();

            try
            {
                prayers = this.Context.Prayers.FindAll().Reverse().Take(10).ToList();

                if (prayers == null)
                {
                    prayers = new List<Prayer>();
                }
            }
            catch (System.Exception)
            {

            }

            this.TempData["TopTenPrayers"] = prayers;
        }

        private void PopulatePateriks()
        {

            var pateriks = new List<Paterik>();

            try
            {
                pateriks = this.Context.Pateriks.FindAll().Reverse().ToList();

                if (pateriks == null)
                {
                    pateriks = new List<Paterik>();
                }
            }
            catch (System.Exception)
            {

            }

            this.TempData["Pateriks"] = pateriks;
        }
    }
}