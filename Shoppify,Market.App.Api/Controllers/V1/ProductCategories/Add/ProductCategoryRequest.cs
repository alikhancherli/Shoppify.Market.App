using System.ComponentModel.DataAnnotations;

namespace Shoppify.Market.App.Api.Controllers.V1.ProductCategories.Add
{
    public class ProductCategoryRequest
    {
        public int? ParentId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Guid Image { get; set; }
    }
}
