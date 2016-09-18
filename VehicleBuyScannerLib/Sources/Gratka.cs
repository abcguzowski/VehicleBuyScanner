using HtmlAgilityPack;
using VehicleBuyScannerLib.Model;

namespace VehicleBuyScannerLib.Sources
{
    public class Gratka : VehicleSource
    {
        const string xpath = "//li[contains(@class, 'moderacja')]";

        public Gratka(string urlAddress) : base(urlAddress, xpath)
        {
        }
        
        protected override Vehicle FetchVehicle(HtmlNode sourceElement)
        {
            var locationNode = sourceElement.SelectSingleNode(".//p[@class='listaRegion']").FirstChild;

            return new Vehicle(
                sourceElement.SelectSingleNode(".//div[@class='schowaj']").Attributes["id"].Value,
                "http://moto.gratka.pl"+ sourceElement.SelectSingleNode(".//div/h3/a").Attributes["href"].Value,
                sourceElement.SelectSingleNode(".//div/a/img").Attributes["src"].Value,
                sourceElement.SelectSingleNode(".//div/h3/a").Attributes["title"].Value,
                "",
                sourceElement.SelectSingleNode(".//div[@class='pasek']/strong").InnerText.Trim(),
                sourceElement.SelectSingleNode(".//div[@class='pasek']/ul/li[@title='Rok produkcji']/span").NextSibling.OuterHtml.Trim(),
                locationNode.OuterHtml.Trim().Split('\n')[0].Trim()+ locationNode.OuterHtml.Trim().Split('\n')[1].Trim(),
                sourceElement.SelectSingleNode(".//div[@class='pasek']/ul/li/span").NextSibling.OuterHtml.Trim()
                );
        }
    }
}
