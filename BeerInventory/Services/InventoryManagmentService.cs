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

        BeerDetailsService beerService = new BeerDetailsService();

        InventoryService inventoryService = new InventoryService();


        public List<BeerEntity> AddBeerToInventory(String owner, String location, String upc, int count)
        {
            var products = upcService.GetProductIds(upc);

            // If multiple products are associated with this barcode, we need help determining what's what, so we return options
            if (products.Count > 1)
            {
                return products.Select(x => beerService.GetBeerDetails(x)).ToList();
            }

            var id = products.First();

            inventoryService.AddBeerToInventory(owner, location, id, count);

            return null;
        }

        public void AddBeerToInventoryById(String owner, String location, String id, int count) => inventoryService.AddBeerToInventory(owner, location, id, count);

        public List<InventoryEntity> GetInventory(String owner) => inventoryService.GetInventory(owner);

        public List<InventoryEntity> GetInventory(String owner, String location) => inventoryService.GetInventory(owner, location);

        public List<InventoryDetails> GetInventoryWithDetails(String owner)
        {
            var toReturn = new List<InventoryDetails>();

            var inventory = GetInventory(owner);

            foreach (var beer in inventory)
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