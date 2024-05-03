using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using YuTechsBL.Dtos;
using YuTechsEF.Entites;
using static System.Net.WebRequestMethods;

namespace YuTechsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<ApplicationUser> userManager,IConfiguration configuration)
        {
            this._userManager=userManager;
            this._configuration=configuration;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterUserDto UserDto)
        {
            if (ModelState.IsValid) 
            {
                var user = await _userManager.FindByNameAsync(UserDto.UserName);
                if (user != null)
                {
                    ModelState.AddModelError("UserName", "UserName is already taken.");

                    return BadRequest(ModelState);
                }

                ApplicationUser applicationUser = new ApplicationUser();
                applicationUser.UserName = UserDto.UserName;
                //applicationUser.Email = UserDto.Email;
                //applicationUser.FirstName= UserDto.FirstName;

                var identityResult = await _userManager.CreateAsync(applicationUser, UserDto.Password);
                if (identityResult.Succeeded) 
                {
                    return Ok("Account Created Success");
                }
                else 
                {
                    return BadRequest(identityResult.Errors.First());
                }
            
            }
            return BadRequest(ModelState);


        }
      
        
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (ModelState.IsValid) 
            {
               var result = await _userManager.FindByNameAsync(loginDto.UserName);
              
                if(result != null) 
                {
                    if (await _userManager.CheckPasswordAsync(result, loginDto.Password)) 
                    {
                        //claims id-role 
                        List<Claim> myClaims = new List<Claim>();
                        myClaims.Add(new Claim(ClaimTypes.NameIdentifier, result.Id));
                        myClaims.Add(new Claim(ClaimTypes.Name, result.UserName));

                        var role = await _userManager.GetRolesAsync(result);
                        if (role != null) 
                        {
                         foreach(var item in role) 
                            {
                                myClaims.Add(new Claim(ClaimTypes.Role, item));
                            }
                        }
                        //signingCredentials
                        var authSecretKey =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:authSecuretKey"]));
                        SigningCredentials credentials = 
                            new SigningCredentials(authSecretKey, SecurityAlgorithms.HmacSha256);
                        //Repersent token 
                        JwtSecurityToken mytoken = new JwtSecurityToken(
                        issuer: _configuration["JWT:issuer"],
                        audience: _configuration["JWT:audience"],
                        expires:DateTime.Now.AddDays(1),
                        claims:myClaims,
                        signingCredentials: credentials
                            );

                        //Create token 
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                            expires= mytoken.ValidTo
                        });
                    }
                    return BadRequest("Invalid Login Account");
                
                }
                return BadRequest("Invalid Login Account");
            }
            return BadRequest("Invalid Login Account");

        }
    }
}
