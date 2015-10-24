using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ShopingCartDemo.Models;
namespace ShopingCartDemo.Controllers
{
    public class HomeController : Controller
    {
        DemoContext dbObj = new DemoContext();
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("login");
            }
            else
            {
                string getName = Session["username"].ToString();
                ViewBag.userName = getName;
                int count = dbObj.Cart.Where(s => s.UserName == getName).Count();
                ViewBag.cartCount = count;
                return View("Index", getAllItems());
            }

        }

        [HttpGet]
        public ActionResult Login()
        {
            Session["username"] = null;
            return View("login");
        }

        [HttpPost]
        public ActionResult Login(Login objLogin)
        {
            var getValidUser = dbObj.Login.FirstOrDefault((p) => p.UserName == objLogin.UserName && p.Password == objLogin.Password);
            if (getValidUser != null)
            {
                Session["username"] = objLogin.UserName;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.LoginFaild = "Invalid username or password please retry...";
                return View("login");
            }
        }

        public IList<Item> getAllItems()
        {
            IList<Item> items = new List<Item>();
            items = dbObj.Item.ToList();
            return items;
        }

        public int AddCart(int ItemId)
        {
            string username = Session["username"].ToString();
            Cart objcart = new Cart()
            {
                UserName = username,
                ItemId = ItemId
            };
            dbObj.Cart.Add(objcart);
            dbObj.SaveChanges();
            int count = dbObj.Cart.Where(s => s.UserName == username).Count();
            return count;
        }


        public PartialViewResult GetCartItems()
        {
            string username = Session["username"].ToString();
            var sum = 0;
            var GetItemsId = dbObj.Cart.Where(s => s.UserName == username).Select(u => u.ItemId).ToList();
            var GetCartItem = from itemList in dbObj.Item where GetItemsId.Contains(itemList.Id) select itemList;
            foreach (var totalsum in GetCartItem)
            {
                sum = sum + totalsum.Price;
                sum = sum - ((totalsum.Price * totalsum.Offer) / 100);
            }
            ViewBag.Total = sum;
            return PartialView("_cartItem", GetCartItem);

        }

        public PartialViewResult DeleteCart(int itemId)
        {
            string getName = Session["username"].ToString();
            Cart removeCart = dbObj.Cart.FirstOrDefault(s => s.UserName == getName && s.ItemId == itemId);
            dbObj.Cart.Remove(removeCart);
            dbObj.SaveChanges();
            return GetCartItems();
        }
    }
}