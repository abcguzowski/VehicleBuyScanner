using HtmlAgilityPack;
using VehicleBuyScannerLib.Model;

namespace VehicleBuyScannerLib.Sources
{
    public class Sprzedajemy : VehicleSource
    {
        const string xpath = "//li[contains(@id, 'offer-')]";

        public Sprzedajemy(string urlAddress) : base(urlAddress, xpath)
        {
        }

        protected override Vehicle FetchVehicle(HtmlNode sourceElement)
        {
            return new Vehicle(
                sourceElement.Attributes["id"].Value,
                "http://sprzedajemy.pl" + sourceElement.SelectSingleNode(".//li[@class='has photo']/a").Attributes["href"].Value,
                sourceElement.SelectSingleNode(".//a/span/img").Attributes["src"].Value,
                sourceElement.SelectSingleNode(".//h2/a").InnerHtml.Trim(),
                "",
                sourceElement.SelectSingleNode(".//div[@class='pricing']/span").InnerHtml,
                sourceElement.SelectSingleNode(".//div[@class='offer-list-item-footer']/p/span").InnerHtml.Split(':')[1].Trim(),
                sourceElement.SelectSingleNode(".//div[@class='address']/strong").InnerHtml,
                ""
                );
        }
    }
}
