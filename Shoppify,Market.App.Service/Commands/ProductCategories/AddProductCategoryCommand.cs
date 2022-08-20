using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Shoppify.Market.App.Domain.Constants;
using Shoppify.Market.App.Service.Handlers;
using Shoppify.Market.App.Service.Resources;

namespace Shoppify.Market.App.Service.Commands.ProductCategories;

public record AddProductCategoryCommand(
    int? ParentId,
    string Title,
    string Description,
    Guid Image) : IRequest<AppHandlerResult>;

public sealed class AddProductCategoryCommandValidator : AbstractValidator<AddProductCategoryCommand>
{
    private GlobalResources _globalResources;

    public AddProductCategoryCommandValidator(
        IOptions<GlobalResources> globalResourcesOptionsMonitor)
    {
        _globalResources = globalResourcesOptionsMonitor.Value;

        RuleFor(a => a.Title)
            .NotEmpty()
            .NotNull()
            .WithName(_globalResources.ProductCategoryResources.TitleName)
            .WithMessage(_globalResources.RequiredField)
            .Length(ProductCategoryConstants.TitleMinLength, ProductCategoryConstants.TitleMaxLength)
            .WithMessage(_globalResources.LengthExceeded);

        RuleFor(a => a.Description)
            .NotEmpty()
            .NotNull()
            .WithName(_globalResources.ProductCategoryResources.DescriptionName)
            .WithMessage(_globalResources.RequiredField)
            .Length(ProductCategoryConstants.DescriptionMinLength, ProductCategoryConstants.DescriptionMaxLength)
            .WithMessage(_globalResources.LengthExceeded);
    }
}
