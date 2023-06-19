using System;
using System.IO;
using System.Linq;
//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using W_Machine.Abstract;
using W_Machine.Models;

namespace W_Machine.Controllers
{
    //[Authorize]
    public class AdminController : Controller
    {
        private IProductRepository repository;
        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index()
        {
            return View();
        }
        public ViewResult Drink()
        {
            return View(repository.Drinks);
        }
        public ViewResult Edit(int productId)
        {
            Drink drink = repository.Drinks
            .FirstOrDefault(p => p.ProductID == productId);
            return View(drink);
        }
        [HttpPost]
        public ActionResult Edit(Drink drink, IFormFile image)
        {

            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    drink.ImageMimeType = image.ContentType;
                    drink.ImageData = new byte[image.Length];
                    var resultInBytes = ConvertToBytes(image);
                    Array.Copy(resultInBytes, drink.ImageData, resultInBytes.Length);
                }
                repository.SaveProduct(drink);
                TempData["message"] = string.Format("{0} Сохранен", drink.Name);
                return RedirectToAction("Drink");
            }
            else
            {
                // there is something wrong with the data values
                return View(drink);
            }
        }
        private byte[] ConvertToBytes(IFormFile image)
        {
            using (var memoryStream = new MemoryStream())
            {
                image.OpenReadStream().CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
        [HttpPost]
        public ActionResult Delete(int productId)
        {
            Drink deletedProduct = repository.DeleteDrink(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("{0} удален", deletedProduct.Name);
            }
            return RedirectToAction("Drink");
        }
        public ViewResult Create()
        {
            return View("Edit", new Drink());
        }
        public ViewResult Coin()
        {
            return View(repository.Coins);
        }
        public ViewResult EditCoin(int CoinID)
        {
            Coin coin = repository.Coins
            .FirstOrDefault(p => p.CoinID == CoinID);
            return View(coin);
        }
        [HttpPost]
        public ActionResult EditCoin(Coin coin)
        {
            if (ModelState.IsValid)
            {
                repository.SaveCoin(coin);
                TempData["message"] = string.Format("{0} был сохранен", coin.SNameCoin);
                return RedirectToAction("Coin");
            }
            else
            {
                // there is something wrong with the data values
                return View(coin);
            }
        }
    }
}