using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoppify.Market.App.Domain.Entites;
using Shoppify.Market.App.Domain.Entites.Embedations;

namespace Shoppify.Market.App.Persistence.EF.EntityConfigurations
{
    public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Title).HasMaxLength(500).IsRequired();
            builder.Property(a => a.Description).HasMaxLength(2000).IsRequired();
            builder.Property(a => a.Text).HasMaxLength(5000).IsRequired();

            builder.HasOne<ProductCategory>()
                .WithMany()
                .IsRequired()
                .HasForeignKey(a => a.ProductCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a=>a.ProductGalleries)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
