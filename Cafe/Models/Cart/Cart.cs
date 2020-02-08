using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cafe.DAL;
using Cafe.Models;

namespace Cafe.Models.Cart
{
    [Serializable] 
    public class Cart : IEnumerable<CartItem>
    {
        
        public Cart()
        {
            this.cartItems = new List<CartItem>();
        }

        
        private List<CartItem> cartItems;

        
        public int Count
        {
            get
            {
                return this.cartItems.Count;
            }
        }

        
        public decimal TotalAmount
        {
            get
            {
                decimal totalAmount = 0;
                foreach (var cartItem in this.cartItems)
                {
                    totalAmount = totalAmount + cartItem.Amount;
                }
                return totalAmount;
            }
        }

        
        public bool AddProduct(int ProductId)
        {
            var findItem = this.cartItems
                            .Where(s => s.Id == ProductId)
                            .Select(s => s)
                            .FirstOrDefault();

            
            if (findItem == default(Models.Cart.CartItem))
            {   
                using (dbCafeEntities db = new dbCafeEntities())

                {
                    var product = (from s in db.Products
                                   where s.ProductId == ProductId
                                   select s).FirstOrDefault();
                    if (product != default(Product))
                    {
                        this.AddProduct(product);
                    }
                }
            }
            else
            {   //存在購物車內，則將商品數量累加
                findItem.Quantity += 1;
            }
            return true;
        }

        //新增一筆Product，使用Product物件
        private bool AddProduct(Product product)
        {
            //將Product轉為CartItem
            var cartItem = new Models.Cart.CartItem()
            {
                Id = product.ProductId,
                ProductName = product.ProductName,
                Price=product.Price,
                ProductImage = product.ProductImage,
                Quantity = 1
            };

            
            this.cartItems.Add(cartItem);
            return true;
        }

        
        public bool RemoveProduct(int ProductId)
        {
            var findItem = this.cartItems
                            .Where(s => s.Id == ProductId)
                            .Select(s => s)
                            .FirstOrDefault();

            
            if (findItem == default(Models.Cart.CartItem))
            {
                
            }
            else
            {   
                this.cartItems.Remove(findItem);
            }
            return true;
        }

       
        public bool ClearCart()
        {
            this.cartItems.Clear();
            return true;
        }

        
        public List<OrderDetail> ToOrderDetailList(int orderId)
        {
            var result = new List<OrderDetail>();
            foreach (var cartItem in this.cartItems)
            {
                result.Add(new OrderDetail()
                {
                    ProductName = cartItem.ProductName,
                    Price = cartItem.Price,
                    Quantity = cartItem.Quantity,
                    OrderId = orderId
                });
            }
            return result;
        }


        #region IEnumerator

        IEnumerator<CartItem> IEnumerable<CartItem>.GetEnumerator()
        {
            return this.cartItems.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.cartItems.GetEnumerator();
        }

        #endregion
    }
}


