using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsStore.Core.Models;
using NewsStore.DataBase.Entites;

namespace News.DataBase.Configurations
{
    public class UsersConfiguration : IEntityTypeConfiguration<UsersEntites>
    {
        public void Configure(EntityTypeBuilder<UsersEntites> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(e => e.Role)
                .WithMany(e => e.Users)
                .HasForeignKey(e => e.RoleId)
                .IsRequired();

            builder.HasMany(e => e.Tidings)
                .WithOne(e => e.Users)
                .HasForeignKey(e => e.UsersId)
                .IsRequired();

            builder.Property(e => e.RoleId).IsRequired();
        }
    }
}
