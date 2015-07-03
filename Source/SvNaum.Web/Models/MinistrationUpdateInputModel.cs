namespace SvNaum.Web.Models
{
    using System.Web.Mvc;

    public class MinistrationUpdateInputModel : MinistrationInputModel
    {
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }
    }
}