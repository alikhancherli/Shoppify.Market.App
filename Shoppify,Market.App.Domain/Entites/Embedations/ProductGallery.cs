namespace Shoppify.Market.App.Domain.Entites.Embedations
{
    public sealed class ProductGallery : IRootEntity<int>
    {
        private ProductGallery()
        {
        }

        public int Id { get; set; }
        public Guid Image { get; private set; }

        public static ProductGallery New(Guid image) =>
            new ProductGallery()
            {
                Image = image
            };
    }
}
