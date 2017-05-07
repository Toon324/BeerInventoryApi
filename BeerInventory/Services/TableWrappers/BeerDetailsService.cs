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

            var result = client.Execute<ResponseContainer<List<dynamic>>>(request);
            return result.Data.Data;
        }

        public IEnumerable<BeerEntity> GetAll() => beerService.GetAll();

        internal void Add(BeerEntity beer)
        {
            beerService.AddOrUpdate(beer);
        }

        public dynamic FetchBeerInfoFromDb(String brewery, String beer)
        {
            Trace.TraceInformation("Searching for " + brewery + " " + beer);
            var results = Search(brewery + " " + beer);
            Trace.TraceInformation("Found " + results.Count() + " results");
            return results.First();
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
            var details = FetchBeerInfoFromDb(brewery, beerName);

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
    }
}
