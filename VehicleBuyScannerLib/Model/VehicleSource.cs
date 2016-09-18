using HtmlAgilityPack;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace VehicleBuyScannerLib.Model
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
            //Add here multipages logic part {

            var siteSource = await RetrievePage();
            
            var vehicles = new List<Vehicle>();
            var elementsNodes = XPathLeadToListRootElement ? siteSource.DocumentNode.SelectSingleNode(XPath).ChildNodes : siteSource.DocumentNode.SelectNodes(XPath);
            
            foreach (var article in elementsNodes)
            {
                vehicles.Add(FetchVehicle(article));            
            }

            //}

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

        //For now only first page will be read. Why? Because it should be enough.  
        //If you would have 60+ results from EVERY portal than I think you should better think what you want...
        //protected abstract int NumberOfPagesToRead();
    }
}
