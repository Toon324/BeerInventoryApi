using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerInventory.Models
{
    public class InventoryEntity : TableEntity
    {
        public InventoryEntity(String owner, String UPC)
        {
            PartitionKey = owner;
            RowKey = UPC;

            Owner = owner;
            Barcode = UPC;
        }

        public InventoryEntity() { }

        public string Barcode { get; set; }

        public string Owner { get; set; }

        public string Location { get; set; }

        public int Count { get; set; }

        public DateTime LastAdded { get; set; }

        public override string ToString()
        {
            return "[" + Barcode + "] " + Owner + " | " + Location + " : " + Count + "  Last added: " + LastAdded.ToString("MM/dd/yyyy");
        }
    }
}