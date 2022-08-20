using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Shoppify.Market.App.Domain.Entites;
using Shoppify.Market.App.Persistence.Repositories.Contracts;
using Shoppify.Market.App.Service.Commands.ProductCategories;
using Shoppify.Market.App.Service.Resources;

namespace Shoppify.Market.App.Service.Handlers.ProductCategories
{
    public class AddProductCategoryCommandHandler : IRequestHandler<AddProductCategoryCommand, AppHandlerResult>
    {
        private readonly IRepository<ProductCategory> _repository;
        private readonly IValidator<AddProductCategoryCommand> _validator;
        private GlobalResources _globalResources;

        public AddProductCategoryCommandHandler(
            IRepository<ProductCategory> repository,
            IValidator<AddProductCategoryCommand> validator,
            IOptionsMonitor<GlobalResources> globalResourceOptions)
        {
            _repository = repository;
            _validator = validator;

            _globalResources = globalResourceOptions.CurrentValue;
            globalResourceOptions.OnChange(_ => _globalResources = _);
        }

        public async Task<AppHandlerResult> Handle(AddProductCategoryCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return AppHandlerResult.ValidationError(validationResult.Errors.Select(g => g.ErrorMessage).ToList());

            ProductCategory? parent = null;

            if (request.ParentId is not null)
            {
                parent = await _repository.GetByIdAsync(cancellationToken, request.ParentId);

                if (parent is null)
                    return AppHandlerResult.ValidationError(_globalResources.ParentNotfound);
            }

            var productCategory = ProductCategory.New(
                parent,
                request.Title,
                request.Description,
                request.Image);

            await _repository.AddAsync(productCategory, cancellationToken);

            return AppHandlerResult.Ok(productCategory);
        }
    }
}
