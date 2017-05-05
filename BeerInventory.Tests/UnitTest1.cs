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

        [TestMethod]
        public void Test2()
        {
            var service = new BeerDetailsService();

            var beer = service.GetBeerDetails("793866530018");

            Console.WriteLine();
        }
    }
}
