using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using News.DataBase.Configurations;
using NewsStore.Core.Models;
using NewsStore.DataBase.Configurations;
using NewsStore.DataBase.Entites;
using System.Reflection.Metadata;

namespace NewsStore.DataBase
{
    public class TidingsStoreDbContext : DbContext
    {
        public TidingsStoreDbContext(DbContextOptions<TidingsStoreDbContext> options) : base(options)
        {

        }

        public DbSet<TidingsEntites> Tidings { get; set; }
        public DbSet<UsersEntites> Users { get; set; }
        public DbSet<RoleEntites> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TidingsConfiguration());
            modelBuilder.ApplyConfiguration(new UsersConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());

            string adminRoleName = "admin";
            string userRoleName = "user";

            string adminPassword = "123456";

            // добавляем роли
            Role adminRole = new Role { Id = Guid.NewGuid(), Name = adminRoleName };
            Role userRole = new Role { Id = Guid.NewGuid(), Name = userRoleName };
            Users adminUser = new Users()
            {
                Id = Guid.NewGuid(),
                UserName = "admin",
                Password = adminPassword,
                RoleId = adminRole.Id,
            };

            //modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            //modelBuilder.Entity<Users>().HasData(new Users[] { adminUser });

            base.OnModelCreating(modelBuilder);
        }
    }
}
