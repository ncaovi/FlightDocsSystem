using FlightDocsSystem.Model;
using Microsoft.EntityFrameworkCore;

namespace FlightDocsSystem.Models
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<UserModel> UserModels { get; set; }
        public DbSet<CargoManifest> CargoManifests { get; set; }
        public DbSet<DocumentList> DocumentLists { get; set; }
        public DbSet<RoleModel> RoleModels { get; set; }
        public DbSet<GroupPermission> GroupPermissions { get; set; }

    }
}