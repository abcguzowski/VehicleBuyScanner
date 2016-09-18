using HtmlAgilityPack;
using VehicleBuyScannerLib.Model;

namespace VehicleBuyScannerLib.Sources
{
    public class Otomoto : VehicleSource
    {
        const string xpath = "//article";

        public Otomoto(string urlAddress) : base(urlAddress, xpath)
        {
        }

        protected override Vehicle FetchVehicle(HtmlNode sourceElement) {

            var imgNode = sourceElement.SelectSingleNode(".//div/a[@data-ad-id]");
            var locationNode = sourceElement.SelectSingleNode(".//span[@class='icon-location']").NextSibling.NextSibling.FirstChild;

            return new Vehicle(
                imgNode.Attributes["data-ad-id"].Value,
                imgNode.Attributes["href"].Value,
                imgNode.Attributes["style"].Value.Split('\'')[1],
                imgNode.Attributes["title"].Value,
                sourceElement.SelectSingleNode(".//h3[@class='offer-item__subtitle']").InnerHtml,
                sourceElement.SelectSingleNode(".//span[@class='offer-price__number']").FirstChild.InnerHtml.Trim(),
                sourceElement.SelectSingleNode(".//li[@class='offer-item__params-item']/span").InnerText.TrimEnd(),
                locationNode.NextSibling == null ? locationNode.OuterHtml.Trim() : locationNode.OuterHtml.Trim() + locationNode.NextSibling.InnerText.Trim(),
                sourceElement.SelectSingleNode(".//li[@data-code='engine_capacity']/span").InnerText.TrimEnd()
                );
        }
    }
}
