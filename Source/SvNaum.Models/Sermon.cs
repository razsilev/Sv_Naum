namespace SvNaum.Models
{
    using System;

    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    
    public class Sermon
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Theme { get; set; }

        public DateTime Date { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string ImageUrl { get; set; }

        public string Text { get; set; }
    }
}
