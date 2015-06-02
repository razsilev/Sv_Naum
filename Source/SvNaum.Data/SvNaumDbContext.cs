﻿namespace SvNaum.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using SvNaum.Models;

    using MongoDB.Driver;

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
    }
}