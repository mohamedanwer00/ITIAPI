using ITIAPI.DTO;
using ITIAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ITIAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<ApplicationUser> userManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Registration(RegisterUserDto userDto)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = userDto.UserName;
                user.Email = userDto.Email;
                IdentityResult result = await _userManager.CreateAsync(user, userDto.Password);
                if (result.Succeeded)
                    return Ok("Account Added Succesfly");

                return BadRequest(result.Errors.FirstOrDefault());
            }
            return BadRequest(ModelState);
        }

        //Check Account Valid "Login" "Post"
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto userDto)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByNameAsync(userDto.UserName);
                if (user != null)
                {
                    bool found = await _userManager.CheckPasswordAsync(user, userDto.Password);
                    if (found)
                    {
                        //Claims Token
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        //get role
                        var roles = await _userManager.GetRolesAsync(user);
                        foreach (var itemRole in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, itemRole));
                        }
                        SecurityKey securityKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                        SigningCredentials signincred =
                            new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                        //Create token
                        JwtSecurityToken mytoken = new JwtSecurityToken(
                            issuer: _configuration["JWT:ValidIssuer"],//url web api
                            audience: _configuration["JWT:ValidAudiance"],//url consumer angular
                            claims: claims,
                            expires: DateTime.Now.AddDays(30),
                            signingCredentials: signincred
                            );
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                            expiration = mytoken.ValidTo
                        });
                    }
                }
                return Unauthorized();
            }
            return Unauthorized();
        }

    }
}
