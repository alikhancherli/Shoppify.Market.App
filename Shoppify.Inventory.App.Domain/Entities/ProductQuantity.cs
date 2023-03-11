using Shoppify.Market.App.Domain.Entites;

namespace Shoppify.Inventory.App.Domain.Entities
{
    public class ProductQuantity : BaseEntity
    {
        private ProductQuantity()
        {

        }

        public int ProductId { get; private set; }
        public long Quantity { get; private set; }
        public string? Descriptions { get; private set; }

        public static ProductQuantity New(int productId, long quantity, string? descriptions) =>
            new ProductQuantity()
            {
                CreatedDateUtc = DateTime.Now,
                ProductId = productId,
                Quantity = quantity,
                Descriptions = descriptions
            };

        /// <summary>
        /// New object with zero quantity
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="descriptions">Descriptions of quantity insertion</param>
        /// <returns></returns>
        public static ProductQuantity New(int productId, string? descriptions) =>
            New(productId, 0, descriptions);

        public void IncreaseQuantity(int quantity)
        {
            Quantity += quantity;
            ModifiedDateUtc = DateTime.Now;
        }

        public void DecreaseQuantity(int quantity)
        {
            Quantity -= quantity;
            ModifiedDateUtc = DateTime.Now;
        }
    }
}
