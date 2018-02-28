using CurrencyCalc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalc.Controllers
{
    class Calculator
    {

        #region Properties
        public CurrencyList CurrencyList { get; set; }

        #endregion


        #region Constructor
        public Calculator()
        {
            CurrencyListLoader Loader = new CurrencyListLoader();
            CurrencyList = Loader.LoadCurrencyList();
        }
        #endregion


        #region Public methods
        
        public double Excange(int inputCurrencyId, int outCurrencyId, double inputCurrencyAmount)
        {
            Currency inputCurrency = null;
            Currency outCurrency = null;
            inputCurrency = GetCurrencyById(inputCurrencyId);
            outCurrency = GetCurrencyById(outCurrencyId);
            if (inputCurrency != null && outCurrency != null)
            {
                Double result = Math.Round(inputCurrencyAmount / inputCurrency.Rate * outCurrency.Rate, 4);
                return result;
            }
            else
            {
                throw new Exception("Wrong Currency Id");
            }

        }

        #endregion


        #region Private methods
        private Currency GetCurrencyById(int Id)
        {
            var curr = CurrencyList.Where(item => item.Id == Id);
            if (curr != null)
            {
                foreach (Currency item in curr)
                {
                    return item;
                }
            }
            return null;
        }

#endregion

    }
}
