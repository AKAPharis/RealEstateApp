using Microsoft.AspNetCore.Identity;

namespace RealEstateApp.Infrastructure.Identity.Models
{
    public class InternalUser : IdentityUser
    {
        public string FirsName { get; set; }
        public string LastName { get; set; }
        public string DocumentId { get; set; }
        public bool IsActive { get; set; }

    }
}
