using BeerInventory.Models;
using Microsoft.ApplicationInsights;
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
        private string TableName { get; set; }

        protected CloudTable Table { get; set; }

        public AzureTableService(String tableName)
        {
            TableName = tableName;
            var storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);

            var table = storageAccount.CreateCloudTableClient().GetTableReference(TableName);

            table.CreateIfNotExists();

            Table = table;
        }

        public T GetEntity(string partitionKey, string rowKey)
        {
            try
            {
                var result = Table.Execute(TableOperation.Retrieve<T>(partitionKey, rowKey)).Result;

                return ((T)result);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                return null;
            }
        }

        public void AddOrUpdate(T entity)
        {
            try
            {
                Table.Execute(TableOperation.InsertOrReplace(entity));
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
            }
        }
    }
}