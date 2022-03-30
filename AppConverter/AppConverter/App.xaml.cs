using AppConverter.Services;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

using AppConverter.Models;
using System.Threading.Tasks;

namespace AppConverter
{
    public partial class App : Application
    {
        public static ConvertDatabase Database { get; set; }
        public App()
        {
            InitializeComponent();
            DBInitialize();
            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.FromHex("333")
            };
        }

        /// <summary>
        /// If database is null -> Create and add convert in database
        /// </summary>
        private async void DBInitialize()
        {
            if (Database == null)
            {
                Database = new ConvertDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "level.db3"));
                List<Conversion> listConvert = await Database.GetConversionAsync();
                if (listConvert.Count <= 0)
                {
                    await Task.Run(async () =>
                    {
                        await App.Database.AddConversionOnDatabaseAsync("Distance Convert", 4, DateTime.Now, "#007afe", "DistanceConvert.png", "m]km]ft]in", "Meters]Kilometers]Feet]Inches");
                        await App.Database.AddConversionOnDatabaseAsync("Weight Convert", 4, DateTime.Now, "#28a745", "WeightConvert.png", "g]kg]lbs]stone", "Grams]Kilograms]Pounds]Stone");
                        await App.Database.AddConversionOnDatabaseAsync("Speed Convert", 4, DateTime.Now, "#dd3445", "SpeedConvert.png", "m/s]km/h]mph]nd", "Meters/second]Kilometer/hour]Mile/hour");
                        await App.Database.AddConversionOnDatabaseAsync("Money Convert", 5, DateTime.Now, "#fec106", "MoneyConvert.png", "€]$]£]¥]DZD", "Euros]Dollars Américain]Livres Sterling]Yen]Dinar Algérien");
                        await App.Database.AddConversionOnDatabaseAsync("Temp Convert", 3, DateTime.Now, Color.SkyBlue.ToHex(), "NumbersConvert.png", "°C]K]°F", "Degree Celsius]Degree Kelvin]Degree Fahrenheit"); ;
                        await App.Database.SaveVerificationDatabaseAsync(new VerificationDatabase { CreateDatabase = false });
                    });
                }
            }
        }

        protected override void OnStart()
        {
            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
