using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerInventory.Models
{
    public class BeerEntity : TableEntity
    {
        public BeerEntity(string brewer, string beername)
        {
            PartitionKey = brewer;
            RowKey = beername;
            Brewer = brewer;
            Name = beername;
        }

        public BeerEntity() { }

        public string Brewer { get; set; }

        public string Name { get; set; }

        public double ABV { get; set; }

        public string Type { get; set; }

        public string Glass { get; set; }

        public string Description { get; set; }

        public string Barcode { get; set; }

        public string LabelUrl { get; set; }

        public string Availablity { get; set; }

        public int BrewYear { get; set; }

        public string BrewSeason { get; set; }


        public override string ToString()
        {
            return "[" + Barcode + "] " + Brewer + " - " + Name;
        }
    }
}