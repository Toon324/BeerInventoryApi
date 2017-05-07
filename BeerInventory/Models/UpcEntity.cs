using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerInventory.Models
{
    public class UpcEntity : TableEntity
    {
        public UpcEntity(string type, string id, string upc)
        {
            PartitionKey = type;
            RowKey = id + "|" +  upc;
            UPC = upc;
            Type = type;
            ID = id;
        }

        public UpcEntity() { }

        public String ID { get; set; }

        public String UPC { get; set; }

        public String Type { get; set; }

    }
}
