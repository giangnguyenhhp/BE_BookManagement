namespace BookManagement.Models.Request.Account
{
    public class UpdateAccount
    {
        public string UserName { get; set; }

        public string Email { get; set; }
        
        public List<string>? Roles { get; set; }
        
        
    }
}