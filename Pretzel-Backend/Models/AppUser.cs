using Microsoft.AspNetCore.Identity;

namespace Pretzel_Backend.Models
{
    public class AppUser:IdentityUser
    {
        public string Fullname { get; set; }
    }
}
