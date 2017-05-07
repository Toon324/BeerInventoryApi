using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using BeerInventory.Services;
using System.Text;

namespace BeerInventory.Controllers
{
    public class InventoryController : ApiController
    {
        InventoryManagmentService inventoryService = new InventoryManagmentService();

        // GET api/inventory/user?location=X
        public IEnumerable<string> Get(String id, String location)
        {
            return inventoryService.GetInventoryWithDetails(id).Select(x => x.ToString());
        }

        // GET api/inventory/user
        [SwaggerOperation("GetById")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IEnumerable<string> Get(String id)
        {
            return inventoryService.GetInventoryWithDetails(id).Select(x => x.ToString());
        }

        // POST api/inventory/5?user=X&location=X
        [SwaggerOperation("Create")]
        [SwaggerResponse(HttpStatusCode.Created)]
        public void Post(String id, String user, String location)
        {
            inventoryService.AddBeerToInventory(user, location, id, 1);
        }

        // POST api/inventory/5?user=X&location=X&count=5
        [SwaggerOperation("Create")]
        [SwaggerResponse(HttpStatusCode.Created)]
        public void Post(String id, String user, String location, int count)
        {
            inventoryService.AddBeerToInventory(user, location, id, count);
        }

        // PUT api/inventory/5
        [SwaggerOperation("Update")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/inventory/5
        [SwaggerOperation("Delete")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public void Delete(int id)
        {
        }
    }
}
