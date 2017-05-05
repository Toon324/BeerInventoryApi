using BreweryDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BreweryDB.Interfaces;

namespace BeerInventory.Models
{
    public class DbBeer : Beer
    {
        public DbBeer() : base(null, null, null, null, null, null, null)
        { }

        public DbBeer(IGlass glass, ISrm srm, IAvailable available, IStyle style, List<Brewery> breweries, ILabels labels, List<SocialAccount> socialAccounts) : base(glass, srm, available, style, breweries, labels, socialAccounts)
        {
        }

        public new List<ISocialAccount> SocialAccounts { get; set; }
        public new Labels Labels { get; set; }
        public new List<Brewery> Breweries { get; set; }
        public new Style Style { get; set; }
        public new Available Available { get; set; }
        public new Srm Srm { get; set; }
        public new Glass Glass { get; set; }
    }
}