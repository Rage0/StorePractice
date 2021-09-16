using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorePractice.Models
{
    public class Cart
    {
        private List<CartLine> _cartLines = new List<CartLine>();

        public virtual void AddItem(Product product)
        {
            CartLine haveList = _cartLines
                .Where(p => p.Product.ProductID == product.ProductID)
                .FirstOrDefault();

            if ( haveList == null)
            {
                _cartLines.Add(new CartLine() { Product = product });
            }
            else
            {
                haveList.Quantity += 1;
            }
        }

        public IEnumerable<CartLine> GetItem => _cartLines;

        public virtual void RemoveItem(Product product)
        {
            CartLine haveList = _cartLines.FirstOrDefault(p => p.Product.ProductID == product.ProductID);

            if (haveList != null)
            {
                if (haveList.Quantity <= 1)
                {
                    _cartLines.Remove(haveList);
                }
                else
                {
                    haveList.Quantity -= 1;
                }
            }
            
        }

        public virtual void Clear() => _cartLines.Clear();
    }
}
