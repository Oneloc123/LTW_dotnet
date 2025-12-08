using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers { 
    public class ProductController : Controller
    {
    public IActionResult Index()
    {
        var products = ProductData.getAll(); 
        return View(products);
    }

    public IActionResult Detail(int id)
    {
        var product = ProductData.getIdProduct(id);

        if (product == null)
            return NotFound();

        return View(product);
    }
}}

