namespace SvNaum.Web.Models
{
    using System.Web.Mvc;

    public class NewsUpdateModel : NewsInputModel
    {
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }
    }
}