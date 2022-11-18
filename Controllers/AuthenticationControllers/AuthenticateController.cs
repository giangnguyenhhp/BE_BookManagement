namespace BookManagement.Controllers.AuthenticationControllers
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Models.Auth;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;
    using Models;
    using Models.Request.Account;
    using Service.IAuth;
    [ApiController] [Route("api/[controller]")]
    public class AuthenticateController : ControllerBase
    {
        readonly IAuthenticateRepository _authenticateRepository;
        readonly UserManager<User> _userManager;
        readonly RoleManager<Role> _roleManager;
        readonly IConfiguration _configuration;
        readonly MasterDbContext _context;

        public AuthenticateController(IAuthenticateRepository authenticateRepository, UserManager<User> userManager, RoleManager<Role> roleManager, IConfiguration configuration, MasterDbContext context)
        {
            _authenticateRepository = authenticateRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
        }

        private JwtSecurityToken GetToken(IEnumerable<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(7),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
            return token;
        }
        [HttpPost] [Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                // lay tat ca role cua user dang nhap
                var roles = await _userManager.GetRolesAsync(user);
                var roleList = await _context.Roles.Where(r => roles.Contains(r.Name)).ToListAsync();

                var authClaims = new List<Claim>
                {
                new(ClaimTypes.Name, user.UserName),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                //lay role cua user de add vao token
                var userRoles = await _userManager.GetRolesAsync(user);
                authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));

                //lay claim trong list roles
                var claims = new List<Claim>();
                foreach (var role in roleList)
                {
                    var claimsInRole = await _roleManager.GetClaimsAsync(role);
                    if (claimsInRole.Any())
                    {
                        claims.AddRange(claimsInRole);
                    }
                }

                if (claims.Any())
                {
                    authClaims.AddRange(claims);
                }

                var token = GetToken(authClaims);
                return Ok(new
                {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,
                claims = claims.Select(x=>x.Value)
                });
            }
            return Unauthorized();
        }

        [HttpPost] [Route("register")] 
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            return Ok(await _authenticateRepository.Register(model));
        }

        [HttpPost] [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            return Ok(await _authenticateRepository.RegisterAdmin(model));
        }
    }
}