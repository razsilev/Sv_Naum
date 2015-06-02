namespace SvNaum.Data
{
    using MongoDB.Driver;

    public class DbConnection
    {
        public const string DbName = "svNaum";
        private MongoDatabase db;

        public DbConnection(string connectionString)
        {
            var client = new MongoClient(connectionString);
            var server = client.GetServer();

            this.Db = server.GetDatabase(DbConnection.DbName);
        }

        public MongoDatabase Db
        {
            get
            {
                return this.db;
            }

            private set
            {
                this.db = value;
            }
        }
    }
}
