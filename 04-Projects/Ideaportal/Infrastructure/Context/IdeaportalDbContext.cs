using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class IdeaportalDbContext(DbContextOptions<IdeaportalDbContext> options) : IdentityDbContext(options)
    {
    }
}
