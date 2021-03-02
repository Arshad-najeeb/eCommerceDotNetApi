using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceNet.Models
{

    public class User
    {
       
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string ShippingAddress { get; set; }

        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<PurchaseHistory> PurchaseHistory { get; set; }

    }
}
