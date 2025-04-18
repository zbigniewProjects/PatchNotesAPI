using Microsoft.EntityFrameworkCore;

namespace DestructorsNetApi.Data
{
    public class PatchNotesDbContext (IConfiguration configuration) : DbContext 
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("PatchNotesDB"));
        }


        public DbSet<PatchNote> PatchNotes { get; set; }
    }
}
