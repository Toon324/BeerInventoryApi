using BeerInventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerInventory.Services
{
    public class InventoryManagmentService
    {
        UpcService upcService = new UpcService();

        ProductManagementService beerService = new ProductManagementService();

        InventoryTableService inventoryService = new InventoryTableService();

        public bool BeerExistsInInventory(string owner, string location, string id)
        {
            return inventoryService.Get(owner, location, id) != null;
        }

        public List<BeerEntity> AddBeerToInventory(String owner, String location, String upc, int count)
        {
            var products = upcService.GetProductIds(upc);

            if (!products.Any())
            {
                try
                {
                    var beer = beerService.GetBeerDetails(upc);
                    if (beer != null)
                    {
                        products.Add(upc);
                    }
                }
                catch (Exception e)
                {
                    
                }
            }

            if (!products.Any())
            {
                return null;
            }

            // If multiple products are associated with this barcode, we need help determining what's what, so we return options
            if (products.Count > 1)
            {
                return products.Select(x => beerService.GetBeerDetails(x)).ToList();
            }

            var id = products.First();

            AddBeerToInventoryById(owner, location, id, count);

            return new List<BeerEntity> { beerService.GetBeerDetails(id) };
        }

        private void AddBeerToInventoryById(string owner, string location, string id, int count)
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

        public List<InventoryEntity> GetInventory(String owner) => inventoryService.GetAll(owner).Where(x => !String.IsNullOrEmpty(x.Id)).ToList();

        public List<InventoryEntity> GetInventory(String owner, String location) => inventoryService.GetAll(owner, location).Where(x => !String.IsNullOrEmpty(x.Id)).ToList();

        public List<InventoryDetails> GetInventoryWithDetails(String owner)
        {
            var toReturn = new List<InventoryDetails>();

            var inventory = GetInventory(owner);

            foreach (var beer in inventory.Where(x => x.Count > 0))
            {
                toReturn.Add(new InventoryDetails(beerService.GetBeerDetails(beer.Id), beer));
            }

            return toReturn;
        }

        public List<InventoryDetails> GetInventoryWithDetails(String owner, String location)
        {
            var toReturn = new List<InventoryDetails>();

            var inventory = GetInventory(owner, location);

            foreach (var beer in inventory)
            {
                toReturn.Add(new InventoryDetails(beerService.GetBeerDetails(beer.Id), beer));
            }

            return toReturn;
        }
    }
}
