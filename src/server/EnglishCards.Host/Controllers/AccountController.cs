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
using EnglishCards.Model.Repositories;
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
        private AccountRepository _accountRepo;
        private SystemRepository _systemRepo;
        public AccountController(SystemRepository systemRepository, AccountRepository accountRepository)
        {
            _accountRepo = accountRepository;
            _systemRepo = systemRepository;
        }

        [HttpPost("registration")]
        public async Task<RegistrationResponse> Registration([FromBody] RegistrationRequest request)
        {
            if (!await _accountRepo.IsUserExists(request.Login, request.Email))
            {
                var nativeLang = await _systemRepo.GetLanguageByCode(request.NativeLangCode);
                var foreignLang = await _systemRepo.GetLanguageByCode(request.ForeignLangCode);
                if (nativeLang == null || foreignLang == null)
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
                var user = new User()
                {
                    Id = Guid.NewGuid(),
                    Login = request.Login,
                    Email = request.Email,
                    Password = request.Password,
                    NativeLanguage = nativeLang,
                    ForeignLanguage = foreignLang
                };

                _accountRepo.AddUser(user);
                _accountRepo.GrantUser(user.Id, Constants.Groups.User);

                await _accountRepo.SaveChanges();

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
            User user = await _accountRepo.GetUserByLoginPassword(request.Login, request.Password);
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