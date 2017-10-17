using ApiAiSDK;
using ApiAiSDK.Model;
using BeerInventory.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace BeerInventory.Services
{
    public class ApiAiService
    {
        InventoryManagmentService inventoryService = new InventoryManagmentService();

        public String HandleResponse(AIResponse response) {

            if (response.Result.Action != "ShowBeerList")
            {
                return "I don't deal with this.";
            }

            var location = response.Result.Parameters["Location"].ToString().ToUpper();

            var name = response.Result.Parameters["given-name"].ToString().ToUpper();

            if (string.IsNullOrEmpty(name))
            {
                name = response.Result.Parameters["name"].ToString().ToUpper();
            }

            if (string.IsNullOrEmpty(name))
            {
                return "Who the hell are you?";
            }

            if (string.IsNullOrEmpty(location))
            {
                return "I have no bloody idea where you want to look";
            }

            var inventory = new List<InventoryDetails>();

            if (string.IsNullOrEmpty(location))
            {
                location = "All";
                inventory = inventoryService.GetInventoryWithDetails(name);
            }
            else
            {
                inventory = inventoryService.GetInventoryWithDetails(name, location);
            }

            var builder = new StringBuilder();

            builder.Append("<speak>");
            builder.Append("<p>In location " + location + ", you have the following</p>");

            for (int x = 0; x < inventory.Count(); x++)
            {
                var beer = inventory[x];
                builder.Append("<p><say-as interpret-as=\"cardinal\">" + beer.Count + "</say-as> of " + beer.Name + "</p>");

                if (x == inventory.Count() - 2)
                {
                    builder.Append(" and ");
                }
                else if (x < inventory.Count() - 2)
                {
                    builder.Append(", ");
                }
            }

            builder.Append("</speak>");

            return builder.ToString();
        }
    }
}
