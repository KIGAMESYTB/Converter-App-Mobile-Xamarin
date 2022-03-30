using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppConverter.Models
{
    /// <summary>
    /// Table Database Conversion
    /// </summary>
    public class Conversion
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Title { get; set; }

        public int NbConvert { get; set; }

        public DateTime Date { get; set; }

        public string HexColor { get; set; }

        public string SourceImage { get; set; }

        public string Unit_ABRG { get; set; }

        public string Unit_Name { get; set; }
    }
}
