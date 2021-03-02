using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceNet.Code;
using ECommerceNet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext context;
        public LoginController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<LoginResponse> Post([FromBody] LoginData ld)
        {
            var resp = new LoginResponse { Success = false };

            try
            {
                var user = context.Users.FirstOrDefault(u => u.Email == ld.Email);

                if (user != null)
                {
                    if (PasswordHash.VerifyHashedPassword(user.Password, ld.Password))
                    {
                        await HttpContext.Session.LoadAsync();
                        HttpContext.Session.SetInt32("UserId", user.Id);
                        await HttpContext.Session.CommitAsync();

                        resp.Name = user.UserName;
                        resp.Success = true;
                    }
                }
            }
            catch { }

            return resp;
        }    


        public class LoginData
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
        public class LoginResponse
        {
            public string Name { get; set; }
            public bool Success { get; set; }
        }
    }
}