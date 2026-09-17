using KASHOP.BLL.Common;
using KASHOP.DAL.Dto;
using KASHOP.DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services
{
    public class AuthunticationService : IAuthunticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _confg;

        public AuthunticationService( UserManager<ApplicationUser>userManager,IEmailSender emailSender,IConfiguration confg
            )
        {
             _userManager = userManager;
           _emailSender = emailSender;
            _confg = confg;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {

            var user= await _userManager.FindByEmailAsync(request.Email);
            if (user==null) return new LoginResponse()
            {
                Message = "Invalid Email "
            };
            if (!await _userManager.IsEmailConfirmedAsync(user)) {

                return new LoginResponse()
                {
                    Message = "Email Not Confirmed"
                };
                }
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid) return new LoginResponse()
            {
                Message = "Invalid Password"
            };

            return new LoginResponse() { Message ="Success", AccessToken = await GenrateToken(user) };

        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            var user= request.Adapt<ApplicationUser>();
            var result = await _userManager.CreateAsync(user,request.Password);
            if (!result.Succeeded) return new RegisterResponse()
            {

                Message = "Error",
                Errors = result.Errors.Select(e => e.Description).ToList()      

            };
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token= Uri.EscapeDataString(token);
            var emailURL = $"https://localhost:7166/api/Account/ConfirmEmail?token={token}&userId={user.Id}";
            await _emailSender.SendEmailAsync(request.Email, "confirm email", "<div><h2>welcome to KAShop app</h2>" +
                $"<a href='{emailURL}'> confirm</a>" +
               
                "<div>");
            return new RegisterResponse()
            {
                Message = "Success"
            };


        }
        public async Task<bool>ConfirmEmail (ConfirmationRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user is  null) return false;
            request.Token = Uri.UnescapeDataString(request.Token);
            var result = await _userManager.ConfirmEmailAsync(user, request.Token);
            return result.Succeeded;
        }

        private async Task<string> GenrateToken(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var userClaims = new List<Claim>() { 
            
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,string.Join(",",roles))


            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_confg["ApiSettings:SecretKey"]));

            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _confg["ApiSettings:SecretKey"],
                audience: _confg["ApiSettings:SecretKey"],
                claims: userClaims,
                expires: DateTime.UtcNow.AddDays(20),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
