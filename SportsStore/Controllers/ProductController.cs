using SportsStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace SportsStore.Controllers
{
    public class ProductController : Controller

    {

        private IProductRepository repository;
        public int PageSize = 4;

        public ProductController(IProductRepository repo)
        {

            repository = repo;
        }

        //  public ViewResult List()=>View(repository.Products);

        public ViewResult List(int productPage = 1)
        {
            return View(repository.Products
                .OrderBy(p => p.ProductID)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize));




        }

    }
}
