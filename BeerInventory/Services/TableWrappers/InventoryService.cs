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

        public bool BeerExistsInInventory(string owner, string location, string id)
        {
            return inventoryService.Get(owner, location, id) != null;
        }

        public void AddBeerToInventory(string owner, string location, string id, int count)
        {
            var stock = inventoryService.Get(owner, location, id);

            if (stock == null)
            {
                stock = new InventoryEntity(owner, location, id)
                {
                    Count = 0
                };
            }

            stock.LastAdded = DateTime.Now.Date;

            stock.Count += count;

            if (stock.Count < 0)
            {
                stock.Count = 0;
            }

            inventoryService.AddOrUpdate(stock);
        }

        public List<InventoryEntity> GetInventory(string owner)
        {
            return inventoryService.GetAll(owner).Where(x => String.IsNullOrEmpty(x.Id)).ToList();
        }

        public List<InventoryEntity> GetInventory(string owner, string location)
        {
            return inventoryService.GetAll(owner, location).Where(x => String.IsNullOrEmpty(x.Id)).ToList();
        }
    }
}