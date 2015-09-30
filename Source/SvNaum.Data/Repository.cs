namespace SvNaum.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    
    using MongoDB.Driver;
    using MongoDB.Bson;
    using MongoDB.Driver.Builders;

    public class Repository
    {
        public T FindOneById<T>(MongoCollection<T> collection, string id) where T : class
        {
            T result = null;

            try
            {
                result = collection.FindOneById(ObjectId.Parse(id));
            }
            catch (Exception)
            {

            }

            return result;
        }

        public MongoCursor<T> FindAll<T>(MongoCollection<T> collection)
        {
            MongoCursor<T> result = null;

            try
            {
                result = collection.FindAll();
            }
            catch (Exception)
            {

            }

            return result;
        }

        public void Insert<T>(MongoCollection<T> collection, T data)
        {
            collection.Insert(data);
        }

        public void Update<T>(MongoCollection<T> collection, T data, string id)
        {
            collection.Insert(data);

            this.Delete(collection, id);
        }

        public void Delete(MongoCollection collection, string id)
        {
            IMongoQuery query = this.CreateQueryById(id);

            collection.Remove(query);
        }

        private IMongoQuery CreateQueryById(string id)
        {
            MongoDB.Driver.IMongoQuery query = Query.EQ("_id", ObjectId.Parse(id));

            return query;
        }
    }
}
