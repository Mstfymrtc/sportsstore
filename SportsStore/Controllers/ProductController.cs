using SportsStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace SportsStore.Controllers
{
    public class ProductController : Controller

    {

        private IProductRepository repository;

        public ProductController(IProductRepository repo)
        {

            repository = repo;
        }

        public ViewResult List()=>View(repository.Products);
    }
}
