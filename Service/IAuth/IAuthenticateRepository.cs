namespace BookManagement.Service.IAuth
{
    using Microsoft.AspNetCore.Mvc;
    using Models.Auth;
    using Models.Request.Account;
    public interface IAuthenticateRepository
    {
        public Task<IActionResult> Register([FromBody] RegisterModel model);
        public Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model);

    }
}