using BeerInventory.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace BeerInventory.Services
{
    public class ProductManagementService
    {

        BeerTableService beerService = new BeerTableService();
        BeerDetailsService detailsService = new BeerDetailsService();

        UpcService upcService = new UpcService();

        public IEnumerable<BeerEntity> GetBeerForUpc(string upc)
        {
            return upcService.GetProductIds(upc).Select(x => GetBeerDetails(x));
        }

        public void AddUpcToProduct(string upc, string id, string type) => upcService.AddUpcToProduct(upc, id, type);

        public IEnumerable<BeerEntity> GetAll() => beerService.GetAll();

        public void AddBeerDetails(string upc, string brewer, string beername)
        {
            var beer = AddBeerDetails(brewer, beername);

            AddUpcToProduct(upc, beer.Id, "Beer");
        }

        public BeerEntity GetBeerByName(string brewery, string beerName) => beerService.GetByName(brewery, beerName);

        public void AddCustom(string upc, BeerEntity beer)
        {
            beer.Id = upc;

            Add(beer);

            AddUpcToProduct(upc, beer.Id, "Beer");
        }

        public bool BeerDetailsExists(string id)
        {
            return beerService.GetById(id) != null;
        }

        public BeerEntity GetBeerDetails(string id)
        {
            return beerService.GetById(id);
        }

        public BeerEntity AddBeerDetails(string brewery, string beerName)
        {
            var details = detailsService.FetchBeerInfoFromDb(brewery, beerName);

            var beer = new BeerEntity(details["breweries"][0]["name"], details["id"])
            {
                Name = details["name"],
                ABV = Double.Parse(details["abv"]),
                Availablity = details["available"]["description"],
                Type = details["style"]["name"],
                Glass = details["glass"]["name"],
                Description = details["description"],
                LabelUrl = details["labels"]["medium"],
                BrewSeason = details["available"]["name"]
            };

            Trace.TraceInformation("Adding " + beer.Name + " brewed by " + beer.Brewer);

            beerService.AddOrUpdate(beer);

            return beer;
        }

        internal void Add(BeerEntity beer)
        {
            beerService.AddOrUpdate(beer);
        }
    }
}
