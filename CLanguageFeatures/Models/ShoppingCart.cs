using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CLanguageFeatures.Models
{
    public class ShoppingCart : IEnumerable<Product>
    {
        public List<Product> products { get; set; }
        public IEnumerator<Product> GetEnumerator()
        {
            return products.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}