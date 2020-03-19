using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CLanguageFeatures.Models
{
    public static class MyExtensionMethods
    {
       public static decimal TotalPrices(this ShoppingCart cartParam)
        {
            decimal total = 0;
            foreach(Product product in cartParam.products)
            {
                total += product.Price;
            }
            return total;
        }

        public static decimal TotalPricesIE(this IEnumerable<Product> productsEnum)
        {
            decimal total = 0;
            foreach(Product pro in productsEnum)
            {
                total += pro.Price;
            }
            return total;
        }

        public static IEnumerable<Product> FilterByCategory(this IEnumerable<Product> productsEnum,string categoryParam)
        {
            foreach (Product pro in productsEnum)
            {
                if(pro.Category == categoryParam)
                {
                    yield return pro;
                }
            }
        }
    }
}