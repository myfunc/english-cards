using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EnglishCards.Contract.Api.Request;
using EnglishCards.Contract.Api.Response;
using EnglishCards.Contract.Api.Response.Data;
using EnglishCards.Model;
using EnglishCards.Model.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Ocsp;

namespace EnglishCards.Host.Controllers
{
    [Route("api")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private DataContext _context;
        public AccountController(DataContext dataContext)
        {
            _context = dataContext;
        }

        [HttpPost("registration")]
        public async Task<RegistrationResponse> Registration([FromBody] RegistrationRequest request)
        {
            User user = await _context.Users.FirstOrDefaultAsync(p => p.Login == request.Login || p.Email == request.Email);
            if (user == null)
            {
                var language = await _context.Languages.FirstOrDefaultAsync(p => p.Code == request.NativeLangCode);
                if (language == null)
                {
                    return new RegistrationResponse()
                    {
                        IsSuccess = false,
                        Error = new Error()
                        {
                            Code = 200,
                            Message = "Language code doesn't exist"
                        }

                    };
                }
                user = new User()
                {
                    Id = Guid.NewGuid(),
                    Login = request.Login,
                    Email = request.Email,
                    Password = request.Password,
                    NativeLanguage = language
                };

                var userInGroup = new UserInGroup()
                {
                    UserId = user.Id,
                    GroupId = Constants.Groups.User
                };

                _context.Users.Add(user);
                _context.UserInGroups.Add(userInGroup);
                await _context.SaveChangesAsync();

                return new RegistrationResponse();
            }
            return new RegistrationResponse()
            {
                IsSuccess = false,
                Error = new Error()
                {
                    Code = 200,
                    Message = "User already exists"
                }
            };
        }

        [HttpPost("login")]
        public async Task<LoginResponse> Login([FromBody] LoginRequest request)
        {
            User user = await _context.Users.FirstOrDefaultAsync(p => p.Login == request.Login && p.Password == request.Password);
            if (user != null)
            {
                await Authenticate(user);

                return new LoginResponse();
            }
            return new LoginResponse()
            {
                IsSuccess = false,
                Error = new Error()
                {
                    Code = 200,
                    Message = "Invalid credentials"
                }
            };
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString())
            };
            ClaimsIdentity id = new ClaimsIdentity(
                claims, 
                "ApplicationCookie", 
                ClaimsIdentity.DefaultNameClaimType, 
                ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}