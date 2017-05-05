using BeerInventory.Models;
using BreweryDB;
using BreweryDB.Models;
using RestSharp;
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

        public List<dynamic> Search(string query)
        {
            var client = new RestClient("http://api.brewerydb.com");
            var request = new RestRequest("/v2/search", Method.GET);
            request.AddQueryParameter("key", ConfigurationManager.AppSettings["BreweryDbKey"]);
            request.AddQueryParameter("q", query);
            request.AddQueryParameter("type", "beer");
            request.AddQueryParameter("withBreweries", "y");
            request.AddQueryParameter("hasLabels", "y");
            request.AddQueryParameter("withIngredients", "y");
            request.AddQueryParameter("withGuilds", "y");

            var result = client.Execute<ResponseContainer<List<dynamic>>>(request);
            return result.Data.Data;
        }

        public dynamic FetchBeerInfoFromDb(String brewery, String beer)
        {
            Trace.TraceInformation("Searching for " + brewery + " " + beer);
            var results = Search(brewery + " " + beer);
            Trace.TraceInformation("Found " + results.Count() + " results");
            return results.First();
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

            var beer = new BeerEntity(details["breweries"][0]["name"], upc)
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
    }
}
