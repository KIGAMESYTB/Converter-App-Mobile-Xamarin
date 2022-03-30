using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AppConverter.Models;

namespace AppConverter.Services
{
    public class DisplayListViewUnitConversion
    {
        #region Variable

        private Conversion GetConversion { get; set; }
        private List<ListViewUnitConversion> listViewUnits;
        private readonly ColorFrame GetColor;
        private readonly Random random;
        private Operator GetOperator { get; set; }

        private int ColorRandom = -1;
        private int colorRandom;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="conversion">Varaiable Conversion</param>
        public DisplayListViewUnitConversion(Conversion conversion)
        {
            random = new Random();
            GetColor = new ColorFrame();
            GetConversion = conversion;
            colorRandom = random.Next(GetColor.BackColor().Count);
            GetOperator = new Operator();
        }

        /// <summary>
        /// Get Data in list view to convert page
        /// </summary>
        /// <returns>List Data Table ConversionOperation</returns>
        public List<ListViewUnitConversion> GetDataListView()
        {
            listViewUnits = new List<ListViewUnitConversion>();
            string[] unit_name = GetConversion.Unit_Name.Split(']');
            string[] unit_abrg = GetConversion.Unit_ABRG.Split(']');

            Console.WriteLine(unit_name);
            for (int i=0; i<unit_name.Length; i++)
            {
                ListViewUnitConversion list = new ListViewUnitConversion
                {
                    Unit_ABRG = unit_abrg[i],
                    Unit_Name = unit_name[i],
                    Value = 0
                };
                while(colorRandom == ColorRandom)
                {
                    colorRandom = random.Next(GetColor.BackColor().Count);
                }
                list.BackColorHex = GetColor.BackColor()[colorRandom];
                ColorRandom = colorRandom;
                listViewUnits.Add(list);
            }

            return listViewUnits;
        }

        /// <summary>
        /// Display the result of the conversions
        /// </summary>
        /// <param name="listViewUnitConversion">List data in convert page</param>
        /// <param name="unit">string unit </param>
        /// <param name="value">Value SearchBar in Convert Page</param>
        /// <returns>List Data</returns>
        public async Task<List<ListViewUnitConversion>> CalCulConversionSearch(List<ListViewUnitConversion> listViewUnitConversion, string unit, string value)
        {
            foreach(var convert in listViewUnitConversion)
            {
                if(convert.Unit_ABRG == unit)
                {
                    convert.Value = double.Parse(value);
                }
                else
                {
                    ConversionOperation conversionOperation = await App.Database.GetConversionOperationAsync($"{unit}-{convert.Unit_ABRG}");
                    convert.Value = GetOperator.SymbolOperatorCalcul(conversionOperation.SymbolOperation, conversionOperation.Coefficient, double.Parse(value));
                }
            }
            return listViewUnitConversion;
        }


    }
}
