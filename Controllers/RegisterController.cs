using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class RegisterController : ControllerBase
    {
        public class RegisterData
        {
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string ShippingAddress { get; set; }
        }

        public class RegistrationResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public string UserName { get; set; }     
        }

        private readonly AppDbContext context;
        public RegisterController(AppDbContext context)
        {
            this.context = context;
        }

      
        [HttpPost]
        public async Task<RegistrationResponse> Post([FromBody] User user)
        {
            var resp = new RegistrationResponse { Success = false };
            try
            {
                if (!context.Users.Any(u => u.Email == user.Email))
                {
                    var emailValidator = new EmailAddressAttribute();

                    if (emailValidator.IsValid(user.Email) &&
                        !string.IsNullOrEmpty(user.Password) &&
                        !string.IsNullOrEmpty(user.UserName))
                       
                    {
                        user.Password = PasswordHash.HashPassword(user.Password);
                        await context.Users.AddAsync(user);
                        await context.SaveChangesAsync();
                        resp.UserName = user.UserName;
                        resp.Success = true;
                    }
                    else
                        resp.Message = "Strings cannot be empty";
                }
                else
                    resp.Message = "An account with this email already exists";
            }
            catch
            {
                resp.Message = "An internal error occurred. Please try again.";
            }

            return resp;
        }       
    }
}

