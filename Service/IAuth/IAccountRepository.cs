namespace BookManagement.Service.IAuth
{
    using Microsoft.AspNetCore.Identity;
    using Models.Auth;
    using Models.Request.Account;
    public interface IAccountRepository
    {
        public Task<List<User>> GetAllAccount();
        public Task<User> UpdateAccount(string id, UpdateAccount request);
        public Task<User> DeleteAccount(string id);
        public Task<List<string>> GetRolesByAccountId(string id);
    }
}