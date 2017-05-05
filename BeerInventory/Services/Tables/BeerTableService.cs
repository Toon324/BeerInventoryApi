using BeerInventory.Models;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerInventory.Services
{
    public class BeerTableService : AzureTableService<BeerEntity>
    {
        public BeerTableService() : base("BeerDetails") { }

        public BeerEntity GetByUpc(String upc)
        {
            var table = GetTable();

            var results = table.CreateQuery<BeerEntity>().Where(x => x.RowKey == upc).ToList();

            if (!results.Any())
            {
                return null;
            }

            return results.First();
        }

    }
}