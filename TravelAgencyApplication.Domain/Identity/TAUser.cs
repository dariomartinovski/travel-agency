
using Microsoft.AspNetCore.Identity;
using TravelAgencyApplication.Domain.Enum;
using TravelAgencyApplication.Domain.Model;

namespace TravelAgencyApplication.Domain.Identity
{
    public class TAUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber {  get; set; }

        public UserRole UserRole { get; set; }
        
        public virtual ICollection<Reservation>? Reservations { get; set; }
    }
}
