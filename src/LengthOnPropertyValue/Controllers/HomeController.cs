using System.Collections.Generic;
using System.Web.Mvc;
using LengthOnPropertyValue.Models;

namespace LengthOnPropertyValue.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      var model = new ContactForm();
      model.CountryCode = "ITA";
      HidratateLists(model);
      return View(model);
    }

    private static void HidratateLists(ContactForm model)
    {
      model.CountriesList = new SelectList(new Dictionary<string, string>()
      {
          { "ITA", "Italy" },
          { "GBP", "Great Britain" },
          { "FRA", "France" }
      }, "Key", "Value", model.CountryCode);
    }

    [HttpPost]
    public ActionResult Index(ContactForm model)
    {
      if (ModelState.IsValid)
        return RedirectToAction("Thanks", model);
      HidratateLists(model);
      return View(model);
    }

    public ActionResult Thanks(ContactForm model)
    {
      return View(model);
    }
  }
}