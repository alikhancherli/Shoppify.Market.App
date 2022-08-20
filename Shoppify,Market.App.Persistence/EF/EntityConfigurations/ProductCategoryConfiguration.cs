using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoppify.Market.App.Domain.Entites;

namespace Shoppify.Market.App.Persistence.EF.EntityConfigurations
{
    public sealed class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Title).HasMaxLength(300).IsRequired();
            builder.Property(a => a.Description).HasMaxLength(2000);
            builder.Property(a => a.Image).IsRequired();

            builder.HasOne(a => a.Parent)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
