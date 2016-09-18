using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VehicleBuyScannerLib.Structure
{
    abstract public class VehicleSource
    {
        protected string UrlAddress;
        protected string XPath;
        protected bool XPathLeadToListRootElement;

        public VehicleSource(string urlAddress, string xpath, bool xpathLeadToListRootElement = false)
        {
            UrlAddress = urlAddress;
            XPath = xpath;
            XPathLeadToListRootElement = xpathLeadToListRootElement;
        }
        public virtual async Task<List<Vehicle>> GetVehicles()
        {
            return await ProcessSource();
        }

        protected virtual async Task<List<Vehicle>> ProcessSource()
        {
            var siteSource = await RetrievePage();

            var vehicles = new List<Vehicle>();
            var elementsNodes = XPathLeadToListRootElement ? siteSource.DocumentNode.SelectSingleNode(XPath).ChildNodes : siteSource.DocumentNode.SelectNodes(XPath);
            var i = 0;
            foreach (var article in elementsNodes)
            {
                vehicles.Add(FetchVehicle(article));
                i++;
            }

            return vehicles;
        }

        protected virtual async Task<HtmlDocument> RetrievePage()
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

        protected virtual Vehicle FetchVehicle(HtmlNode sourceElement) { return new Vehicle(); }
    }
}
