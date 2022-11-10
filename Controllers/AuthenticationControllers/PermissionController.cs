using Microsoft.AspNetCore.Mvc;
namespace BookManagement.Controllers.AuthenticationControllers
{
    using Models.Auth;
    [ApiController]
    [Route("[controller]")]
    public class PermissionController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllPermission()
        {
            return Ok(SystemPermission.DefaultClaims);
        }
    }
}