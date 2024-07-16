using KrishnaDairyDotNetApi.Controller;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplication1New.Context;
using WebApplication1New.Entity;
using WebApplication1New.Models;

namespace WebApplication1New.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _userContext;
        private readonly IConfiguration _configuration;

        public UserController(UserContext userContext, IConfiguration configuration)
        {
            this._userContext = userContext;
            this._configuration = configuration;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult UserRegistration(UserRegisterationModel model)
        {
            try
            {
                var userExist = _userContext.Customertable.FirstOrDefault(x => x.Email == model.Email || x.Password == model.Password);
                if(userExist != null)
                {
                    return Ok(new { success = true, message = "You already have an account"});
                }
                if (model != null)
                {
                    UserEntity userTable = new UserEntity();
                    userTable.FirstName = model.FirstName;
                    userTable.PhoneNumber = model.PhoneNumber;
                    userTable.Email = model.Email;
                    userTable.Password = model.Password;

                    _userContext.Customertable.Add(userTable);
                    _userContext.SaveChanges();

                    if(userTable != null)
                    {
                        return Ok(new { success = true, message = "registered successfully", result = userTable });
                    }
                    else
                    {
                        return this.BadRequest(new { success = false, message = " failed to register", result = userTable });
                    }
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("Login")]
        public Dictionary<string, string> Login(LoginModel model)
        {
            JWTClass j = new JWTClass(_configuration);
            
            try
            {
                /*var userExist = _userContext.Customertable.FirstOrDefault(x => x.Email == model.email && x.Password == model.password);
                if (userExist != null)
                {
                    return this.Ok(new { success = true, message = "Login successful", result = userExist });
                }
                return null;*/

                 var credentials = _userContext.Customertable.FirstOrDefault(x => x.Email == model.email && x.Password == model.password);
                 if (credentials != null)
                 {
                     var token = j.GenerateJwtToken(credentials.Email, credentials.UserId);
                     var data = new Dictionary<string, string>
                     {
                         {"token", token },
                         {"userId", credentials.UserId.ToString()},
                         {"name", credentials.FirstName},
                         {"email", credentials.Email},
                         {"phonenumber", credentials.PhoneNumber},
                         {"role", credentials.Role},
                         {"UserPhoto", credentials.UserPhoto}
                     };
                     return data;
                 }
                 else
                 {
                     return null;
                 }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

       
    }
}
