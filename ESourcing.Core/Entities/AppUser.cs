using Microsoft.AspNetCore.Identity;

namespace ESourcing.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool IsSeller { get; set; }
        public bool IsBuyer { get; set; }
    }
}
