using MediatR;
using Shoppify.Market.App.Domain.Entites;

namespace Shoppify.Market.App.Service.Commands.ProductCategories;

public sealed record EditProductCategoryCommand(
           int Id,
           ProductCategory? Parent,
           string Title,
           string Description,
           Guid Image) : IRequest<bool>;