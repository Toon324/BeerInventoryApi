using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BeerInventory.Services;
using System.Linq;

namespace BeerInventory.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var service = new BeerDetailsService();
            var beer = service.FetchBeerInfoFromDb("Epic Brewing Company", "Big Bad Baptista");

            Console.WriteLine(beer);
        }

        [TestMethod]
        public void Test2()
        {
            var service = new BeerTableService();

            var results = service.GetAll();

            Assert.IsNotNull(results);

            Assert.IsTrue(results.Any());
        }

        [TestMethod]
        public void AddCustom()
        {
            var service = new ProductManagementService();

            service.AddCustom("JW 0417", new Models.BeerEntity("Glottal Stop", "JW 0417")
            {
                ABV = 6.0,
                Name = "John's Wort Ale",
                BrewYear = 2017,
                Availablity = "Extremely limited release.",
                Description = "A highly custom beer light brown in color. Made with a helping of John's Wort, this beer is pleasant to the senses and goes down smooth.",
                Type = "Ale",
                Glass = "Pilsner",
                LabelUrl = "http://i.imgur.com/8RVZWQ3.png"
            });
        }
    }
}
