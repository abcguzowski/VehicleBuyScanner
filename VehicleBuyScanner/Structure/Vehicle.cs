using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleBuyScanner.Structure
{
    public class Vehicle
    {
        public string Id { get; private set; }
        public string VehicleUrl { get; private set; }
        public string ImgUrl { get; private set; }
        public string Title { get; private set; }
        public string SubTitle { get; private set; }
        public string Price{ get; private set; }
        public string Year { get; private set; }
        public string Location { get; private set; }
        public string EngineCapacity { get; private set; }

        public Vehicle(string id, string url, string imgUrl, string title, string subtitle, string price, string year, string location, string engineCapacity) {
            Id = id;
            VehicleUrl = url;
            ImgUrl = imgUrl;
            Title = title;
            SubTitle = subtitle;
            Price = price;
            Year = year;
            Location = location;
            EngineCapacity = engineCapacity;
        }
    }
}
