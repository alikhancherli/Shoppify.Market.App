using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoppify.Market.App.Infrastructure.ResultConfiguration;
using Shoppify.Market.App.Service.Dtos.ProductCategories;
using Shoppify.Market.App.Service.Queries.ProductCategories;

namespace Shoppify.Market.App.Api.Controllers.ProductCategories.GetList
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductCategoriesController : ApiController
    {
        private readonly IMediator _mediator;

        public ProductCategoriesController(IMediator mediator)
        {
            ArgumentNullException.ThrowIfNull(mediator, nameof(mediator));
            _mediator = mediator;
        }

        /// <summary>
        /// Returns list of product category
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ProductCategoryDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductCategoryList(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetProductCategoryListQuery(), cancellationToken);
            return Ok(result);
        }
    }
}
