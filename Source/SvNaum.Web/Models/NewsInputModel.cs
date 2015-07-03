namespace SvNaum.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class NewsInputModel
    {
        [Required]
        [UIHint("tinymce_full")]
        [AllowHtml]
        public string Content { get; set; }
    }
}