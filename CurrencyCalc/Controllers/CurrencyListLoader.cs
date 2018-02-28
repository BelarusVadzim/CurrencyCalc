using CurrencyCalc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalc.Controllers
{
    class CurrencyListLoader
    {

        #region Public methods

        public CurrencyList LoadCurrencyList()
        {
            CurrencyList curlist; ;
            curlist = LoadCurrecyListFromWeb();
            curlist.Add(new Currency() { Id = 0, Name = "Belarusian Ruble", Rate = 1, ShortName = "BYN" });
            curlist.Sort((x, y) => x.Name.CompareTo(y.Name));
            return curlist;
        }

        #endregion

        #region Private methods

        private CurrencyList LoadCurrecyListFromWeb()
        {
            CurrencyList CurrencyList = new CurrencyList();
            WebDataReader WDR = new WebDataReader();
            var jCurrencyList = WDR.LoadJCurrency();
            if(jCurrencyList != null)
            {
                CurrencyList = GetCurrencyListFromJCurrencyList(jCurrencyList);
            }
            else
            {
                throw new Exception("Can't find currency data.");
            }
            return CurrencyList;
        }

        private CurrencyList GetCurrencyListFromJCurrencyList(List<JCurrency> ListOfJCurrency)
        {
            CurrencyList curlist = new CurrencyList();
            foreach (var item in ListOfJCurrency)
            {
                curlist.Add(new Currency()
                {
                    Id = item.Cur_ID,
                    Name = item.Cur_Name_Eng,
                    Rate = item.Cur_OfficialRate / item.Cur_Scale,
                    ShortName = item.Cur_Abbreviation
                });
            }

            return curlist;
        }

        #endregion

    }
}
