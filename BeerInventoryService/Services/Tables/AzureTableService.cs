using BeerInventory.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BeerInventory.Services
{
    public abstract class AzureTableService<T> where T : TableEntity
    {
        CloudStorageAccount storageAccount;

        private string TableName { get; set; }

        public AzureTableService(String tableName)
        {
            TableName = tableName;
            storageAccount = CloudStorageAccount.Parse(
                "DefaultEndpointsProtocol=https;" + 
                "AccountName=cswendrowski; + " +
                "AccountKey=ignDtpPsRpR24UobZz8x0Za9FxhxYfrkgKchCbJsIUKzoafkv7ciCqUwAWaNsfeAgxH3qZt12zy1/TsRgJ6j9w==;" + 
                "EndpointSuffix=core.windows.net");
        }

        private CloudTableClient GetClient()
        {
            return storageAccount.CreateCloudTableClient();
        }

        protected CloudTable GetTable()
        {
            var client = GetClient();

            var table = client.GetTableReference(TableName);

            table.CreateIfNotExistsAsync().Wait();

            return table;
        }

        public T GetEntity(string partitionKey, string rowKey)
        {
            var table = GetTable();

            var result = table.ExecuteAsync(TableOperation.Retrieve<T>(partitionKey, rowKey)).Result;

            return ((T)result.Result);
        }

        public void AddOrUpdate(T entity)
        {
            var table = GetTable();

            table.ExecuteAsync(TableOperation.InsertOrReplace(entity)).Wait();
        }
    }
}