namespace BookManagement.Service.Impl.Auth
{
    using IAuth;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Models.Auth;
    using Models.Request.Account;
    public class AccountRepository : IAccountRepository
    {
        readonly MasterDbContext _context;
        readonly RoleManager<Role> _roleManager;
        readonly UserManager<User> _userManager;
        public AccountRepository(MasterDbContext context, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        
        public async Task<List<User>> GetAllAccount()
        {
           return await _context.Users.ToListAsync();
            
        }
        
        public async Task<List<string>> GetRolesByAccountId(string id)
        {
            var checkuser = _context.Users.FirstOrDefault(a => a.Id == id);
            if (checkuser == null) throw new Exception("User not existed");

            var result = await _userManager.GetRolesAsync(checkuser);
            return result.ToList();
        }
        public async Task<User> UpdateAccount(string id, UpdateAccount request)
        {
            var checkuser = _context.Users.FirstOrDefault(a => a.Id == id);
            if (checkuser == null) throw new Exception("User not existed");
            
            //Delete toan bo user ra khoi cac role cu
            var roles = await _userManager.GetRolesAsync(checkuser);
            await _userManager.RemoveFromRolesAsync(checkuser, roles);
            
            checkuser.UserName = request.UserName;
            checkuser.Email = request.Email;
            
            //Add user vao toan bo cac role moi
            if (request.Roles != null && request.Roles.Any())
            {
                await _userManager.AddToRolesAsync(checkuser, request.Roles);
            }
            
            await _context.SaveChangesAsync();
            return checkuser;
        }
        public async Task<User> DeleteAccount(string id)
        {
            var checkuser = _context.Users.FirstOrDefault(x => x.Id == id);
            if (checkuser == null) throw new Exception("User not existed!!");
            _context.Users.Remove(checkuser);
            await _context.SaveChangesAsync();
            return checkuser;
        }

    }
}