using KASHOP.BLL.Common;
using KASHOP.DAL.Dto;
using KASHOP.DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services
{
    public class AuthunticationService : IAuthunticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public AuthunticationService( UserManager<ApplicationUser>userManager,IEmailSender emailSender
            )
        {
             _userManager = userManager;
           _emailSender = emailSender;
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
           
            return new LoginResponse() { Message ="Success"};

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
    }
}
