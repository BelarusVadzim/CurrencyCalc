using CurrencyCalc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Script.Serialization;

namespace CurrencyCalc.Controllers
{
    class WebDataReader
    {

     # region Properties
        public string CurrencyListUrl { get; set; }
        public string RatesListUrl { get; set; }
        public string CurrencyListFileUri { get; set; }
        public string RatesListFileUri { get; set; }
     #endregion


     #region Constructor
        public WebDataReader()
        {
            CurrencyListUrl = @"http://www.nbrb.by/API/ExRates/Currencies";
            RatesListUrl = @"http://www.nbrb.by/API/ExRates/Rates?Periodicity=0";
            CurrencyListFileUri = @"./currency.json";
            RatesListFileUri = @"./rates.json";

        }
     #endregion


     #region Public methods
        public List<JCurrency> LoadJCurrency()
        {
            List<JCurrency> jCurrencyList;
            List<JCurrency> CurrencyNamesList;

            jCurrencyList = LoadRatesFromUrl();
            if (jCurrencyList == null)
                jCurrencyList = LoadRatesFromFile();
            CurrencyNamesList = LoadCurrencyFromUrl();
            if (CurrencyNamesList == null)
                CurrencyNamesList = LoadCurrencyFromFile();
            for (int i = 0; i < jCurrencyList.Count; i++)
            {
                var a = CurrencyNamesList.Where(o => o.Cur_ID == jCurrencyList[i].Cur_ID);
                foreach (var item in a)
                {
                    jCurrencyList[i].Cur_Name_Eng = item.Cur_Name_Eng;
                }
            }
            return jCurrencyList;
        }
     #endregion


     #region Private methods

        private List<JCurrency> LoadRatesFromUrl()
        {
            try
            {
                using (var webClient = new System.Net.WebClient())
                {
                    webClient.Encoding = Encoding.UTF8;
                    var json = webClient.DownloadString(RatesListUrl);
                    if(!string.IsNullOrWhiteSpace(json))
                    {
                        System.IO.File.WriteAllText(RatesListFileUri, json, Encoding.UTF8);
                        return GetListOfRatesFromJSon(json);
                    }
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        private List<JCurrency> LoadRatesFromFile()
        {
            try
            {
                string json = System.IO.File.ReadAllText(RatesListFileUri);
                return GetListOfJCurrencyFromJSon(json);
            }
            catch
            {
                return null;
            }
        }

        private List<JCurrency> LoadCurrencyFromUrl()
        {
            try
            {
                using (var webClient = new System.Net.WebClient())
                {
                    webClient.Encoding = Encoding.UTF8;
                    var json = webClient.DownloadString(CurrencyListUrl);
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        System.IO.File.WriteAllText(CurrencyListFileUri, json, Encoding.UTF8);
                        return GetListOfJCurrencyFromJSon(json);
                    }
                    return null;
                }
            }
            catch
            {
                return null;
            }

        }

        private List<JCurrency> LoadCurrencyFromFile()
        {
            try
            {
                string json = System.IO.File.ReadAllText(CurrencyListFileUri, Encoding.UTF8);
                return GetListOfJCurrencyFromJSon(json);
            }
            catch
            {
                return null;
            }
        }

        private List<JCurrency> GetListOfRatesFromJSon(string json)
        {
            List<JCurrency> ListOfJCurrency = new JavaScriptSerializer().Deserialize<List<JCurrency>>(json);
            return ListOfJCurrency;
        }

        private List<JCurrency> GetListOfJCurrencyFromJSon(string json)
        {
            List<JCurrency> ListOfJCurrency = new JavaScriptSerializer().Deserialize<List<JCurrency>>(json);
            return ListOfJCurrency;
        }

     #endregion

    }
}
