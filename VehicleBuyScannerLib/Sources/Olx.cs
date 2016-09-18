using HtmlAgilityPack;
using VehicleBuyScannerLib.Model;

namespace VehicleBuyScannerLib.Sources
{
    public class Olx : VehicleSource
    {
        const string xpath = "//table[@summary='Ogłoszenie']";

        public Olx(string urlAddress) : base(urlAddress, xpath)
        {
        }

        protected override Vehicle FetchVehicle(HtmlNode sourceElement)
        {
            return new Vehicle(
                sourceElement.Attributes["data-id"].Value,
                sourceElement.SelectSingleNode(".//tbody/tr/td/a").Attributes["href"].Value,
                sourceElement.SelectSingleNode(".//tbody/tr/td/a/img").Attributes["src"].Value,
                sourceElement.SelectSingleNode(".//h3/a/strong").InnerHtml,
                "",
                sourceElement.SelectSingleNode(".//p[@class='price']/strong").InnerHtml,
                "",
                sourceElement.SelectSingleNode(".//p/small/span").InnerHtml.Trim(),
                ""
                );
        }
    }
}
