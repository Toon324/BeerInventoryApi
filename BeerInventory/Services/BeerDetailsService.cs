using BeerInventory.Models;
using BreweryDB;
using BreweryDB.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace BeerInventory.Services
{
    public class BeerDetailsService
    {
        BeerTableService beerService = new BeerTableService();

        private BreweryDbClient GetClient()
        {
            Trace.TraceInformation("Using BreweryDB Key " + ConfigurationManager.AppSettings["BreweryDbKey"]);
            return new BreweryDbClient(ConfigurationManager.AppSettings["BreweryDbKey"]);
        }

        public Beer FetchBeerInfoFromDb(String brewery, String beer)
        {
            Trace.TraceInformation("Searching for " + brewery + " " + beer);
            var client = GetClient();
            var query = client.Beers.Search(brewery + " " + beer);
            var results = query.Result;
            Trace.TraceInformation("Found " + results.TotalResults + " results");
            return results.Data.First();
        }

        public bool BeerDetailsExists(string upc)
        {
            return beerService.GetByUpc(upc) != null;
        }

        public BeerEntity GetBeerDetails(string upc)
        {
            return beerService.GetByUpc(upc);
        }

        public BeerEntity AddBeerDetails(string upc, string brewery, string beerName)
        {
            var details = FetchBeerInfoFromDb(brewery, beerName);

            Trace.TraceInformation("Search found " + details.Name + " made by " + details.Brewery);

            var beer = new BeerEntity(details.Brewery, upc)
            {
                Name = details.Name,
                ABV = details.Abv,
                Availablity = details.Available.Description,
                Type = details.Style.Name,
                Glass = details.Glass.Name,
                Description = details.Description,
                LabelUrl = details.Labels.Medium,
                BrewSeason = details.Available.Name
            };

            beerService.AddOrUpdate(beer);

            return beer;
        }
    }
}
