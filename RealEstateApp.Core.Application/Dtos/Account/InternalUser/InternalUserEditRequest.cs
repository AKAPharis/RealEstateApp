using Microsoft.AspNetCore.Http;

namespace RealEstateApp.Core.Application.Dtos.Account.InternalUser
{
    public class InternalUserEditRequest
    {
        public string? Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DocumentId { get; set; }
        public string PhoneNumber { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string Role { get; set; }

    }
}
