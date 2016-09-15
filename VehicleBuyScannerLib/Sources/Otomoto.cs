using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using VehicleBuyScannerLib.Structure;

namespace VehicleBuyScannerLib.Sources
{
    public class Otomoto : VehicleSource<HtmlNode>
    {
        string xpath = ".//article";
        string imgXPath = ".//div/a[@data-ad-id]";
        string subtitleXPath = ".//h3[@class='offer-item__subtitle']";
        string priceXPath = ".//span[@class='offer-price__number']";
        string yearXPath = ".//ul[@class='offer-item__params']";
        string locationXPath = ".//span[@class='icon-location']";
        string engineCapacityXPath = ".//ul[@class='offer-item__params']";

        public Otomoto(string urlAddress) : base(urlAddress)
        {
        }

        protected override async Task<List<Vehicle>> ProcessSource()
        {
            var siteSource = await RetrievePage();

            var vehicles = new List<Vehicle>();
            foreach (var article in siteSource.DocumentNode.SelectNodes(xpath))
            {
                vehicles.Add(FetchVehicle(article));
            }

            return vehicles;
        }


        private async Task<HtmlDocument> RetrievePage()
        {
            var client = new HttpClient();

            var responseMessage = await client.GetAsync(UrlAddress);
            string result = await responseMessage.Content.ReadAsStringAsync();
            if (!responseMessage.IsSuccessStatusCode)
                throw new FileNotFoundException("Unable to retrieve document");

            var siteSource = new HtmlAgilityPack.HtmlDocument();
            siteSource.LoadHtml(result);
            return siteSource;
        }

        protected override Vehicle FetchVehicle(HtmlNode article) {
            var imgNode = article.SelectSingleNode(imgXPath);
            var subtitleNode = article.SelectSingleNode(subtitleXPath).FirstChild;
            var priceNode = article.SelectSingleNode(priceXPath).FirstChild;
            var yearNode = article.SelectSingleNode(yearXPath).ChildNodes[1].ChildNodes[1].FirstChild;
            var locationNode = article.SelectSingleNode(locationXPath).NextSibling.NextSibling.FirstChild;
            var engineCapacityNode = article.SelectSingleNode(engineCapacityXPath).ChildNodes[5].ChildNodes[1].FirstChild;

            return new Vehicle(
                imgNode.Attributes["data-ad-id"].Value,
                imgNode.Attributes["href"].Value,
                imgNode.Attributes["style"].Value.Split('\'')[1],
                imgNode.Attributes["title"].Value,
                subtitleNode.OuterHtml,
                priceNode.OuterHtml.TrimEnd(),
                yearNode.OuterHtml.TrimEnd(),
                locationNode.NextSibling == null ? locationNode.OuterHtml.Trim() : locationNode.OuterHtml.Trim() + locationNode.NextSibling.FirstChild.OuterHtml.Trim(),
                engineCapacityNode.OuterHtml.TrimEnd()
                );
        }
    }
}
