using MediatR;
using Shoppify.Market.App.Service.Dtos.ProductCategories;

namespace Shoppify.Market.App.Service.Queries.ProductCategories;

public sealed record GetProductCategoryListQuery(bool OnlyEnabled = false) : IRequest<IList<ProductCategoryDto>>;