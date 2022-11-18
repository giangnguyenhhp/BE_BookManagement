namespace BookManagement.Service.Impl.Auth
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using BookManagement.Models.Auth;
    using Models.Request.Roles;
    using IAuth;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Models;
    public class RoleRepository : IRoleRepository
    {
        private readonly MasterDbContext _context;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        public RoleRepository(MasterDbContext context, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<List<Role>> GetAllRoles()
        {
            var roles = _context.Roles.ToList();
            foreach (var role in roles)
            {
                var listClaims = await _roleManager.GetClaimsAsync(role);
                role.RoleClaims = listClaims;
            }
            return roles;
        }
        public async Task<Role> CreatRole(CreatRole request)
        {
            var newRole = new Role(request.RoleName);
            await _roleManager.CreateAsync(newRole);
            await _context.SaveChangesAsync();
            return newRole;
        }
        public async Task<Role> MapPermission(MapPermissionRequest request)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == request.RoleId);

            if (role == null) throw new Exception("Role not existed!!!");

            if (!request.Permissions.Any()) throw new Exception("Permission not valid!!!");

            //remove all claim
            var claims = await _roleManager.GetClaimsAsync(role);
            foreach (var claim in claims)
            {
                await _roleManager.RemoveClaimAsync(role, claim);
            }

            //all new claim
            foreach (var requestPermission in request.Permissions)
            {
                await _roleManager.AddClaimAsync(role, new Claim(ClaimTypes.Role, requestPermission));
            }
            await _context.SaveChangesAsync();
            return role;
        }
        public async Task<Role> UpdateRole(string id, UpdateRole request)
        {
            var checkRole = _context.Roles.FirstOrDefault(r => r.Id == id);
            if (checkRole == null) throw new Exception("Role not existed!!!");

            checkRole.Name = request.Name;
            checkRole.NormalizedName = request.Name.ToUpper();
            await _context.SaveChangesAsync();
            return checkRole;
        }
        public async Task<Role> DeleteRole(string name)
        {
            var checkRole = _context.Roles.FirstOrDefault(r => r.Name == name);
            if (checkRole == null) throw new Exception("Role not existed");
            _context.Roles.Remove(checkRole);
            await _context.SaveChangesAsync();
            return checkRole;
        }

    }
}