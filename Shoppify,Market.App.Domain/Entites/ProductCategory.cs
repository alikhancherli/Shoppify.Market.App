namespace Shoppify.Market.App.Domain.Entites
{
    public class ProductCategory : BaseEntity
    {
        private ProductCategory()
        {

        }

        public ProductCategory? Parent { get; private set; }
        public string Title { get; private set; }
        public string? Description { get; private set; }
        public Guid Image { get; private set; }

        public static ProductCategory New(
            ProductCategory? parent,
            string title,
            string description,
            Guid image) =>
            new ProductCategory()
            {
                Parent= parent,
                Title = title,
                Description = description,
                Image = image
            };

        public void Edit(
            ProductCategory? parent,
            string title,
            string description,
            Guid image)
        {
            Parent = parent;
            Title = title;
            Description = description;
            Image = image;
            ModifiedDateUtc = DateTimeOffset.UtcNow;
        }
    }
}
