namespace BookManagement.Models.Request.Roles
{
    using System.Collections.Generic;
    public class MapPermissionRequest
    {
        public string RoleId { get; set; }

        public List<string> Permissions { get; set; }
    }
}