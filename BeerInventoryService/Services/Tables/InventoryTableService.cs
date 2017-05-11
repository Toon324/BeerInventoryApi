using BeerInventory.Models;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerInventory.Services
{
    public class InventoryTableService : AzureTableService<InventoryEntity>
    {
        public InventoryTableService() : base("Inventory") { }

        private List<InventoryEntity> GetAvailable(String owner, String location)
        {
            var table = GetTable();

            return table.CreateQuery<InventoryEntity>().Where(x => x.PartitionKey == owner && x.Location == location &&  x.Count > 0).ToList();
        }

        public List<InventoryEntity> GetAvailable(String owner)
        {
            var table = GetTable();

            return table.CreateQuery<InventoryEntity>().Where(x => x.PartitionKey == owner && x.Count > 0).ToList();
        }

        public List<InventoryEntity> GetAll(String owner)
        {
            var table = GetTable();

            return table.CreateQuery<InventoryEntity>().Where(x => x.PartitionKey == owner).ToList();
        }

        public List<InventoryEntity> GetAll(String owner, String location)
        {
            var table = GetTable();

            return table.CreateQuery<InventoryEntity>().Where(x => x.PartitionKey == owner && x.Location == location).ToList();
        }

        public InventoryEntity Get(String owner, String location, String id)
        {
            var table = GetTable();

            var results = table.CreateQuery<InventoryEntity>().Where(x => x.PartitionKey == owner && x.Location == location && x.Id == id).ToList();

            if (!results.Any())
            {
                return null;
            }

            return results.First();
        }
    }
}
