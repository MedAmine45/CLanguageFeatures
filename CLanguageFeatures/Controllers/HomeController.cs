using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CLanguageFeatures.Models;

namespace CLanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public string Index()
        {
            return "Navigate to a URL to show an example ";
        }

        public ActionResult Result()
        {
            return View();
        }
        public ActionResult AutoProperty()
        {
            Product myProduct = new Product();
            myProduct.Name = "Kayak";//setter
            string productName = myProduct.Name;//getter

            return View("Result",
                (object) String.Format( "Product Name : {0}", productName));
        }

        public ViewResult CreateProduct()
        {
            Product myProduct = new Product
            {
                ProductID = 100,
                Name = "Kayak",
                Description = "A boat for one person",
                Price = 275M,
                Category = "Watersports"
            };
            return View("Result",
           (object)String.Format("Category : {0} , Product : {1}", 
                                            myProduct.Category, myProduct.Name));
        }

        public ViewResult CreateCollection()
        {
            string[] stringArray = { "apple","orange","plum"};
            List<int> intList = new List<int> { 10, 20, 30, 40 };
           // Collection<string> col = new Collection<string> { "x", "y" };
            Dictionary<string, int> myDict = new Dictionary<string, int>
            {
                {"apple",10 },
                {"orange",20 },
                {"plum",30 }
            };
            return View("Result", (object)intList[2].ToString());
        }

        public ViewResult UseExtension()
        {
            ShoppingCart cart = new ShoppingCart
            {
                products = new List<Product>
                {
                    new Product{Name="Kayak",Price=275M},
                    new Product{Name="Lifejacket",Price=48.95M},
                    new Product{Name="Soccer",Price=19.50M},
                    new Product{Name="Conrner flag",Price=34.95M}
                }
                
            };
            decimal cartTotal = cart.TotalPrices();
            return View("Result", (object)String.Format("Total: ${0}", cartTotal.ToString()));
        }

        public ViewResult UseExtensionEnumerable()
        {

            IEnumerable<Product> products = new ShoppingCart
            {
                products = new List<Product>
                {
                    new Product{Name="Kayak",Price=275M},
                    new Product{Name="Lifejacket",Price=48.95M},
                    new Product{Name="Soccer",Price=19.50M},
                    new Product{Name="Conrner flag",Price=34.95M}
                }
                };

            Product[] productArray =
            {
                new Product{Name="Kayak",Price=275M},
                    new Product{Name="Lifejacket",Price=48.95M},
                    new Product{Name="Soccer",Price=19.50M},
                    new Product{Name="Conrner flag",Price=34.95M}
            };
            decimal cartTotal = products.TotalPricesIE();
            decimal arrayTotal = productArray.TotalPricesIE();
            return View("Result", (object)String.Format("Cart Total: ${0}, Array Total: {1}", cartTotal.ToString(),arrayTotal));
        }
        public ViewResult UserFilterExtensionMethod()
        {
            var myVariable = new Product { Name = "Kayak", Category = "Watersports", Price = 275M };
            string name = myVariable.Name;
            var myanon = new
            {
                custno = 15,
                custname = "Aly",
                phone = "0124578787"
            };


            IEnumerable<Product> products = new ShoppingCart
            {
                products = new List<Product>
                {
                    new Product{Name="Kayak",Category="Watersports",Price=275M},
                    new Product{Name="Lifejacket",Category="Watersports",Price=48.95M},
                    new Product{Name="Soccer",Category="Soccer Ball",Price=19.50M},
                    new Product{Name="Conrner flag",Category="Corner flag",Price=34.95M}
                }
            };
            int count = products.Count();
            decimal total = 0;
            foreach (Product pro in products.FilterByCategory("Watersports"))
                total += pro.Price;
            return View("Result", (object)String.Format("Total: ${0}", total.ToString()));
        }

        public ViewResult CreateAnonArray()
        {
            var oddsAndEnds = new[]
            {
                new{ Name="MVC",Category="Pattern"},
                new{ Name="Hat",Category="Clothing"},
                new{ Name="Apple",Category="fruit"}
            };
            StringBuilder result = new StringBuilder();
            foreach(var item in oddsAndEnds)
            {
                result.Append(item.Name).Append(" ");
            }
            return View("Result", (object)result.ToString());
        }
        public ViewResult FindProducts()
        {
            Product[] products =
            {
                new Product{Name="Kayak",Category="Watersports",Price=275M},
                new Product{Name="Lifejacket",Category="Watersports",Price=45.95M},
                new Product{Name="Soccer Ball",Category="Soccer",Price=19.50M},
                new Product{Name="Conrner flag",Category="Soccer",Price=34.95M}
            };
            var foundProducts = from p in products
                                             orderby p.Price descending
                                            select new { p.Name, p.Price, p.Category };
            StringBuilder result = new StringBuilder();
            foreach(var p in foundProducts)
            {
                result.AppendFormat("Price : {0} ",p.Price);
            
            }
            return View("Result", (object)result.ToString());
        }

        public ViewResult FindProductsDot()
        {
            Product[] products =
            {
                new Product{Name="Kayak",Category="Watersports",Price=275M},
                new Product{Name="Lifejacket",Category="Watersports",Price=45.95M},
                new Product{Name="Soccer Ball",Category="Soccer",Price=19.50M},
                new Product{Name="Conrner flag",Category="Soccer",Price=34.95M}
            };
            var foundProducts = products.OrderByDescending(p => p.Price)
                                              .Take(3)
                                              .Select(p => new { p.Name, p.Price, p.Category });
            var sum1 = products.Sum(p => p.Price);
            products[2] = new Product { Name = "Stadium", Price = 79600M };
            decimal sum2 = products.Sum(p => p.Price);
            StringBuilder result = new StringBuilder();
            foreach (var p in foundProducts)
            {
                result.AppendFormat("Price : {0}  ", p.Price);

            }
            return View("Result", (object)("sum1 ="+sum1+" ,Sum2 = "+sum2).ToString());
        }
    }
}