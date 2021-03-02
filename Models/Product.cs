using System;


using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceNet.Models
{
    public class Product
    {      
        public int ProductId { get; set; }
        public string ProductName { get; set; }       
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public decimal ShippingCost { get; set; }
        public virtual ICollection<PurchaseHistory> PurchaseHistory { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
    }
}
