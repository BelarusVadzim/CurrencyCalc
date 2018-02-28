using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalc.Models
{
    class Currency
    {
        public string ShortName { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public Double Rate { get; set; }
    }
}
