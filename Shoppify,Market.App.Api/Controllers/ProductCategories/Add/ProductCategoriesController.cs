using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoppify.Market.App.Infrastructure.ResultConfiguration;
using Shoppify.Market.App.Service.Commands.ProductCategories;

namespace Shoppify.Market.App.Api.Controllers.ProductCategories.Add
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoriesController : ApiController
    {
        private readonly IMediator _mediator;

        public ProductCategoriesController(IMediator mediator)
        {
            ArgumentNullException.ThrowIfNull(mediator, nameof(mediator));
            _mediator = mediator;
        }

        /// <summary>
        /// Add a product category
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ValidationErrorResult), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddProductCategory(
            [FromBody] ProductCategoryRequest model,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new AddProductCategoryCommand(model.ParentId, model.Title, model.Description, model.Image), cancellationToken);

            return HandlerResultExcecution(result);
        }
    }
}
