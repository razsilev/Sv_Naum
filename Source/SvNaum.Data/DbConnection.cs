namespace SvNaum.Data
{
    using MongoDB.Driver;

    public class DbConnection
    {
        public const string DbName = "appharbor_1aca7d3f-ed85-4975-8b3f-56a35407755d";
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
