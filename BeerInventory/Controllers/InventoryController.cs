using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using BeerInventory.Services;

namespace BeerInventory.Controllers
{
    public class InventoryController : ApiController
    {
        InventoryService inventoryService = new InventoryService();
        BeerDetailsService beerService = new BeerDetailsService();

        // GET api/iventory
        [SwaggerOperation("GetAll")]
        public IEnumerable<string> Get()
        {
            return inventoryService.GetInventory("Cody").Select(x => x.ToString());
        }

        // GET api/iventory/5
        [SwaggerOperation("GetById")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public string Get(String id)
        {
            return "";
        }

        // POST api/iventory/AddBeer
        [SwaggerOperation("Create")]
        [SwaggerResponse(HttpStatusCode.Created)]
        public void AddBeer(string brewer, string beerName, string upc)
        {
            beerService.AddBeerDetails(upc, brewer, beerName);
            inventoryService.AddBeerToInventory("Cody", "Fridge", upc, 1);
        }

        // PUT api/iventory/5
        [SwaggerOperation("Update")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/iventory/5
        [SwaggerOperation("Delete")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public void Delete(int id)
        {
        }
    }
}
