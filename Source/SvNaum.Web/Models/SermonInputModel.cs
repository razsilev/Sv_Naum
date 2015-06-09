namespace SvNaum.Web.Models
{
    using MongoDB.Bson.Serialization.Attributes;
    using System;
    using System.ComponentModel.DataAnnotations;
    
    public class SermonInputModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string Theme { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Author { get; set; }

        //public string ImageUrl { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
    }
}