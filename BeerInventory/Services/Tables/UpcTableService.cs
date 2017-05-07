using BeerInventory.Models;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerInventory.Services
{
    public class UpcTableService : AzureTableService<UpcEntity>
    {
        public UpcTableService() : base("UpcLookup") { }

        public List<UpcEntity> GetByUpc(String upc)
        {
            var table = GetTable();

            var results = table.CreateQuery<UpcEntity>().Where(x => x.UPC == upc).ToList();

            if (!results.Any())
            {
                return new List<UpcEntity>();
            }

            return results;
        }

        public UpcEntity GetEntity(String type, String id, String upc)
        {
            return GetEntity(type, id + "|" + upc);
        }
    }
}
