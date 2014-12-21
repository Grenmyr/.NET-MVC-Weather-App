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
            XDocument xmlResponse;
            var urlString = String.Format("http://api.yr.no/weatherapi/locationforecast/1.9/?lat={0};lon={1}", location.Lat, location.Lng);
            //var urlstring = "http://api.yr.no/weatherapi/locationforecast/1.9/?lat=56;lon=16";
            var webRequest = WebRequest.Create(urlString);

            using (var response = webRequest.GetResponse())
            using (var content = response.GetResponseStream())
            using (var reader = new StreamReader(content))
            {
                xmlResponse = XDocument.Load(content);
            }


            var alltimes = xmlResponse.Descendants("time");
            var currentWeather = alltimes.Take(2);

            var superlist = new List<Forecast>();

            superlist.Add(new Forecast()
            {
                LocationId = location.Id,               
                Temperature = currentWeather.Descendants("temperature").First().Attribute("value").Value,
                SymbolId = int.Parse(currentWeather.Descendants("symbol").First().Attribute("number").Value)
            });

            var symbolId = xmlResponse.Descendants("time").Skip(2).Where(d => d.Attribute("to").Value.Contains("12:00")
                               && d.Attribute("from").Value.Contains("06:00")
                               ).Select(n => n.Descendants("symbol").First().Attribute("number").Value)
                               .Take(4).ToArray();

            var temperature = xmlResponse.Descendants("time").Skip(2).Where(d => d.Attribute("to").Value.Contains("12:00")
                               && d.Attribute("from").Value.Contains("12:00")
                               ).Select(n => n.Descendants("temperature").First().Attribute("value").Value)
                               .Take(4).ToArray();

            for (int i = 0; i < 4; i++)
            {
                superlist.Add(new Forecast()
                {
                    LocationId = location.Id,
                    Temperature = temperature[i].ToString(),
                    SymbolId = int.Parse(symbolId[i])
                });
            }



            //var list = (from weather in xmlResponse.Descendants("time").Skip(2).Where(d => d.Attribute("to").Value.Contains("12:00")
            //                  && d.Attribute("from").Value.Contains("12:00")).Select(n => n.Descendants("temperature")).Take(4) 

            //        select new Forecast
            //        { 
            //            Temperature = weather.First().Attribute("value").Value.ToString()
            //        });


            return superlist;

            //foreach (var item in list)
            //{
            //    superlist.Add(new Forecast()
            //    {
            //        Temperature = item.FirstOrDefault().Attribute("value").Value.ToString()
            //    });
            //}

            //var secondNode =  xmlResponse.Descendants("time").Skip(2+i).Where(d => d.Attribute("to").Value.Contains("12:00")
            //                && d.Attribute("from").Value.Contains("12:00"))

            //return superlist;




            //var forecast3 = xmlResponse.Descendants("time").Skip(2).Where(d => d.Attribute("to").Value.Contains("12:00")
            //                    && d.Attribute("from").Value.Contains("12:00"))
            //          .SelectMany(n => new[] { 
            //            n.Descendants("temperature").First().Attribute("value").Value, 
            //            n.Descendants("cloudiness").First().Attribute("percent").Value,
            //            }                        
            //              ).Take(12);







            //var forecast1 = xmlResponse.Descendants("time").Skip(2).Where(d => d.Attribute("to").Value.Contains("12:00")
            //                       && d.Attribute("from").Value.Contains("12:00"))
            //              .SelectMany(n => new[] { n, n.NextNode }).Take(8).ToList();



            // return superlist;
        }
    }
}
