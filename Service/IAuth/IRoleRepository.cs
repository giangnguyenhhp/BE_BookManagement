namespace BookManagement.Service.IAuth
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BookManagement.Models.Auth;
    using BookManagement.Models.Request.Roles;
    public interface IRoleRepository
    {
        public Task<List<Role>> GetAllRoles();

        public Task<Role> CreatRole(CreatRole newRole);
        public Task<Role> MapPermission(MapPermissionRequest request);


        public Task<Role> UpdateRole(string id, UpdateRole request);
        public Task<Role> DeleteRole(string name);
    }
}