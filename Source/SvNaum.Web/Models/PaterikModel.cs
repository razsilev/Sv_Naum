namespace SvNaum.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class PaterikModel
    {
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        [Required]
        [MinLength(3)]
        [DataType(DataType.Text)]
        public string Author { get; set; }

        [Required]
        [AllowHtml]
        [UIHint("tinymce_full")]
        public string Text { get; set; }
    }
}