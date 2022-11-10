namespace BookManagement.Service.Impl.Auth
{
    using IAuth;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Auth;
    using Models.Request.Account;
    public class AuthenticateRepository : ControllerBase, IAuthenticateRepository
    {
        readonly UserManager<User> _userManager;
        readonly RoleManager<Role> _roleManager;
        public AuthenticateRepository(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                new Response { Status = "Error", Message = "User already exists!" });

            User user = new()
            {
            Id = Guid.NewGuid().ToString(),
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);


            if (!await _roleManager.RoleExistsAsync(SystemRoles.User))
                await _roleManager.CreateAsync(new Role(SystemRoles.User));

            if (await _roleManager.RoleExistsAsync(SystemRoles.User))
                await _userManager.AddToRoleAsync(user, SystemRoles.User);


            return !result.Succeeded
            ? StatusCode(StatusCodes.Status500InternalServerError,
            new Response
            {
            Status = "Error", Message = "User creation failed! Please check user details and try again."
            })
            : Ok(new Response { Status = "Success", Message = "User created successfully" });
        }
        public async Task<IActionResult> RegisterAdmin(RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            User user = new()
            {
            Id = Guid.NewGuid().ToString(),
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again!" });
            

            if (model.Roles != null && model.Roles.Any())
            {
                await _userManager.AddToRolesAsync(user, model.Roles);
            }

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

    }
}