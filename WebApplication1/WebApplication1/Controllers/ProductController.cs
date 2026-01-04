using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {

        string connectionString = "server=localhost;port=3306;database=netdb;user=root;password=1111";

        public IActionResult Index(string search = "", string brand = "") 
        {
           
            var products = ProductData.GetProducts(connectionString, search, brand);

           
            ViewBag.SearchKeyword = search;
            ViewBag.CurrentBrand = brand;

            return View(products);
        }

        public IActionResult Detail(int id)
        {
       
            var product = ProductData.GetProductById(id, connectionString);

            if (product == null)
                return NotFound();

            return View(product);
        }
    }
}