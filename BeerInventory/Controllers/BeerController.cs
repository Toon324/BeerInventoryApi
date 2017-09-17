using BeerInventory.Models;
using BeerInventory.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace BeerInventory.Controllers
{
    public class BeerController : ApiController
    {
        InventoryManagmentService inventoryService = new InventoryManagmentService();
        ProductManagementService beerService = new ProductManagementService();

        // GET api/beer
        public IEnumerable<BeerEntity> Get()
        {
            return beerService.GetAll();
        }

        // GET api/beer/5
        public BeerEntity Get(String id)
        {
            return beerService.GetBeerDetails(id);
        }

        // GET api/beer?brewery=X&beerName=X
        public BeerEntity Get(String brewery, String beerName)
        {
            return beerService.GetBeerByName(brewery, beerName);
        }

        // POST api/beer?upc=X
        public void Post([FromBody]BeerEntity value, String upc)
        {
            beerService.AddCustom(upc, value);
        }

        // PUT api/beer/5?brewer=X&beerName=X
        public void Put(string brewer, string beerName, string id)
        {
            beerService.AddBeerDetails(id, brewer, beerName);
        }

        // DELETE api/beer/5
        public void Delete(int id)
        {
        }
    }
}
