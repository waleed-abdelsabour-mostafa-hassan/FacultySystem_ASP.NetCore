using Microsoft.AspNetCore.Identity;

namespace ASP.NetCore_MVC2.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Address { get; set; }
    }
}
