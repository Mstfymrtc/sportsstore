using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SportsStore.Infrastructure;
/*The SessionCart class subclasses the Cart class and overrides the AddItem, RemoveLine, and Clear
methods so they call the base implementations and then store the updated state in the session using the
extension methods on the ISession interface I defined in Chapter 9*/
namespace SportsStore.Models
{
    public class SessionCart : Cart
    {
        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            SessionCart cart = session?.GetJson<SessionCart>("Cart") ?? new SessionCart();
            cart.Session = session;
            return cart;
        }

        [JsonIgnore]
        public ISession Session { get; set; }


        public override void AddItem(Product product, int quantity)
        {
            base.AddItem(product, quantity);
            Session.SetJson("Cart", this);
        }

        /*NEDEN CART SINIFIYLA OLUŞTURMAMIZA RAĞMEN*/
        /*METHOD ÇAĞIRILDIĞINDA DİREKT OLARAK*/
        /*SESSION CART CLASSININ METHODLARI*/
        /*ÇALIŞIYOR???*/

        public override void Clear()
        {
            base.Clear();
            Session.Remove("Cart");


        }

        public override void RemoveLine(Product product)
        {
            base.RemoveLine(product);
            Session.SetJson("Cart", this);
        }
    }
}
