using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SportsStore.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository repository;
        
        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index() => View(repository.Products);
       
        //GET: Edit
        public ViewResult Edit(int productId)
        {
           return View(repository.Products.FirstOrDefault(p => productId == p.ProductID));
        }
        
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been saved.";
                //holds data until its read, not like viewbag, it ends when http request is done.
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }

        }


    }
}
