#region asdasd
//using InnoApi.Dtos.Account;
//using InnoApi.Interfaces;
//using InnoApi.Models;
//using InnoApi.Service;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;

//namespace InnoApi.Controllers
//{
//    [Route("api/account")]
//    [ApiController]
//    public class AccountController : Controller
//    {
//        private readonly UserManager<AppUserModel> _userManager;
//        private readonly ITokenService _tokenService;
//        private readonly SignInManager<AppUserModel> _signInManager; 
//        private readonly PasswordHashing _passwordHashing;

//        public AccountController(UserManager<AppUserModel> userManager, ITokenService tokenService, SignInManager<AppUserModel> signInManager, PasswordHashing passwordHashing)
//        {
//            _userManager = userManager;
//            _tokenService = tokenService;
//            _signInManager = signInManager;
//            _passwordHashing = passwordHashing;
//        }

//        [HttpPost("login")]

//        public async Task<IActionResult> Login(LoginDto loginDto)
//        {
//            //if(_signInManager == null)
//                //return BadRequest();
//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);
//            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());
//            if(user == null)
//            {
//                return Unauthorized("Invalid username.");
//            }
//            var hashedPassword = _passwordHashing.HashPassword(loginDto.PasswordHash);
//            var result = await _signInManager.CheckPasswordSignInAsync(user, hashedPassword, true);

//            //var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.PasswordHash, false);

//            if (!result.Succeeded)
//            {
//                return Unauthorized("Username not found and/or password incorrect");
//            }
//            else if (result.IsLockedOut)
//            {
//                return Unauthorized("Locked out");
//            }
//            else
//            {
//                return Ok(
//                    new NewUserDto
//                    {
//                        UserName = user.UserName,
//                        Email = user.Email,
//                        Token = _tokenService.CreateToken(user)
//                    });
//            }
//        }


//        [HttpPost("register")]
//        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
//        {

//            try
//            {
//                if (!ModelState.IsValid)
//                    return BadRequest(ModelState);
//                string hashedPassword = _passwordHashing.HashPassword(registerDto.PasswordHash);
//                var appUser = new AppUserModel
//                {

//                    UserName = registerDto.Username,
//                    Email = registerDto.Email,
//                    PasswordHash = hashedPassword,
//                };

//                //var createdUser = await _userManager.CreateAsync(appUser); // Pass password separately
//                IdentityResult result = await _userManager.CreateAsync(appUser);

//                if (result.Succeeded)
//                {
//                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
//                    if (roleResult.Succeeded)
//                    {
//                        return Ok(
//                            new NewUserDto
//                            {
//                                UserName = appUser.UserName,
//                                Email = appUser.Email,
//                                Token = _tokenService.CreateToken(appUser)
//                            }
//                            );
//                    }
//                    else
//                    {
//                        return StatusCode(500, roleResult.Errors);
//                    }
//                }
//                else
//                {
//                    return StatusCode(500, result.Errors);
//                }
//            }
//            catch (Exception e)
//            {
//                // Log the exception
//                return StatusCode(500, new { message = "An error occurred while processing your request.", exception = e.Message });
//            }
//        }

//    }
//}
#endregion
using InnoApi.Dtos.Account;
using InnoApi.Interfaces;
using InnoApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;

namespace InnoApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUserModel> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUserModel> _signInManager;

        public AccountController(UserManager<AppUserModel> userManager, ITokenService tokenService, SignInManager<AppUserModel> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            if (user == null)
            {
                return Unauthorized("Username not found.");
            }

            byte[] passwordBytes = Encoding.UTF8.GetBytes(loginDto.PasswordHash);
            string base64Password = Convert.ToBase64String(passwordBytes);
            //var passwordHasher = new PasswordHasher<AppUserModel>();
            var hashedPassword = base64Password;
            if (hashedPassword != user.PasswordHash)
            {
                return Unauthorized("Password is incorrect.");
            }
            return Ok(new NewUserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            });
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                //string hashedPassword = _passwordHashing.HashPassword(registerDto.PasswordHash);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(registerDto.PasswordHash);

                string base64Password = Convert.ToBase64String(passwordBytes);
                //var passwordHasher = new PasswordHasher<AppUserModel>();
                var hashedPassword = base64Password;
                var appUser = new AppUserModel
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                    PasswordHash = hashedPassword,
                };

                //var createdUser = await _userManager.CreateAsync(appUser); // Pass password separately
                IdentityResult result = await _userManager.CreateAsync(appUser);

                if (result.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto
                            {
                                UserName = appUser.UserName,
                                Email = appUser.Email,
                                Token = _tokenService.CreateToken(appUser)
                            }
                            );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, result.Errors);
                }
            }
            catch (Exception e)
            {
                // Log the exception
                return StatusCode(500, new { message = "An error occurred while processing your request.", exception = e.Message });
            }
        }

    }
}
