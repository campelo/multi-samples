using Microsoft.AspNetCore.Mvc;
using MVC.HoldSessionInfo.Models;
using System.Text;
using System.Text.Json;

namespace MVC.HoldSessionInfo.Controllers;

public class EnterpriseController : Controller
{
    public ActionResult Index()
    {
        var enterprise = new EnterpriseViewModel();
        HttpContext.Session.TryGetValue("Enterprise", out byte[] enterpriseData);
        if (enterpriseData is not null)
        {
            string jEnterprise = Encoding.UTF8.GetString(enterpriseData);
            enterprise = JsonSerializer.Deserialize<EnterpriseViewModel>(jEnterprise);
        }
        return View(enterprise);
    }

    [HttpPost]
    public IActionResult SetEnterprise(EnterpriseViewModel enterprise)
    {
        string jEnterprise = JsonSerializer.Serialize(enterprise);
        byte[] enterpriseData = Encoding.UTF8.GetBytes(jEnterprise);
        HttpContext.Session.Set("Enterprise", enterpriseData);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult EditEnterprise(EnterpriseViewModel model)
    {
        if (!ModelState.IsValid)
            return View("Index", model);

        // ...save enterprise
        return View("Index", model);
    }

}
