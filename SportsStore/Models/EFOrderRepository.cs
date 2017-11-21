using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class EFOrderRepository : IOrderRepository
    {

        private ApplicationDbContext context;

        public EFOrderRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Order> Orders => context.Orders
            .Include(o => o.Lines).ThenInclude(l => l.Product);
                        
            //orders modeli içinde cartline tipinde navigation property var ve
            //cartline içinde de product tipinden navigation property var!!


        public void SaveOrder(Order order)
        { // order ekleme çıkartma var şu anlık, onun için saveOrder var.

            context.AddRange(order.Lines.Select(l => l.Product));
            // bu seçilenleri tracked olarak işaretliyor, zaten var olan product
            //nesnelerinin tekrar database e yazılmasını engellemek amacıyla tracked olarak işaretliyoruz.

            if (order.OrderID==0)//?
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }
    }
}
