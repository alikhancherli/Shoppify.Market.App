using Shoppify.Market.App.Domain.Entites.Embedations;

namespace Shoppify.Market.App.Domain.Entites
{
    public class Product : BaseEntity
    {
        private Product()
        {

        }

        public int ProductCategoryId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Text { get; private set; }
        public long Price { get; private set; }
        public sbyte Rate { get; private set; }
        public Guid IndexImage { get; private set; }
        public IEnumerable<ProductGallery> ProductGalleries { get; private set; }

        public static Product New(
            int productCategoryId,
            string title,
            string descripton,
            string text,
            long price,
            sbyte rate,
            Guid indexImage,
            IEnumerable<ProductGallery> productGalleries) =>
            new Product()
            {
                ProductCategoryId = productCategoryId,
                Title = title,
                Description = descripton,
                Text = text,
                Price = price,
                Rate = rate,
                IndexImage = indexImage,
                ProductGalleries = productGalleries
            };

        public void Edit(
            int productCategoryId,
            string title,
            string descripton,
            string text,
            long price,
            sbyte rate,
            Guid indexImage,
            IEnumerable<ProductGallery> productGalleries)
        {
            ProductCategoryId = productCategoryId;
            Title = title;
            Description = descripton;
            Text = text;
            Price = price;
            Rate = rate;
            IndexImage = indexImage;
            ProductGalleries = productGalleries;
        }
    }
}
