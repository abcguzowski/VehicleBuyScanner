using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VehicleBuyScannerLib.Sources;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeSources();
        }

        private async void InitializeSources()
        {
            var otoMoto = new Otomoto("http://otomoto.pl/motocykle-i-quady/kawasaki/vulcan/?search%5Bfilter_float_engine_capacity%3Afrom%5D=1500&search%5Bcountry%5D=");
            var otoMotoMatchedVehicles = await otoMoto.GetVehicles();

            var motoGratka = new Gratka("http://moto.gratka.pl/szukaj/16-1400-2-35000-1-5-1-c_1-pt-po-u-cd-r-kg-b1-sr.html");
            var motoGratkaMatchedVehicles = await motoGratka.GetVehicles();

            var sprzedajemy = new Sprzedajemy("https://sprzedajemy.pl/motoryzacja/motocykle-skutery-quady?inp_price%5Bfrom%5D=15000&inp_price%5Bto%5D=35000&inp_only_with_photos=1&inp_attribute_90%5Bfrom%5D=1500&inp_attribute_225=1130&inp_attribute_227=1136&sort=inp_srt_price_a&offset=0&items_per_page=60");
            var sprzedajemMatchedVehicles = await sprzedajemy.GetVehicles();
            
        }
    }
}
