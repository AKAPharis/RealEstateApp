using Microsoft.AspNetCore.Identity;

namespace RealEstateApp.Infrastructure.Identity.Models
{
    public class CustomerRole : IdentityRole
    {
        public CustomerRole(string role) : base(role) { }


        public CustomerRole() : base() { }
        
    }
}
