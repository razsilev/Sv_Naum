namespace SvNaum.Web.Models
{
    using System.Web.Mvc;

    public class PrayerUpdateInputModel : PrayerInputModel
    {
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }
    }
}