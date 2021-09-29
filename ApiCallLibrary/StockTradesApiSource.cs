using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ApiCallLibrary
{
    public class StockTradesApiSource
    {

        public static async Task<StockDataModel[]>  LoadStockData()
        {
            string url = "https://api.polygon.io/v3/reference/tickers?active=true&sort=ticker&order=asc&limit=10&apiKey=LbUK5oYKY_CbGwCziNnfRJikrQ0Uk5Dn";
            using(HttpResponseMessage response= await ApiHelper.ApiClient.GetAsync(url))
            {

                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        StockModel stockData = await response.Content.ReadAsAsync<StockModel>();
                       
                        return stockData.results;
                    }
                    else
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                return null;
                
            }
        }
    }
}
