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

        public BeerEntity GetById(String id)
        {
            var results = Table.CreateQuery<BeerEntity>().Where(x => x.RowKey == id).ToList();

            if (!results.Any())
            {
                return null;
            }

            return results.First();
        }

        public List<BeerEntity> GetAll()
        {
            return Table.CreateQuery<BeerEntity>().ToList();
        }

        public void Remove(BeerEntity beer) => Table.Execute(TableOperation.Delete(beer));

        public BeerEntity GetByName(string brewery, string beerName)
        {
            var results = Table.CreateQuery<BeerEntity>()
                .Where(x => x.Brewer == brewery &&
                x.Name == beerName).ToList();

            if (!results.Any())
            {
                return null;
            }

            return results.First();
        }
    }
}
