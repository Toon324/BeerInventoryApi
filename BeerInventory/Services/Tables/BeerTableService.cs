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
            var table = GetTable();

            var results = table.CreateQuery<BeerEntity>().Where(x => x.RowKey == id).ToList();

            if (!results.Any())
            {
                return null;
            }

            return results.First();
        }

        public IEnumerable<BeerEntity> GetAll()
        {
            var table = GetTable();

            var results = table.CreateQuery<BeerEntity>().ToList();

            return results;
        }

        public void Remove(BeerEntity beer)
        {
            var table = GetTable();

            table.Execute(TableOperation.Delete(beer));
        }
    }
}