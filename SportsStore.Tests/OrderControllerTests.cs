using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using Xunit;

namespace SportsStore.Tests
{
    class OrderControllerTests
    {


        [Fact]
        public void Cannot_Checkout_Empty_Cart()
        {
            //Arrange

            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            //Arrange
            Cart cart = new Cart();
            Order order = new Order();
            OrderController target = new OrderController(mock.Object, cart);

            //Act

            ViewResult result = target.Checkout(order) as ViewResult;

            
            // Assert - check that the order hasn't been stored
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
            // Assert - check that the method is returning the default view
            Assert.True(string.IsNullOrEmpty(result.ViewName));
            // Assert - check that I am passing an invalid model to the view
            Assert.False(result.ViewData.ModelState.IsValid);
        }
    }
}
