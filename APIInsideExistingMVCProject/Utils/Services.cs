using APIInsideExistingMVCProject.Models;
using APIInsideExistingMVCProject.Models.API;
using System;
using System.Collections.Generic;


using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace APIInsideExistingMVCProject.Utils
{
    public class Services
    {
        public static string CreateJWTToken(LoginModel loginModel, List<string> roles = null)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,loginModel.UserName),
            };
            if (roles != null)
            {
                roles.ForEach(role =>
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                });
            }
            var expiredMinute = 60; //Or how many you want;
            var validTo = DateTime.Now.AddMinutes(expiredMinute);
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("kOd8Q~UGneMFWFfYpeNc7rfy3I2mjPHUPPbmbdtJ"));//Or whatever you wnat;
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                                                    issuer: "787f414e-9a15-48be-beda-f13583a1ed96",//Or whatever you want
                                                    audience: "8363f4f8-f36b-40f0-93d1-9bf80555473b", // Or whatever you want
                                                    claims: claims,
                                                    expires: validTo,
                                                    signingCredentials: signinCredentials);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }
    }
}