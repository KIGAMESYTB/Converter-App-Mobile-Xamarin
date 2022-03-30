using System;
using System.Collections.Generic;
using System.Text;

namespace AppConverter.Services
{
    public class Operator
    {

        public List<VariableOperation> listDatabase = new List<VariableOperation>();

        /// <summary>
        /// Class Variable Operation
        /// </summary>
        public class VariableOperation
        {
            public String Symbol { get; set; }
            public double Coeffient { get; set; }
        }
        
        public List<string> ListDatabase(string symbol, string coefficient)
        {
            return new List<string>() { symbol, coefficient };
        }

        /// <summary>
        /// Calcul according to the symbols
        /// </summary>
        /// <param name="symbol">symbol</param>
        /// <param name="coefficient">coeeficient</param>
        /// <param name="value">value</param>
        /// <returns></returns>
        public double SymbolOperatorCalcul(string symbol, double coefficient, double value)
        {
            if (symbol == "/")
                return value / coefficient;
            else if (symbol == "*")
                return value * coefficient; 
            else if (symbol == "+")
                return value + coefficient;
            else if (symbol == "-")
                return value - coefficient;
            else if (symbol == "°c°f")
                return (value*1.8)+32;
            else if (symbol == "°f°c")
                return (value-32)*0.555555;
            else if (symbol == "k°f")
                return ((value-273.15)*1.8)+32;
            else if (symbol == "°fk")
                return ((value-32)*0.55555)+273.15;
            return 0;
        }

        public List<VariableOperation> Operation(string firstUnit, string secondUnit)
        {
            listDatabase.Clear();
            string formula = $"{firstUnit}-{secondUnit}";

            if (firstUnit == "m" || firstUnit == "km" || firstUnit == "ft" || firstUnit == "in")
                OperationDistance(formula);
            else if(firstUnit == "g" || firstUnit == "kg" || firstUnit == "lbs" || firstUnit == "stone")
                OperationWeight(formula);
            else if (firstUnit == "m/s" || firstUnit == "km/h" || firstUnit == "mph" || firstUnit == "nd")
                OperationSpeed(formula);
            else if (firstUnit == "€" || firstUnit == "$" || firstUnit == "£" || firstUnit == "¥" || firstUnit == "DZD")
                OperatorMoney(formula);
            else if (firstUnit == "°C" || firstUnit == "K" || firstUnit == "°F")
                OperatorTemp(formula);

            return listDatabase;

        }

