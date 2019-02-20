using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreeGuanajuato.Areas.Identity.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CreeGuanajuato.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private UserManager<CreeGuanajuatoUser> _userManager;
        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }


        public AccountController(UserManager<CreeGuanajuatoUser> userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {            
            AccessTokenFormat = accessTokenFormat;
        }
    }
}