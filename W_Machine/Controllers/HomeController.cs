using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using W_Machine.Abstract;
using W_Machine.Models;

namespace W_Machine.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepository repository;

        public HomeController(IProductRepository productRepository)
        {
           this.repository = productRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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

        public ActionResult Wmach()
        {
            ViewBag.SumMoney = int.Parse("0");
            ViewBag.RestOfMoney = 0;
            DefaultViewBag();
            if (HttpContext.Request.Query["id"] == "28031989")
                return Redirect(Url.Action("Index", "Admin"));
            else return View(repository.Drinks);
        }
        [HttpPost]
        public ActionResult Wmach(string SumMoney, string clickonbutton, string clickbuttoncoin, string buttonrestofmoney)
        {
            ViewBag.Title = buttonrestofmoney;
           
            int iSumInController = 0;
            int.TryParse(SumMoney, out iSumInController);
            
            ViewBag.RestOfMoney = 0;
            ViewBag.SumMoney = int.Parse(SumMoney);
            DefaultViewBag();
            if (!string.IsNullOrEmpty(clickbuttoncoin)) SaveCoinInBase(clickbuttoncoin.Split(' ')[0]);
            if (!string.IsNullOrEmpty(clickonbutton))
            {
                Drink Drink = repository.Drinks.FirstOrDefault(d => d.Name == clickonbutton);
                Drink.iCount--;
                repository.SaveProduct(Drink);
                clickonbutton = "";
                clickbuttoncoin = "";
                ViewBag.RestOfMoney = iSumInController - (int)Drink.Price;

                ViewBag.SumMoney = ViewBag.RestOfMoney;
            }
            if (!string.IsNullOrEmpty(buttonrestofmoney))
            {
                foreach (KeyValuePair<int, int> item in CalculateChange((int.Parse(buttonrestofmoney.Split(' ')[0]))))
                {
                    switch (item.Key)
                    {
                        case 1:
                            SaveCoinInBase("One", -item.Value);
                            break;
                        case 2:
                            SaveCoinInBase("Two", -item.Value);
                            break;
                        case 5:
                            SaveCoinInBase("Five", -item.Value);
                            break;
                        case 10:
                            SaveCoinInBase("Ten", -item.Value);
                            break;
                    }
                }
                ViewBag.SumMoney = 0;
            }

            return View(repository.Drinks);
        }
        public void DefaultViewBag()
        {
            ViewBag.BDontOne = false;
            ViewBag.BDontTwo = false;
            ViewBag.BDontFive = false;
            ViewBag.BDontTen = false;
            foreach (var c in repository.Coins)
            {
                if ((c.SNameCoin == "One") & (c.BDontCoin)) ViewBag.BDontOne = true;

                if ((c.SNameCoin == "Two") & (c.BDontCoin)) ViewBag.BDontTwo = true;

                if ((c.SNameCoin == "Five") & (c.BDontCoin)) ViewBag.BDontFive = true;

                if ((c.SNameCoin == "Ten") & (c.BDontCoin)) ViewBag.BDontTen = true;
            }
        }
        private Dictionary<int, int> CalculateChange(int Money)
        {
            Dictionary<int, int> Dic = new Dictionary<int, int>();
            int[] FaceValues = { 10, 5, 2, 1 };
            foreach (int item in FaceValues)
            {
                if (Money / item == 0) continue;
                Dic.Add(item, Money / item);
                Money %= item;
                if (Money == 0) break;
            }
            return Dic;
        }
        private void SaveCoinInBase(string NameNumberCoin)
        {
            Coin coin = repository.Coins.FirstOrDefault(d => d.SNameNumberCoin == NameNumberCoin);
            coin.iCountCoin++;
            repository.SaveCoin(coin);
        }
        private void SaveCoinInBase(string NameCoin, int ValueCountCoin)
        {
            Coin coin = repository.Coins.FirstOrDefault(d => d.SNameCoin == NameCoin);
            coin.iCountCoin += ValueCountCoin;
            repository.SaveCoin(coin);
        }
        public FileContentResult GetImage(int productId)
        {
            Drink prod = repository.Drinks.FirstOrDefault(p => p.ProductID == productId);
            if (prod != null)
            {
                return File(prod.ImageData, prod.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}
