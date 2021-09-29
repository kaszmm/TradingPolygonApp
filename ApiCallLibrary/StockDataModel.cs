using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCallLibrary
{
    public  class StockDataModel
    {
        public string Ticker { get; set; }
        public string Name { get; set; }
        public string Market { get; set; }
        public DateTime Last_updated_utc { get; set; }
    }
}
