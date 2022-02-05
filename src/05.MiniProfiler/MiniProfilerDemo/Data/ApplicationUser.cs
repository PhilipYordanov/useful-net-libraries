using Microsoft.AspNetCore.Identity;

namespace MiniProfilerDemo.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
