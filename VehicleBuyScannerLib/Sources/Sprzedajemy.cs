using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VehicleBuyScannerLib.Structure;

namespace VehicleBuyScannerLib.Sources
{
    public class Sprzedajemy : VehicleSource
    {
        const string xpath = "//li[contains(@id, 'offer-')]";
        //const string idXPath 
        const string imgXPath = ".//a/span/img";
        const string titleXPath = ".//h2/a";
        const string urlXPath = ".//li[@class='has photo']/a";
        const string priceXPath = ".//div[@class='pricing']/span";
        const string yearXPath = ".//div[@class='offer-list-item-footer']/p/span";
        const string locationXPath = ".//div[@class='address']/strong";
        //const string engineCapacityXPath = ".//div[@class='pasek']/ul/li/span";

        public Sprzedajemy(string urlAddress) : base(urlAddress, xpath)
        {
        }

        protected override Vehicle FetchVehicle(HtmlNode sourceElement)
        {
            var idNode = sourceElement;
            var imgNode = sourceElement.SelectSingleNode(imgXPath);
            var urlNode = sourceElement.SelectSingleNode(urlXPath);
            var titleNode = sourceElement.SelectSingleNode(titleXPath);
            var priceNode = sourceElement.SelectSingleNode(priceXPath);
            var yearNode = sourceElement.SelectSingleNode(yearXPath);
            var locationNode = sourceElement.SelectSingleNode(locationXPath);            

            return new Vehicle(
                idNode.Attributes["id"].Value,
                "http://sprzedajemy.pl" + urlNode.Attributes["href"].Value,
                imgNode.Attributes["src"].Value,
                titleNode.InnerHtml.Trim(),
                "",
                priceNode.InnerHtml,
                yearNode.InnerHtml.Split(':')[1].Trim(),
                locationNode.InnerHtml,
                ""
                );
        }
    }
}
