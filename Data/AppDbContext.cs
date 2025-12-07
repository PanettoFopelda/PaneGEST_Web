using Microsoft.EntityFrameworkCore;

namespace PaneGEST_Blazor_MySQL.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        // As tabelas virão aqui depois
    }
}
