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
using System.Xml.Linq;

namespace Weather.Domain.Webservices
{
    public class YrWebservice
    {

        public Forecast GetForecast(Location location)
        {
            XDocument xmlResponse;
            String searchObject;
            var urlString = String.Format("http://api.yr.no/weatherapi/locationforecast/1.9/?lat={0};lon={1}", location.Lng, location.Lat);

            var webRequest = WebRequest.Create(urlString);

            using (var response = webRequest.GetResponse())
            using (var content = response.GetResponseStream())
            using (var reader = new StreamReader(content))
            {
                xmlResponse = XDocument.Load(content);
                searchObject = xmlResponse.Root.ToString();

            }
            //TODO XML Istället för Json
            return new Forecast();
        }
    }
}
