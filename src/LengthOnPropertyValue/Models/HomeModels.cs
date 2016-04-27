using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LengthOnPropertyValue.Models
{
  public class ContactForm
  {
    [Required]
    [Display(Name = "First name")]
    public string FirstName { get; set; }

    [Required]
    [Display(Name = "Last name")]
    public string LastName { get; set; }

    [Required]
    [Display(Name = "Country")]
    public string CountryCode { get; set; }

    public SelectList CountriesList { get; set; }

    [Required]
    [Display(Name = "Province")]
    [LengthOnOtherPropertyValue("CountryCode", "ITA", 2, 50, ErrorMessage = "{0} can be {1} chars long")]
    public string Province { get; set; }
  }
}