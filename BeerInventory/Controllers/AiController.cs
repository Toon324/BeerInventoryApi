using ApiAiSDK.Model;
using BeerInventory.Services;
using Newtonsoft.Json;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BeerInventory.Controllers
{
    public class AiController : ApiController
    {

        ApiAiService aiService = new ApiAiService();

        // POST api/ai
        public HttpResponseMessage Post([FromBody]AIResponse value)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);

            var toReturn = aiService.HandleResponse(value);

            var fulfillment = new Fulfillment()
            {
                Speech = toReturn,
                DisplayText = toReturn,
                Source = "BeerInventory"
            };

            response.Content = new StringContent(JsonConvert.SerializeObject(fulfillment), System.Text.Encoding.UTF8, "application/json");
            response.StatusCode = HttpStatusCode.OK;

            return response;
        }
    }
}