using BeerInventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerInventory.Services
{
    public class UpcService
    {
        UpcTableService upcService = new UpcTableService();

        public List<String> GetProductIds(string upc)
        {
            return upcService.GetByUpc(upc).Select(x => x.ID).ToList();
        }

        public void AddUpcToProduct(string upc, string id, string type)
        {
            upcService.AddOrUpdate(new UpcEntity(type, id, upc));
        }
    }
}
