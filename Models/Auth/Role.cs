namespace BookManagement.Models.Auth
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Identity;
    public class Role : IdentityRole
    {
        public Role(){}
        public Role(string roleName) : base(roleName) {}
        
       [NotMapped]
        public virtual ICollection<Claim> RoleClaims { get; set; }
    }
}