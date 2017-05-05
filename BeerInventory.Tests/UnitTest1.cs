using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BeerInventory.Services;

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
    }
}
