using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FantasticTour
{
    public class IdentityContext : IdentityDbContext<FanturUser>
    {
        public IdentityContext(DbContextOptions options) : base(options)
        {
        }
    }
}