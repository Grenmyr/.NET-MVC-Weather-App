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
using System.Collections;

namespace Weather.Domain.Webservices
{
    public class YrWebservice
    {
        

        public IEnumerable<Forecast> GetForecasts(Location location)
        {
            const int FORECASTLENGTH = 4;

            XDocument xmlResponse;
            var urlString = String.Format("http://api.yr.no/weatherapi/locationforecast/1.9/?lat={0};lon={1}", location.Lat, location.Lng);
            var webRequest = WebRequest.Create(urlString);

            using (var response = webRequest.GetResponse())
            using (var content = response.GetResponseStream())
            using (var reader = new StreamReader(content))
            {
                xmlResponse = XDocument.Load(content);
            }


            var alltimes = xmlResponse.Descendants("time");
            var currentWeather = alltimes.Take(2);

            var forecastList = new List<Forecast>();

            forecastList.Add(new Forecast()
            {
                LocationId = location.Id,               
                Temperature = currentWeather.Descendants("temperature").First().Attribute("value").Value,
                SymbolId = int.Parse(currentWeather.Descendants("symbol").First().Attribute("number").Value),
                NederBird = currentWeather.Descendants("precipitation").First().Attribute("value").Value
            });

            var symbolId = xmlResponse.Descendants("time").Skip(2).Where(d => d.Attribute("to").Value.Contains("12:00")
                               && d.Attribute("from").Value.Contains("06:00")
                               ).Select(n => n.Descendants("symbol").First().Attribute("number").Value)
                               .Take(FORECASTLENGTH).ToArray();

            var nederBird = xmlResponse.Descendants("time").Skip(2).Where(d => d.Attribute("to").Value.Contains("12:00")
                             && d.Attribute("from").Value.Contains("06:00")
                             ).Select(n => n.Descendants("precipitation").First().Attribute("value").Value)
                             .Take(FORECASTLENGTH).ToArray();

            var temperature = xmlResponse.Descendants("time").Skip(2).Where(d => d.Attribute("to").Value.Contains("12:00")
                               && d.Attribute("from").Value.Contains("12:00")
                               ).Select(n => n.Descendants("temperature").First().Attribute("value").Value)
                               .Take(FORECASTLENGTH).ToArray();


            for (int i = 0; i < FORECASTLENGTH; i++)
            {
                    forecastList.Add(new Forecast()
                    {
                        NederBird = nederBird[i].ToString(),
                        LocationId = location.Id,
                        Temperature = temperature[i].ToString(),
                        SymbolId = int.Parse(symbolId[i])
                    });
               
            }

            return forecastList;
        }
    }
}
