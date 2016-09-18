using HtmlAgilityPack;
using VehicleBuyScannerLib.Model;

namespace VehicleBuyScannerLib.Sources
{
    public class Gratka : VehicleSource
    {
        const string xpath = ".//li[contains(@class, 'moderacja')]";
        const string idXPath = ".//div[@class='schowaj']";
        const string imgXPath = ".//div/a/img";
        const string titleXPath = ".//div/h3/a";
        const string urlXPath = ".//div/h3/a";
        const string priceXPath = ".//div[@class='pasek']/strong";
        const string yearXPath = ".//div[@class='pasek']/ul/li[@title='Rok produkcji']/span";
        const string locationXPath = ".//p[@class='listaRegion']";
        const string engineCapacityXPath = ".//div[@class='pasek']/ul/li/span";

        public Gratka(string urlAddress) : base(urlAddress, xpath)
        {
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
