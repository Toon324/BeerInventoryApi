using BeerInventory.Models;
using BreweryDB;
using BreweryDB.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;

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

        public dynamic FetchBeerInfoFromDb(String brewery, String beer)
        {
            try
            {
                Trace.TraceInformation("Searching for " + brewery + " " + beer);
                var results = Search(brewery + " " + beer);
                Trace.TraceInformation("Found " + results.Count() + " results");
                return results.First();
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                return null;
            }
        }
    }
}
