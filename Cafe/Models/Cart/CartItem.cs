using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cafe.DAL;

namespace Cafe.Models.Cart
{
    [Serializable]
    public class CartItem  
    {
        
        public int Id { get; set; }
   
        
        public string ProductName { get; set; }

        
        public decimal Price { get; set; }

        
        public int Quantity { get; set; }

        
        public decimal Amount
        {
            get
            {
                return this.Price * this.Quantity;
            }
        }

        public string ProductImage { get; set; }
    }

}

