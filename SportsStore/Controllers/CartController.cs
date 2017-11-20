using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using SportsStore.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using SportsStore.Models.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SportsStore.Controllers
{
    #region before simplifying
    //public class CartController : Controller
    //{
    //    private IProductRepository repository;
    //    public CartController(IProductRepository repo)
    //    {
    //        repository = repo;
    //    }

    //    public ViewResult Index(string returnUrl)
    //    {

    //        return View(new CartIndexViewModel {
    //            ReturnUrl = returnUrl,
    //            Cart = GetCart()
    //        });
    //    }
    //    public RedirectToActionResult AddToCart (int productId,string returnUrl) { 
    //              Product product = repository.Products.FirstOrDefault(x => x.ProductID == productId);

    //        if (product!=null)
    //        {
    //            Cart cart = GetCart();
    //            cart.AddItem(product, 1);
    //            SaveCart(cart);

    //        }
    //        return RedirectToAction("Index", new { returnUrl });

    //    }

    //    public RedirectToActionResult RemoveFromCart(int productId,string returnUrl)
    //    {
    //        Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
    //        if (product!=null)
    //        {
    //            Cart cart = GetCart();
    //            cart.RemoveLine(product);
    //            SaveCart(cart);
    //        }
    //        return RedirectToAction("Index", new { returnUrl });
    //    }

    //    private Cart GetCart()
    //    {
    //        Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
    //        return cart;
    //    }
    //    private void SaveCart(Cart cart)
    //    {
    //        HttpContext.Session.SetJson("Cart", cart);
    //    }
    //} 
    #endregion



    public class CartController : Controller
    {
        private IProductRepository repository;
        private Cart cart;
        public CartController(IProductRepository repo, Cart cartService)
        {
            repository = repo;
            cart = cartService;
        }
        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product product = repository.Products
            .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public RedirectToActionResult RemoveFromCart(int productId,
        string returnUrl)
        {
            Product product = repository.Products
            .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
    }

}
