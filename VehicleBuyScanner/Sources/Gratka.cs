using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VehicleBuyScanner.Structure;

namespace VehicleBuyScanner.Sources
{
    class Gratka : VehicleSource<HtmlNode>
    {
        string xpath = ".//li[contains(@class, 'moderacja')]";
        string idXPath = ".//div[@class='schowaj']";
        string imgXPath = ".//div/a/img";
        string titleXPath = ".//div/h3/a";
        string urlXPath = ".//div/h3/a";
        string priceXPath = ".//div[@class='pasek']/strong";
        string yearXPath = ".//div[@class='pasek']/ul/li[@title='Rok produkcji']/span";
        string locationXPath = ".//p[@class='listaRegion']";
        string engineCapacityXPath = ".//div[@class='pasek']/ul/li/span";

        public Gratka(string urlAddress) : base(urlAddress)
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
        protected override Vehicle FetchVehicle(HtmlNode sourceElement)
        {
            var idNode = sourceElement.SelectSingleNode(idXPath);
            var imgNode = sourceElement.SelectSingleNode(imgXPath);
            var urlNode = sourceElement.SelectSingleNode(urlXPath);
            var titleNode = sourceElement.SelectSingleNode(titleXPath);
            var priceNode = sourceElement.SelectSingleNode(priceXPath).FirstChild;
            var yearNode = sourceElement.SelectSingleNode(yearXPath).NextSibling;
            var locationNode = sourceElement.SelectSingleNode(locationXPath).FirstChild;
            var engineCapacityNode = sourceElement.SelectSingleNode(engineCapacityXPath).NextSibling;

            return new Vehicle(
                idNode.Attributes["id"].Value,
                "http://moto.gratka.pl"+urlNode.Attributes["href"].Value,
                imgNode.Attributes["src"].Value,
                titleNode.Attributes["title"].Value,
                "",
                priceNode.OuterHtml.Trim(),
                yearNode.OuterHtml.Trim(),
                locationNode.OuterHtml.Trim().Split('\n')[0].Trim()+ locationNode.OuterHtml.Trim().Split('\n')[1].Trim(),
                engineCapacityNode.OuterHtml.Trim()
                );
        }
    }
}
