using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cafe.Models.OrderModel
{
    public class Ship
    {
        
        [Required]
        [Display(Name = "Table Number")]
        [StringLength(15, ErrorMessage = "At Least 2 digit", MinimumLength = 1)]
        public string Table { get; set; }

       

    }
}