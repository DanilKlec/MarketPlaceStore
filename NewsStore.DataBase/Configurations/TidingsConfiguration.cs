using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsStore.Core.Models;
using NewsStore.DataBase.Entites;

namespace News.DataBase.Configurations
{
    public class TidingsConfiguration : IEntityTypeConfiguration<TidingsEntites>
    {
        public void Configure(EntityTypeBuilder<TidingsEntites> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(e => e.Users)
                .WithMany(e => e.Tidings)
                .HasForeignKey(e => e.UsersId)
                .IsRequired();

            builder.Property(b => b.Description).IsRequired();
            builder.Property(b => b.Name).IsRequired();
            builder.Property(b => b.Number);
        }
    }
}
