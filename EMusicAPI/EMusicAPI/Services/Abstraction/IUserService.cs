using EMusicAPI.Entity;
using EMusicAPI.Models.DataFilter;
using EMusicAPI.Models.Dto;
using EMusicAPI.Models.Wrappers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMusicAPI.Services.Abstraction
{
    public interface IUserService
    {
        Task<AuthenticateResponse> LoginAsync(LoginModel model);
        Task<ApplicationUser> RegisterAsync(RegisterModel model);
       
       
    }
}
