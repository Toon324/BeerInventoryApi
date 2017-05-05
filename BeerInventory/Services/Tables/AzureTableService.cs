using BeerInventory.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace BeerInventory.Services
{
    public abstract class AzureTableService<T> where T : TableEntity
    {
        CloudStorageAccount storageAccount;

        private string TableName { get; set; }

        public AzureTableService(String tableName)
        {
            TableName = tableName;
            storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
        }

        private CloudTableClient GetClient()
        {
            return storageAccount.CreateCloudTableClient();
        }

        protected CloudTable GetTable()
        {
            var client = GetClient();

            var table = client.GetTableReference(TableName);

            table.CreateIfNotExists();

            return table;
        }

        public T GetEntity(string partitionKey, string rowKey)
        {
            var table = GetTable();

            var result = table.Execute(TableOperation.Retrieve<T>(partitionKey, rowKey)).Result;

            return ((T)result);
        }

        public void AddOrUpdate(T entity)
        {
            var table = GetTable();

            table.Execute(TableOperation.InsertOrReplace(entity));
        }
    }
}