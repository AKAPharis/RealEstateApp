using Microsoft.AspNetCore.Identity;

namespace RealEstateApp.Infrastructure.Identity.Models
{
    public class InternalUserRole : IdentityRole
    {
        public InternalUserRole(string role) : base(role) { }


        public InternalUserRole() : base() { }
    }
}
