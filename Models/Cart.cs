using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceNet.Models
{
    public class Cart
    {
        public int CartId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product ProductName { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }
        public User UserName { get; set; }

        public int Quantity { get; set; }
    }
}
