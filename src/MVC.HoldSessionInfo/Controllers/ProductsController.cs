using Microsoft.AspNetCore.Mvc;
using MVC.HoldSessionInfo.Models;
using System.Text;
using System.Text.Json;

namespace MVC.HoldSessionInfo.Controllers;

public class ProductsController : Controller
{
    public ActionResult Index()
    {
        var product = new Product();
        HttpContext.Session.TryGetValue("Product", out byte[] productData);
        if (productData is not null)
        {
            string jProduct = Encoding.UTF8.GetString(productData);
            product = JsonSerializer.Deserialize<Product>(jProduct);
        }
        return View(product);
    }

    [HttpPost]
    public IActionResult SetProduct(Product product)
    {
        string jProduct = JsonSerializer.Serialize(product);
        byte[] productData = Encoding.UTF8.GetBytes(jProduct);
        HttpContext.Session.Set("Product", productData);
        return RedirectToAction("Index");
    }

}
