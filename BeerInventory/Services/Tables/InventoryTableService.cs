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

        private List<InventoryEntity> GetAvailable()
        {
            var table = GetTable();

            var results = table.ExecuteQuery(new TableQuery<InventoryEntity>()
                .Where(TableQuery.GenerateFilterConditionForInt("Count", QueryComparisons.GreaterThan, 0)));

            return results.ToList();
        }

        public List<InventoryEntity> GetAvailable(String owner)
        {
            var table = GetTable();

            var results = table.ExecuteQuery(new TableQuery<InventoryEntity>()
                .Where(TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, owner),
                    TableOperators.And,
                    TableQuery.GenerateFilterConditionForInt("Count", QueryComparisons.GreaterThan, 0)
                    )));

            if (!results.Any())
            {
                return new List<InventoryEntity>();
            }

            return results.ToList();
        }

        public List<InventoryEntity> GetAll(String owner)
        {
            var table = GetTable();

            var results = table.ExecuteQuery(new TableQuery<InventoryEntity>()
                .Where(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, owner)
                    ));

            if (!results.Any())
            {
                return new List<InventoryEntity>();
            }

            return results.ToList();
        }

        public InventoryEntity Get(String owner, String location, String upc)
        {
            var table = GetTable();

            var results = table.ExecuteQuery(new TableQuery<InventoryEntity>()
                .Where(
                TableQuery.CombineFilters(
                    TableQuery.CombineFilters(
                        TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, owner),
                        TableOperators.And,
                        TableQuery.GenerateFilterCondition("Location", QueryComparisons.GreaterThan, location)),
                TableOperators.And,
                TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, upc)
                    )));

            if (!results.Any())
            {
                return null;
            }

            return results.First();
        }

        public void AddToStock(String location, String upc)
        {
            var table = GetTable();

            var result = table.Execute(TableOperation.Retrieve<InventoryEntity>(location, upc)).Result;

            var entity = (InventoryEntity)result;

            entity.Count++;
            entity.LastAdded = DateTime.Now.Date;

            table.Execute(TableOperation.Replace(entity));
        }
    }
}