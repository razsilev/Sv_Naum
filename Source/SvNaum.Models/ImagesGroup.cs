namespace SvNaum.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    
    public class ImagesGroup
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public List<TwoSizeImageUrls> ImagesUrls { get; set; }
    }
}
