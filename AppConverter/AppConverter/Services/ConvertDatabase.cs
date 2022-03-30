using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

using AppConverter.Models;
using System.Threading.Tasks;

namespace AppConverter.Services
{
    public class ConvertDatabase
    {
        readonly SQLiteAsyncConnection _database;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbPath">path database</param>
        public ConvertDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Conversion>().Wait();
            _database.CreateTableAsync<ConversionOperation>().Wait();
            _database.CreateTableAsync<VerificationDatabase>().Wait();
        }

        /// <summary>
        /// List Table Conversion
        /// </summary>
        /// <returns>list</returns>
        public Task<List<Conversion>> GetConversionAsync()
        {
            return _database.Table<Conversion>().ToListAsync();
        }

        /// <summary>
        /// List Table ConversionOperation 
        /// </summary>
        /// <returns>list</returns>
        public Task<List<ConversionOperation>> GetConversionOperationAsync()
        {
            return _database.Table<ConversionOperation>().ToListAsync();
        }

        /// <summary>
        /// List Table VerificationDatabase
        /// </summary>
        /// <returns>list</returns>
        public Task<List<VerificationDatabase>> GetVerificationDatabaseAsync()
        {
            return _database.Table<VerificationDatabase>().ToListAsync();
        }

        /// <summary>
        /// Variable Conversion with Id
        /// </summary>
        /// <param name="id">Id Table Conversion</param>
        /// <returns>Variable Conversion</returns>
        public Task<Conversion> GetConversionAsync(int id)
        {
            return _database.Table<Conversion>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Variable ConversionOperation with Id
        /// </summary>
        /// <param name="id">Id Table ConversionOperation</param>
        /// <returns>Variable ConversionOperation</returns>
        public Task<ConversionOperation> GetConversionOperationAsync(int id)
        {
            return _database.Table<ConversionOperation>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Variable CVerificationDatabase with Id
        /// </summary>
        /// <param name="id">Id Table VerificationDatabase</param>
        /// <returns>Variable VerificationDatabase</returns>
        public Task<VerificationDatabase> GetVerificationDatabaseAsync(int id)
        {
            return _database.Table<VerificationDatabase>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// List Table ConversionOperation with TitleConversion
        /// </summary>
        /// <param name="title">TitleConversion Table ConversionOperation</param>
        /// <returns>List ConversionOperation with title</returns>
        public Task<List<ConversionOperation>> GetConversionTitleOperationAsync(string title)
        {
            return _database.Table<ConversionOperation>()
                .Where(i => i.TitleConversion == title)
                .ToListAsync();
        }

        /// <summary>
        /// Varibale Table ConversionOperation with TitleConversion
        /// </summary>
        /// <param name="name">NameOperation Table ConversionOperation</param>
        /// <returns>Variable ConversionOperation with name</returns>
        public Task<ConversionOperation> GetConversionOperationAsync(string name)
        {
            return _database.Table<ConversionOperation>()
                .Where(i => i.NameOperation == name)
                .FirstOrDefaultAsync();
        }
        
        /// <summary>
        /// Search Table Conversion with Title
        /// </summary>
        /// <param name="titleConvert">Title Table Conversion</param>
        /// <returns>List Variable in Table Conversion</returns>
        public Task<List<Conversion>> SearchConvertionAsync(string titleConvert)
        {
            string searchNoSpaces = titleConvert.Replace(" ", "%");
            var get_docnumb = _database.QueryAsync<Conversion>("SELECT * FROM Conversion WHERE Title LIKE ?", "%" + searchNoSpaces + "%");

            return get_docnumb;
        }

        /// <summary>
        /// Search Table ConversionOperation with TitleConversion
        /// </summary>
        /// <param name="titleConvert">TitleConversion Table ConversionOperation</param>
        /// <returns>List Variable in Table ConversionOperation</returns>
        public Task<List<ConversionOperation>> SearchConvertionOperationAsync(string titleConvert)
        {
            string searchNoSpaces = titleConvert.Replace(" ", "%");
            var get_docnumb = _database.QueryAsync<ConversionOperation>("SELECT * FROM ConvertionOperation WHERE TitleConversion LIKE ?", "%" + searchNoSpaces + "%");

            return get_docnumb;
        }

        /// <summary>
        /// update or insert variable variable conversion in Table Conversion
        /// </summary>
        /// <param name="convert">Variable Conversion</param>
        /// <returns>Update or Insert</returns>
        public Task<int> SaveConversionAsync(Conversion convert)
        {
            if (convert.Id != 0)
                return _database.UpdateAsync(convert);
            else
            {
                return _database.InsertAsync(convert);
            }
        }

        /// <summary>
        /// Update or Insert variable ConversionOperation in table ConversionOperation
        /// </summary>
        /// <param name="conversionOperation">Variable ConversionOperation</param>
        /// <returns>Update or Insert</returns>
        public Task<int> SaveConversionOperationAsync(ConversionOperation conversionOperation)
        {
            if (conversionOperation.Id != 0)
                return _database.UpdateAsync(conversionOperation);
            else
            {
                return _database.InsertAsync(conversionOperation);
            }
        }

        /// <summary>
        /// Update or Insert Variable VerificationDatabase in table VerificationDatabase
        /// </summary>
        /// <param name="verif">Variable VerificationDatabase</param>
        /// <returns>Update or Insert</returns>
        public Task<int> SaveVerificationDatabaseAsync(VerificationDatabase verif)
        {
            if (verif.Id != 0)
                return _database.UpdateAsync(verif);
            else
            {
                return _database.InsertAsync(verif);
            }
        }

        /// <summary>
        /// Delete Variable Conversion
        /// </summary>
        /// <param name="convert">Varaiable Conversion</param>
        /// <returns></returns>
        public Task<int> DeleteConversionAsync(Conversion convert)
        {
            return _database.DeleteAsync(convert);
        }

        /// <summary>
        /// Delete Varibale ConversionOperation
        /// </summary>
        /// <param name="convert">Varable ConvertionOperation</param>
        /// <returns></returns>
        public Task<int> DeleteConversionOperationAsync(ConversionOperation convert)
        {
            return _database.DeleteAsync(convert);
        }

        /// <summary>
        /// Add Conversion in database
        /// </summary>
        /// <param name="title">Title Conversion</param>
        /// <param name="nbConvert">Number Conversion</param>
        /// <param name="date">Date</param>
        /// <param name="hexColor">Color Back</param>
        /// <param name="sourceImage">Image source</param>
        /// <param name="unit_abrg">String unit abrg</param>
        /// <param name="unit_name">strig unit name</param>
        /// <returns></returns>
        public async Task AddConversionOnDatabaseAsync(string title, int nbConvert, DateTime date, string hexColor, string sourceImage,
            string unit_abrg, string unit_name )
        {
            Conversion convert = new Conversion
            {
                Title = title,
                NbConvert = nbConvert,
                Date = date,
                HexColor = hexColor,
                SourceImage = sourceImage,
                Unit_ABRG = unit_abrg,
                Unit_Name = unit_name
            };
            await App.Database.SaveConversionAsync(convert);
            AddConversionOperationOnDatabaseAsync(nbConvert, unit_abrg, title);
        }

        /// <summary>
        /// Add Operation Conversion in table COnversionOperation
        /// </summary>
        /// <param name="nbConvert">Number of conversion</param>
        /// <param name="unit">string unit abrg</param>
        /// <param name="title">Title Conversion</param>
        public async void AddConversionOperationOnDatabaseAsync(int nbConvert, string unit, string title)
        {
            string[] listUnit = unit.Split(']');
            Operator ope = new Operator();
            for(int i=0; i<nbConvert; i++)
            {
                for(int j=0; j<nbConvert; j++)
                {
                    if(listUnit[j] != listUnit[i])
                    {
                        string symbol = ope.Operation(listUnit[i], listUnit[j])[0].Symbol;
                        double coefficient = ope.Operation(listUnit[i], listUnit[j])[0].Coeffient; 
                        ConversionOperation conversionOperation = new ConversionOperation
                        {
                            TitleConversion = title,
                            NameOperation = $"{listUnit[i]}-{listUnit[j]}",
                            SymbolOperation = symbol,
                            Formula = $"number {symbol} {coefficient}",
                            Coefficient = coefficient
                        };
                        if (symbol == "°c°f")
                            conversionOperation.Formula =  "(number * 9/5) + 32";
                        else if (symbol == "°f°c")
                            conversionOperation.Formula = "(number  - 32) * 5/9";
                        else if (symbol == "k°f")
                            conversionOperation.Formula = "(number  - 273.15) * 9/5) + 32";
                        else if (symbol == "°fk")
                            conversionOperation.Formula = "(number  - 32) * 5/9 + 273.15";
                        await App.Database.SaveConversionOperationAsync(conversionOperation);
                    }
                }
            }
        }

    }
}
