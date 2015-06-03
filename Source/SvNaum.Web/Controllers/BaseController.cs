namespace SvNaum.Web.Controllers
{
    using System.Configuration;
    using System.Web.Mvc;
    using MongoDB.Driver;

    using SvNaum.Data;

    public abstract class BaseController : Controller
    {
        private SvNaumDbContext context;

        public BaseController()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("bg", false);

            var conString = this.GetMongoDbConnectionString();
            this.context = new SvNaumDbContext(conString);
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
                "mongodb://localhost:27017";
        }
    }
}