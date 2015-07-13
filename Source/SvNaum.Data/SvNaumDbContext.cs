namespace SvNaum.Data
{
    using MongoDB.Driver;

    using SvNaum.Models;

    public class SvNaumDbContext
    {
        private MongoDatabase db;

        public SvNaumDbContext(string connectionString)
        {
            var dbConnecton = new DbConnection(connectionString);

            this.db = dbConnecton.Db;
        }

        public MongoCollection<Ministration> Ministration
        {
            get
            {
                return this.db.GetCollection<Ministration>("Ministrations");
            }
        }

        public MongoCollection<Sermon> Sermons
        {
            get
            {
                return this.db.GetCollection<Sermon>("Sermons");
            }
        }

        public MongoCollection<Prayer> Prayers
        {
            get
            {
                return this.db.GetCollection<Prayer>("Breviary");
            }
        }

        public MongoCollection<NewsSingle> News
        {
            get
            {
                return this.db.GetCollection<NewsSingle>("News");
            }
        }

        public MongoCollection<Paterik> Pateriks
        {
            get
            {
                return this.db.GetCollection<Paterik>("Pateriks");
            }
        }

        public MongoCollection<ImagesGroup> ImagesGroups
        {
            get
            {
                return this.db.GetCollection<ImagesGroup>("ImagesGroup");
            }
        }
    }
}