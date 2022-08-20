using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoppify.Market.App.Domain.Entites;
using Shoppify.Market.App.Domain.Entites.Embedations;

namespace Shoppify.Market.App.Persistence.EF.EntityConfigurations
{
    public sealed class ProductGalleryConfiguration : IEntityTypeConfiguration<ProductGallery>
    {
        public void Configure(EntityTypeBuilder<ProductGallery> builder)
        {
            builder.HasKey(a => a.Id);


        }
    }
}
