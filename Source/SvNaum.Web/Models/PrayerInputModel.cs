namespace SvNaum.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using MongoDB.Bson.Serialization.Attributes;

    public class PrayerInputModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [DataType(DataType.Text)]
        public string TitleSecond { get; set; }

        [AllowHtml]
        [Required]
        [DataType("tinymce_full")]
        [UIHint("tinymce_full")]
        public string Text { get; set; }

        [DataType(DataType.Text)]
        public string Author { get; set; }
    }
}