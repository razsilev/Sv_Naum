namespace SvNaum.Models
{
    using System;

    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System.ComponentModel.DataAnnotations;

    public class Prayer
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string TitleSecond { get; set; }

        public string Author { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
