namespace BookManagement.Controllers.AuthenticationControllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Auth;
    using Models.Request.Account;
    using Service.IAuth;
    [Authorize(Roles = SystemRoles.Admin)]
    public class AccountController : ControllerBase
    {
        readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet("account")]
        public async Task<IActionResult> GetAllAccount()
        {
            return Ok(await _accountRepository.GetAllAccount());
        }
        
        [HttpGet("get-roles-by-account/{id}")]
        public async Task<IActionResult> GetRolesByAccountId(string id)
        {
            return Ok(await _accountRepository.GetRolesByAccountId(id));
        }

        [HttpPut("account-update/{id}")] 
        public async Task<IActionResult> UpdateAccount(string id, [FromBody] UpdateAccount request)
        {
            return Ok(await _accountRepository.UpdateAccount(id, request));
        }

        [HttpDelete("account-delete/{id}")]
        public async Task<IActionResult> DeleteAccount(string id)
        {
            return Ok(await _accountRepository.DeleteAccount(id));
        }
    }
}