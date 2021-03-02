using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceNet.Models
{
    public class Comments 
    {        
        [Key]
        public int CommentId { get; set; }
        
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product ProductName { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }
        public User UserName { get; set; }
        public int Rating { get; set; }
        public string Image { get; set; }
        public string Text { get; set; }
    }
}
