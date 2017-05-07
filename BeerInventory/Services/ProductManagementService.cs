using BeerInventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerInventory.Services
{
    public class ProductManagementService
    {

        BeerDetailsService beerService = new BeerDetailsService();

        UpcService upcService = new UpcService();

        public IEnumerable<BeerEntity> GetBeerForUpc(string upc)
        {
            return upcService.GetProductIds(upc).Select(x => beerService.GetBeerDetails(x));
        }

        public void AddUpcToProduct(string upc, string id, string type) => upcService.AddUpcToProduct(upc, id, type);

        public IEnumerable<BeerEntity> GetAll() => beerService.GetAll();

        public void AddBeerDetails(string upc, string brewer, string beername)
        {
            var beer = beerService.AddBeerDetails(brewer, beername);

            AddUpcToProduct(upc, beer.Id, "Beer");
        }

        public void AddCustom(string upc, BeerEntity beer)
        {
            beer.Id = upc;

            beerService.Add(beer);

            AddUpcToProduct(upc, beer.Id, "Beer");
        }
    }
}
