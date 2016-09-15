using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleBuyScanner.Structure
{
    abstract public class VehicleSource<T>
    {
        protected string UrlAddress;

        public VehicleSource(string urlAddress)
        {
            UrlAddress = urlAddress;
        }
        public virtual async Task<List<Vehicle>> GetVehicles()
        {
            return await ProcessSource();
        }

        protected abstract Task<List<Vehicle>> ProcessSource();

        protected abstract Vehicle FetchVehicle(T sourceElement);
    }
}
