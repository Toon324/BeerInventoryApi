using BeerInventory.Models;
using BeerInventory.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BeerInventory.Controllers
{
    public class UpcController : ApiController
    {
        ProductManagementService beerService = new ProductManagementService();

        // GET api/upc?beerId=X&brewery=Y&beerName=Z
        public IEnumerable<string> Get(string beerId = "", string brewery = "", string beerName = "")
        {
            return null;
        }

        // GET api/upc/5
        public IEnumerable<BeerEntity> Get(string id)
        {
            return beerService.GetBeerForUpc(id);
        }

        // POST api/upc
        public void Post([FromBody]string value)
        {
        }

        // PUT api/upc/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/upc/5
        public void Delete(int id)
        {
        }
    }
}