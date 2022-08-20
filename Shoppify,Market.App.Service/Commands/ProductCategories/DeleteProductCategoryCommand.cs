using MediatR;

namespace Shoppify.Market.App.Service.Commands.ProductCategories;

public sealed record DeleteProductCategoryCommand(int Id) : IRequest<bool>;