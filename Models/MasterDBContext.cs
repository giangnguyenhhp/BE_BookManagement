namespace BookManagement.Models
{
    using Auth;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    public class MasterDbContext : IdentityDbContext<User, Role, string, IdentityUserClaim<string>, IdentityUserRole<string>, IdentityUserLogin<string>, RoleClaim, IdentityUserToken<string>>
    {
        public DbSet<Book> Book { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Catalog> Catalog { get; set; }
    

        public MasterDbContext(DbContextOptions<MasterDbContext> options) : base(options)
        {
        }
    }
}