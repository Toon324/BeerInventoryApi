using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BeerInventory.RestInterfaces
{
    public interface IBeerRateApi
    {
        [Get("/upc.asp?k=2e1ob20b6n0gc999r")]
        Task<dynamic> GetByUpc(string upc);
    }

    public class BeerRateApi
    {
        public const string ApiUrl = "https://www.ratebeer.com/json";
    }
}