using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalc.Models
{
    class JCurrency
    {
        public int Cur_ID { get; set; }
        public int Cur_Code { get; set; }
        public string Cur_Abbreviation { get; set; }
        public string Cur_Name_Eng { get; set; }
        public double Cur_OfficialRate { get; set; }
        public int Cur_Scale { get; set; }
    }
}
