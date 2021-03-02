using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceNet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public class ProductData
        {
            public string ProductName { get; set; }
            public string Description { get; set; }
            public decimal? Price { get; set; }
            public decimal ShippingCost { get; set; }
        }

        public class ProductResponse
        {
            public bool Success { get; set; }
            public string ProductName { get; set; }
            public string Message { get; set; }
        }

        private readonly AppDbContext context;
        public ProductController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await context.Product.ToListAsync();
        }

        [HttpPost]
        [Produces(typeof(Product))]

        public async Task<ProductResponse> Post([FromBody] Product product)
        {
            var resp = new ProductResponse { Success = false };
            
                try
                {
                if (!context.Product.Any(p => p.ProductName == product.ProductName))
                {
                    await context.Product.AddAsync(product);
                    await context.SaveChangesAsync();
                    resp.ProductName = product.ProductName;
                    resp.Success = true;
                }
                else
                {
                    resp.Message = "A product already exists";
                }
                }
            catch { }

            return resp;
        }
    }
}
