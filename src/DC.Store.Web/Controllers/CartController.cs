using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DC.Store.Web.Helpers;
using DC.Store.Web.Models;
using DC.Store.Core.Services.Interfaces;
using static AutoMapper.Mapper;
using DC.Store.Core.Model;

namespace DC.Store.Web.Controllers
{
    public class CartController : Controller
    {
        #region Fields, Constructors
        private readonly IProductService _productService;

        public CartController(IProductService productService)
        {
            _productService = productService;
        }
        #endregion

        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
            return View();
        }
        
        public async Task<IActionResult> AddToCart(int? id, int qty = 1)
        {
            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                List<Item> cart = new List<Item>
                {
                    new Item { Product = Map<ProductDto, ProductViewModel>(await _productService.GetWithCategoryAsync(id)), Quantity = qty }
                };
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = ItemExists(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item { Product = Map<ProductDto, ProductViewModel>(await _productService.GetWithCategoryAsync(id)), Quantity = qty });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int? id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = ItemExists(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

            return RedirectToAction("Index");
        }

        private int ItemExists(int? id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            var product = cart.Find(i => i.Product.Id == id).Product;

            return product?.Id ?? -1;
        }
    }
}