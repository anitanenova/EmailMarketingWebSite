namespace BusinessManagement.WebSite.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Models.Demo;

    public class DemoController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            var demonstrations = new List<Demonstration>();
            demonstrations.Add(new Demonstration
            {
                Calculation = 557,
                Name = "Демонстрация 1",
                Price = (decimal)444.5,
                Currency = "$"
            });
            demonstrations.Add(new Demonstration
            {
                Calculation = 557,
                Name = "Демонстрация 2",
                Price = (decimal)166.9,
                Currency = "EUR"
            });
            demonstrations.Add(new Demonstration
            {
                Calculation = 5257,
                Name = "Демонстрация 3",
                Price = (decimal)1564.89,
                Currency = "LV"
            });
            demonstrations.Add(new Demonstration
            {
                Calculation = 52357,
                Name = "Демонстрация 4",
                Price = (decimal)144.89,
                Currency = "LV"
            });
            demonstrations.Add(new Demonstration
            {
                Calculation = 52357,
                Name = "Демонстрация 5",
                Price = (decimal)14.89,
                Currency = "LV"
            });
            demonstrations.Add(new Demonstration
            {
                Calculation = 52357,
                Name = "Демонстрация 6",
                Price = (decimal)140.89,
                Currency = "LV"
            });
            demonstrations.Add(new Demonstration
            {
                Calculation = 52357,
                Name = "Демонстрация 7",
                Price = (decimal)1.89,
                Currency = "LV"
            });
            demonstrations.Add(new Demonstration
            {
                Calculation = 52357,
                Name = "Демонстрация 8",
                Price = (decimal)1.89,
                Currency = "LV"
            });


            var model = new DemonstrationViewModel
            {
                 Demonstrations = demonstrations
            };

            return View("Demonstration", model);
        }
    }
}