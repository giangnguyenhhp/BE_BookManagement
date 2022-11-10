namespace BookManagement.Controllers.AuthenticationControllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Models.Request.Roles;
    using Microsoft.AspNetCore.Mvc;
    using Models.Auth;
    using Service.IAuth;
    [Authorize(Roles = SystemRoles.Admin)]
    [ApiController] [Route("[controller]")]
    public class RolesController : ControllerBase
    {
        readonly IRoleRepository _roleRepository;

        public RolesController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet] 
        public async Task<IActionResult> GetAllRoles()
        {
            return Ok(await _roleRepository.GetAllRoles());
        }

        [HttpPost("creat-role")] 
        public async Task<IActionResult> CreatNewRole(CreatRole request)
        {
            return Ok(await _roleRepository.CreatRole(request));
        }

        [HttpPut("update-role/{id}")] 
        public async Task<IActionResult> UpdateRole(string id,[FromBody]UpdateRole request)
        {
            return Ok(await _roleRepository.UpdateRole(id,request));
        }

        [HttpDelete("delete-role/{name}")] 
        public async Task<IActionResult> DeleteRole(string name)
        {
            return Ok(await _roleRepository.DeleteRole(name));
        }

        [HttpPost("map-permission")] 
        public async Task<IActionResult> MapPermission([FromBody] MapPermissionRequest request)
        {
            return Ok(await _roleRepository.MapPermission(request));
        }
        
    }
}