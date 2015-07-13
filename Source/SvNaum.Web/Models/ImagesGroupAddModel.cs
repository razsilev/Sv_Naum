namespace SvNaum.Web.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    
    using SvNaum.Models;

    public class ImagesGroupAddModel
    {
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public List<TwoSizeImageUrls> ImagesUrls { get; set; }
    }
}