namespace SvNaum.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class SermonUpdateInputModel : SermonInputModel
    {
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime Date { get; set; }
    }
}