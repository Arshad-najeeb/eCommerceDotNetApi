using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceNet.Models
{
    public class PurchaseHistory
    {
        [Key]
        public int PurchaseId { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }        
        public DateTime Date { get; set; }
        public int Total { get; set; }
        
    }
}
