using BeerInventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerInventory.Services
{
    public class InventoryService
    {
        InventoryTableService inventoryService = new InventoryTableService();

        public bool BeerExistsInInventory(string owner, string location, string upc)
        {
            return inventoryService.Get(owner, location, upc) != null;
        }

        public void AddBeerToInventory(string owner, string location, string upc, int count)
        {
            var stock = inventoryService.Get(owner, location, upc);

            if (stock == null)
            {
                stock = new InventoryEntity(owner, upc)
                {
                    Count = 0,
                    Location = location
                };
            }

            stock.LastAdded = DateTime.Now.Date;
            stock.Count += count;

            inventoryService.AddOrUpdate(stock);
        }

        public List<InventoryEntity> GetInventory(string owner)
        {
            return inventoryService.GetAll(owner);
        }
    }
}