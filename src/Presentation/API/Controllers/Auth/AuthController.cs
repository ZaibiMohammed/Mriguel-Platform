using Mriguel.Identity.Models;
using Mriguel.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Mriguel.API.Controllers.Auth
{
    /// <summary>
    /// Controller for authentication
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtTokenService _tokenService;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            JwtTokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Login with email and password
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            
            if (user == null)
            {
                return Unauthorized(new AuthResponse { Success = false, Error = "Invalid credentials" });
            }
            
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            
            if (!result.Succeeded)
            {
                return Unauthorized(new AuthResponse { Success = false, Error = "Invalid credentials" });
            }
            
            var token = await _tokenService.GenerateTokenAsync(user);
            
            return Ok(new AuthResponse
            {
                Success = true,
                Token = token,
                UserId = user.Id
            });
        }
    }
    
    /// <summary>
    /// Authentication response model
    /// </summary>
    public class AuthResponse
    {
        public bool Success { get; set; }
        public string? Token { get; set; }
        public string? UserId { get; set; }
        public string? Error { get; set; }
    }
}
