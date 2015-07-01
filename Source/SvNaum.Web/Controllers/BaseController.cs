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

        public BaseController()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("bg", false);

            var conString = this.GetMongoDbConnectionString();
            this.context = new SvNaumDbContext(conString);

            this.PopulateTopTenSermons();
            this.PopulateTopTenBreviaries();
        }

        public SvNaumDbContext Context
        {
            get
            {
                return context;
            }
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

            this.ViewBag.TopTenSermons = sermons;
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

            this.ViewBag.TopTenPrayers = prayers;
        }
    }
}