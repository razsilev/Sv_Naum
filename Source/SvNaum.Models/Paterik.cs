namespace SvNaum.Models
{
    using System.ComponentModel.DataAnnotations;
    
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Paterik
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Author { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
