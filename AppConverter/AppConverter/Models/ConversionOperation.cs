using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppConverter.Models
{
    /// <summary>
    /// Table Operation Calcul of Selected Conversion
    /// </summary>
    public class ConversionOperation
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string TitleConversion { get; set; }

        public string NameOperation { get; set; }

        public string SymbolOperation { get; set; }

        public double Coefficient { get; set; }

        public string Formula { get; set; }
    }
}
