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
                //"mongodb://localhost:27017";
                "mongodb://appharbor_1aca7d3f-ed85-4975-8b3f-56a35407755d:t9q99k30fvct2u51kt09jv1pqa@ds039231.mongolab.com:39231/appharbor_1aca7d3f-ed85-4975-8b3f-56a35407755d";
        }
    }
}