        /// <summary>
        /// Calcul Operation Distance Convert
        /// </summary>
        /// <param name="formula">Two units</param>
        /// <returns>list</returns>
        public List<VariableOperation> OperationDistance(string formula)
        {
            switch (formula)
            {
                case "m-km":
                    listDatabase.Add(new VariableOperation { Symbol = "/", Coeffient = 1000 });
                    break;
                case "m-ft":
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 3.281 });
                    break;
                case "m-in":
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 39.37 });
                    break;
                case "km-m":
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 1000 });
                    break;
                case "km-ft":
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 3281 });
                    break;
                case "km-in":
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 39370 });
                    break;
                case "ft-m":
                    listDatabase.Add(new VariableOperation { Symbol = "/", Coeffient = 3.281 });
                    break;
                case "ft-km":
                    listDatabase.Add(new VariableOperation { Symbol = "/", Coeffient = 3281 });
                    break;
                case "ft-in":
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 12 });
                    break;
                case "in-m":
                    listDatabase.Add(new VariableOperation { Symbol = "/", Coeffient = 39.37 });
                    break;
                case "in-km":
                    listDatabase.Add(new VariableOperation { Symbol = "/", Coeffient = 39370 });
                    break;
                case "in-ft":
                    listDatabase.Add(new VariableOperation { Symbol = "/", Coeffient = 12 });
                    break;
                default:
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 1 });
                    break;
            }

            return listDatabase;
        }

        /// <summary>
        /// Calcul Operation Weight Convert
        /// </summary>
        /// <param name="formula">Two units</param>
        /// <returns>list</returns>
        public List<VariableOperation> OperationWeight(string formula)
        {
            switch (formula)
            {
                case "g-kg":
                    listDatabase.Add(new VariableOperation { Symbol = "/", Coeffient = 1000 });
                    break;
                case "g-lbs":
                    listDatabase.Add(new VariableOperation { Symbol = "/", Coeffient = 454 });
                    break;
                case "g-stone":
                    listDatabase.Add(new VariableOperation { Symbol = "/", Coeffient = 6350 });
                    break;
                case "kg-g":
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 1000 });
                    break;
                case "kg-lbs":
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 2.205 });
                    break;
                case "kg-stone":
                    listDatabase.Add(new VariableOperation { Symbol = "/", Coeffient = 6.35 });
                    break;
                case "lbs-g":
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 454 });
                    break;
                case "lbs-kg":
                    listDatabase.Add(new VariableOperation { Symbol = "/", Coeffient = 2.205 });
                    break;
                case "lbs-stone":
                    listDatabase.Add(new VariableOperation { Symbol = "/", Coeffient = 14 });
                    break;
                case "stone-g":
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 6350 });
                    break;
                case "stone-kg":
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 6.35 });
                    break;
                case "stone-lbs":
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 14 });
                    break;
                default:
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 1 });
                    break;
            }
            return listDatabase;
        }

        /// <summary>
        /// Calcul Operation Speed Convert
        /// </summary>
        /// <param name="formula">Two units</param>
        /// <returns>list</returns>
        public List<VariableOperation> OperationSpeed(string formula)
        {   
            switch (formula)
            {
                case "m/s-km/h":
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 3.6 });
                    break;
                case "m/s-mph":
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 2.237 });
                    break;
                case "m/s-nd":
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 1.944 });
                    break;
                case "km/h-m/s":
                    listDatabase.Add(new VariableOperation { Symbol = "/", Coeffient = 3.6 });
                    break;
                case "km/h-mph":
                    listDatabase.Add(new VariableOperation { Symbol = "/", Coeffient = 1.609 });
                    break;
                case "km/h-nd":
                    listDatabase.Add(new VariableOperation { Symbol = "/", Coeffient = 1.852 });
                    break;
                case "mph-m/s":
                    listDatabase.Add(new VariableOperation { Symbol = "/", Coeffient = 2.237 });
                    break;
                case "mph-km/h":
                    listDatabase.Add(new VariableOperation { Symbol = "/", Coeffient = 1.609 });
                    break;
                case "mph-nd":
                    listDatabase.Add(new VariableOperation { Symbol = "/", Coeffient = 1.151 });
                    break;
                case "nd-m/s":
                    listDatabase.Add(new VariableOperation { Symbol = "/", Coeffient = 1.944 });
                    break;
                case "nd-km/h":
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 1.852 });
                    break;
                case "nd-mph":
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 1.151 });
                    break;
                default:
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 1 });
                    break;
            }
            return listDatabase;
        }

        /// <summary>
        /// Calcul Operation Money Convert
        /// </summary>
        /// <param name="formula">Two units</param>
        /// <returns>list</returns>
        public List<VariableOperation> OperatorMoney(string formula)
        {
            switch (formula)
            {
                case "€-$":
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 1.1 });
                    break;
                case "€-£":
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 0.83 });
                    break;
                case "€-¥":
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 136.06 });
                    break;
                case "€-DZD":
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 156.99 });
                    break;
                case "$-€":
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 0.91 });
                    break;
                case "$-£":
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 0.76 });
                    break;
                case "$-¥":
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 123.95 });
                    break;
                case "$-DZD":
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 143.01 });
                    break;
                case "£-€":
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 1.2 });
                    break;
                case "£-$":
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 1.32 });
                    break;
                case "£-¥":
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 163.19 });
                    break;
                case "£-DZD":
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 188.14 });
                    break;
                case "¥-€":
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 0.0073 });
                    break;
                case "¥-$":
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 0.0081 });
                    break;
                case "¥-£":
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 0.0061 });
                    break;
                case "¥-DZD":
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 1.15 });
                    break;
                case "DZD-€":
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 0.0064 });
                    break;
                case "DZD-$":
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 0.007 });
                    break;
                case "DZD-£":
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 0.0053 });
                    break;
                case "DZD-¥":
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 0.87 });
                    break;
                default:
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 1 });
                    break;
            }
            return listDatabase;
        }

        /// <summary>
        /// Calcul Operation Temp Convert
        /// </summary>
        /// <param name="formula">Two units</param>
        /// <returns>list</returns>
        public List<VariableOperation> OperatorTemp(string formula)
        {
            switch (formula)
            {
                case "°C-K":
                    listDatabase.Add(new VariableOperation { Symbol = "+", Coeffient = 273.15 });
                    break;
                case "°C-°F":
                    listDatabase.Add(new VariableOperation { Symbol = "°c°f", Coeffient = 1 });
                    break;
                case "K-°C":
                    listDatabase.Add(new VariableOperation { Symbol = "-", Coeffient = 273.15 });
                    break;
                case "K-°F":
                    listDatabase.Add(new VariableOperation { Symbol = "k°f", Coeffient = 1 });
                    break;
                case "°F-°C":
                    listDatabase.Add(new VariableOperation { Symbol = "°f°c", Coeffient = 1 });
                    break;
                case "°F-K":
                    listDatabase.Add(new VariableOperation { Symbol = "°fk", Coeffient = 1 });
                    break;
                default:
                    listDatabase.Add(new VariableOperation { Symbol = "*", Coeffient = 1 });
                    break;
            }
            return listDatabase;
        }
    }
}
