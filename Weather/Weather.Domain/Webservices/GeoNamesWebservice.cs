using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Weather.Domain.Entities;

namespace Weather.Domain.Webservices
{
    public class GeoNamesWebservice
    {
        public IEnumerable<Location> FindLocation(string name)
        {
            String searchObject;
            var urlString = String.Format("http://api.geonames.org/search?name={0}&maxRows=100&style=full&featureClass=P&type=json&lang=sv&username=dg222cs", name);
            var webRequest = WebRequest.Create(urlString);
            
            using(var response = webRequest.GetResponse())
            using(var content = response.GetResponseStream())
            using (var reader = new StreamReader(content))
            {
                 searchObject = reader.ReadToEnd();
            }

            return JObject.Parse(searchObject)["geonames"].Select(l => new Location(l)).ToList().AsReadOnly();
        }
    }
}
