using APIInsideExistingMVCProject.Models;
using APIInsideExistingMVCProject.Models.API;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web;
using APIInsideExistingMVCProject.Utils;

namespace APIInsideExistingMVCProject.Controllers
{

    public class API_AuthController : ApiController
    {
        
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public API_AuthController()
        {
           
        }

        public API_AuthController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }




        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }



        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }



        [System.Web.Http.HttpPost]
        [System.Web.Http.AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IHttpActionResult Login([FromBody] LoginModel loginModel)
        {

            var result =  SignInManager.PasswordSignIn(loginModel.UserName, loginModel.Password, false, shouldLockout: false);
            string tokenString = string.Empty;
            JWTTokenResponse response = new JWTTokenResponse();

            switch (result)
            {
                case SignInStatus.Success:
                    response = new JWTTokenResponse();
                    response.error_num = 0;
                    response.access_token = Services.CreateJWTToken(loginModel);
                    break;
                
                case SignInStatus.Failure:
                    response = new JWTTokenResponse();
                    response.error_num = -1;
                    response.access_token = "Failure";
                    break;
                default:

                    break;
            }

            return Ok(response);
        }


    }
}
