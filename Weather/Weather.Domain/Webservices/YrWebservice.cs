using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.Domain.Entities;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.IO;
using System.Net;

namespace Weather.Domain.Webservices
{
    public class YrWebservice
    {

        public Location FindLocation(string lon, string lat)
        {
            var urlString = String.Format("http://api.yr.no/weatherapi/locationforecast/1.9/?lat={0};lon={1}", lon, lat);
            return JArray.Parse(urlString).Select(l => new Location(l)).SingleOrDefault();
        }
    
    }
}
