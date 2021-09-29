using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApiCallLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ServerLogicLibrary;
using TradingPolygon.Models;

namespace TradingPolygon.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

      
        public IActionResult Index()
        {
            ApiHelper.InitializeClient();
            var stockData =StockTradesApiSource.LoadStockData();
            StockDataModel[] stockDataModel = stockData.Result;
           
            return View(stockDataModel);
        }

        public IActionResult RegisterUser()
        {
           
            return View();
        }

        public IActionResult AddUser(IFormCollection collection)
        {
            PersonModel person = new PersonModel
            {
                Name = collection["Name"].ToString(),
                Password = collection["Password"].ToString(),
                EmailAddress = collection["EmailAddress"].ToString(),
                PersonImage = collection["PersonImage"].ToString()

            };
            SqlCrud sqlCrud = new SqlCrud(GetConnectionString());
            sqlCrud.CreateNewPerson(person);
            return RedirectToAction("LoginUser","Home");
        }
        public IActionResult LoginUser()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        //Get Connection String
        private static string GetConnectionString(string connectionStringName = "Default")
        {
            string output = "";
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var config = builder.Build();
            output = config.GetConnectionString(connectionStringName);
            return output;
        }
    }
}
