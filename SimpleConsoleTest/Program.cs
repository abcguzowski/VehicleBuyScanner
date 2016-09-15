using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleBuyScannerLib.Sources;

namespace SimpleConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new Test();
            test.TestIt();
        }
    }

    class Test{
        public void TestIt() {
            InitializeSources();
        }

        private async void InitializeSources()
        {
            var otoMoto = new Otomoto("http://otomoto.pl/motocykle-i-quady/kawasaki/vulcan/?search%5Bfilter_float_engine_capacity%3Afrom%5D=1500&search%5Bcountry%5D=");
            var otoMotoMatchedVehicles = await otoMoto.GetVehicles();

            var motoGratka = new Gratka("http://moto.gratka.pl/szukaj/16-1400-2-35000-1-5-1-c_1-pt-po-u-cd-r-kg-b1-sr.html");
            var motoGratkaMatchedVehicles = await motoGratka.GetVehicles();
        }
    }
}
