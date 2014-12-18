﻿using System;
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
                //searchObject = xmlResponse.Root;

            }

            //var listOne = xmlResponse.Descendants("time").Select(d => d).Where(d => d.LastAttribute.ToString().Contains("15:00")).ToList();
            var listTwo = xmlResponse.Descendants("time")
                .Select(d => d)
                .Where(d => d.Attribute("to").Value
                .Contains("15:00")).ToList();

            var listThree = xmlResponse.Descendants("time").Select(d => d).ToList();
            //var list = xmlResponse.Descendants("time").First();

            //var items = (from forecast in xmlResponse.Descendants("time")
            //             select new Forecast
            //             {
            //                 Temperature = int.Parse(forecast.Element("temperature").Value),
            //                 SymbolId = forecast.Value
            //             });
            

            //TODO XML Istället för Json
            return new Forecast();
        }
    }
}
