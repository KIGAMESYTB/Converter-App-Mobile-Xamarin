using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AppConverter.Models;
using AppConverter.Services;

namespace AppConverter.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConvertPage : ContentPage
    {
        #region Variable

        private readonly Conversion Convert;
        private readonly DisplayListViewUnitConversion DisplayListViewUnit;
        private List<ListViewUnitConversion> listUnitMeasurement;
        private string[] TabUnitABRG;
        private int indexUnit;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="convert">Selected Item Convertion in Database</param>
        public ConvertPage(Conversion convert)
        {
            InitializeComponent();
            Convert = convert;
            indexUnit = 0;
            DisplayListViewUnit = new DisplayListViewUnitConversion(convert);
            ListUnitConversion();
        }

        /// <summary>
        /// Refresh Page
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Device.BeginInvokeOnMainThread(async () =>
            {
                Initialize();
                listUnitMeasurement = DisplayListViewUnit.GetDataListView();
                ListViewUnitMesure.ItemsSource = listUnitMeasurement;
                await Task.Delay(250);
                searchBarElement.Focus();
            });
        }

        /// <summary>
        /// Extract the units corresponding to the conversion tool
        /// </summary>
        private async void ListUnitConversion()
        {
            Conversion ConversionUnit = await App.Database.GetConversionAsync(Convert.Id);
            TabUnitABRG = ConversionUnit.Unit_ABRG.Split(']');
        }

        /// <summary>
        /// Initialize element
        /// </summary>
        private void Initialize()
        {
            lblNameConvert.Text = Convert.Title;
            btnUnit.Text = TabUnitABRG[0];
        }

        /// <summary>
        /// Perform actions if there is text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private  void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            SearchBar searchBar = (SearchBar)sender;

            if (string.IsNullOrEmpty(searchBar.Text) || searchBar.Text == "-")
            {
                stackElementConvert.IsVisible = false;
            }
            else
            {
                stack.VerticalOptions = LayoutOptions.FillAndExpand;
                stackElementConvert.IsVisible = true;
                RefreshData();
            }
        }

        /// <summary>
        /// Perform the calculations and refresh the list view
        /// </summary>
        private async void RefreshData()
        {
            base.OnAppearing();
            ListViewUnitMesure.ItemsSource = null;
            ListViewUnitMesure.ItemsSource = await DisplayListViewUnit.CalCulConversionSearch(listUnitMeasurement, btnUnit.Text, searchBarElement.Text);
        }

        /// <summary>
        /// Button Unit Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private  void BtnUnit_Clicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            indexUnit++;
            if (indexUnit > TabUnitABRG.Count()-1)
                indexUnit = 0;
            btn.Text = TabUnitABRG[indexUnit];
            if (!string.IsNullOrEmpty(searchBarElement.Text))
            {
                RefreshData();
            }
        }

        /// <summary>
        /// Button Mathematical Formula Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnFormules_Clicked(object sender, EventArgs e)
        {
            List<ConversionOperation> conversions = await App.Database.GetConversionTitleOperationAsync(Convert.Title);
            string text = null;
            foreach(var formula in conversions)
            {
                text += $"{formula.NameOperation} : {formula.Formula}\n";
            }
            await DisplayAlert("Formula", text, "ok");
        }

    }
}