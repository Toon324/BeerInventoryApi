using Microsoft.WindowsAzure.Storage.Table;

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

        public string ID { get; set; }

        public string UPC { get; set; }

        public string Type { get; set; }

    }
}
