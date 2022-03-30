using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using AppConverter.Models;
using AppConverter.Views;

namespace AppConverter
{
    public partial class MainPage : ContentPage
    {
        #region Variable
        List<Conversion> listConvert;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            listConvert = new List<Conversion>();
        }

        /// <summary>
        /// Refresh Page
        /// </summary>
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            listConvert = await App.Database.GetConversionAsync();
            VerificationDatabase verif = await App.Database.GetVerificationDatabaseAsync(1);
            if(verif.CreateDatabase == false)
            {
                OnAlertYesNoClicked();
                verif.CreateDatabase = true;
                await App.Database.SaveVerificationDatabaseAsync(verif);
            }
        }

        /// <summary>
        /// Alert Welcome Application
        /// </summary>
        private async void OnAlertYesNoClicked()
        {
            bool answer = await DisplayAlert("Welcome", "Welcome to this new application created by @KIGAMES.\nThis application will allow you to convert according to a specific list.\nEnjoy discovering.", "Cancel", "It's understood");
            if(answer == true ||answer == false)
            {
                base.OnAppearing();
                ListViewConvert.ItemsSource = null;
                ListViewConvert.ItemsSource = await App.Database.GetConversionAsync();
            }
        }

        /// <summary>
        /// Selected item in list convert
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ListViewConvert_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new ConvertPage(listConvert[e.SelectedItemIndex]));
        }

        /// <summary>
        /// Search Text in List Convert
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchBar search = (SearchBar)sender;
            base.OnAppearing();
            Device.BeginInvokeOnMainThread(async () =>
            {
                listConvert = await App.Database.SearchConvertionAsync(search.Text);
                ListViewConvert.ItemsSource = listConvert;
            });
        }
    }
}
