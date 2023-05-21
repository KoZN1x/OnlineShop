using OnlineShopOfSportEquipment.Services;
using OnlineShopOfSportEquipment_Models;
using OnlineShopOfSportEquipment_Models.ViewModels;

namespace OnlineShopOfSportEquipment_Tests
{
    [TestFixture]
    public class UIServicesTestsFixture
    {
        [Test]
        public void OrderTotalCounter_Positive_ShouldReturnCorrectValue()
        {
            ProductUserViewModel productUserViewModel = new ProductUserViewModel()
            {
                ProductList = new List<Product>
                {
                    new Product { Name = "Test", Price = 10M, TempCount = 10 },
                    new Product { Name = "Test2", Price = 20M, TempCount = 20 }
                }
            };
            var res = OrderTotalCounter.Count(productUserViewModel);
            Assert.That(res, Is.EqualTo(500M));
        }
    }
}