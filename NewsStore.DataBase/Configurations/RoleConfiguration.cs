using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NewsStore.DataBase.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsStore.DataBase.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<RoleEntites>
    {
        public void Configure(EntityTypeBuilder<RoleEntites> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(e => e.Users)
                .WithOne(e => e.Role)
                .HasForeignKey(e => e.RoleId)
                .IsRequired();

            builder.Property(b => b.Name).IsRequired();
        }
    }
}
