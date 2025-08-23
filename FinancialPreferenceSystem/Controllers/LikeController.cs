using Microsoft.AspNetCore.Mvc;
using FinancialPreferenceSystem.Services;
using FinancialPreferenceSystem.Models;
using FinancialPreferenceSystem.Models.ViewModels;

namespace FinancialPreferenceSystem.Controllers
{
    public class LikeController : Controller
    {
        private readonly LikeService _service;

        public LikeController(LikeService service)
        {
            _service = service;
        }

        // 查詢喜好清單
        [HttpGet]
        public IActionResult Index(string userId)
        {
            var list = _service.GetLikeList(userId);
            return View(list);
        }

        // 顯示新增表單
        [HttpGet]
        public IActionResult Add(string userId)
        {
            var user = _service.GetUserById(userId);
            ViewBag.Account = user.Account;
            ViewBag.UserId = userId;

            var products = _service.GetAllProducts();
            ViewBag.Products = products;

            return View();
        }

        // 新增 POST
        [HttpPost]
        [ActionName("Add")]  
        public IActionResult AddPost(string userId, int productId, int qty)
        {
            if (qty <= 0)
            {
                ModelState.AddModelError("", "購買數量必須大於 0");
                return RedirectToAction("Add", new { userId = userId });
            }

            var user = _service.GetUserById(userId);
            var account = user.Account;

            _service.AddLike(userId, productId, qty, account);

            return RedirectToAction("Index", new { userId = userId });
        }

        // 顯示修改表單
        [HttpGet]
        public IActionResult Edit(int sn)
        {
            var like = _service.GetLikeById(sn);
            if (like == null) return NotFound();

            ViewBag.Products = _service.GetAllProducts();
            return View(like);
        }

        // 修改 POST
        [HttpPost]
        public IActionResult Edit(LikeListViewModel model)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateLike(model);
                return RedirectToAction("Index", new { userId = model.UserID });
            }
            ViewBag.Products = _service.GetAllProducts();
            return View(model);
        }

        // 刪除
        [HttpPost]
        public IActionResult Delete(int sn, string userId)
        {
            _service.DeleteLike(sn);
            return RedirectToAction("Index", new { userId = userId });
        }

    }
}
