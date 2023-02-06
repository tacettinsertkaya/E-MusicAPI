using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EMusicAPI.Context;
using EMusicAPI.Entity;
using EMusicAPI.Models.Dto;
using EMusicAPI.Models.Wrappers;
using EMusicAPI.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EMusicAPI.Services
{
    public class UserService : IUserService
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        private IHttpContextAccessor _accessor;

        public AppDbContext _db { get; set; }


        public UserService(
                           IHttpContextAccessor accessor,
                            AppDbContext db,
                            UserManager<ApplicationUser> userManager,
                            RoleManager<IdentityRole> roleManager,
                            IConfiguration configuration)
        {

            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            _db = db;
            _accessor = accessor;
           



        }


        public async Task<AuthenticateResponse> LoginAsync(LoginModel model)
        {
            try
            {

                var user = await _db.Users.Where(p => p.Email == model.Email && p.IsDeleted == false).FirstOrDefaultAsync();

                if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
                {


                    var userRoles = await userManager.GetRolesAsync(user);

                    var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }



                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                    var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.UtcNow.AddDays(7),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                        );

                    var response = new AuthenticateResponse()
                    {
                        UserId = user.Id,
                        UserName = user.Name,
                        Name = user.Name,
                        Surname = user.Surname,
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        Expiration = token.ValidTo,
                        Roles = userRoles.ToList(),


                    };
                    return response;

                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }




        public async Task<ApplicationUser> RegisterAsync(RegisterModel model)
        {

            try
            {
                var userExists = await userManager.FindByEmailAsync(model.Email);
                if (userExists != null)
                    return null;




                ApplicationUser user = new ApplicationUser()
                {
                    UserName = model.UserName,
                    Name = model.UserName,
                    Surname = model.UserName,
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };

                var result = await userManager.CreateAsync(user, model.Password);

               
                await _db.SaveChangesAsync();

         

                if (!result.Succeeded)
                    throw new Exception();

                if (!await roleManager.RoleExistsAsync(model.Statu))
                    await roleManager.CreateAsync(new IdentityRole(model.Statu));

                if (await roleManager.RoleExistsAsync(model.Statu))
                {
                    await userManager.AddToRoleAsync(user, model.Statu);
                }




                return user;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

     

     
     
     

    
       





    }
}


