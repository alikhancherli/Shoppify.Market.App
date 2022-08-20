using Shoppify.Market.App.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppify.Market.App.Service.Dtos.ProductCategories
{
    public class ProductCategoryDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public Guid Image { get; set; }
        public bool IsDisabled { get; set; }
        public ProductCategoryDto? Parent { get; set; }
    }
}
