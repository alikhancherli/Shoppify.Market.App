using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoppify.Market.App.Identity;
using Shoppify.Market.App.Infrastructure.ResultConfiguration;
using Shoppify.Market.App.Service.Dtos.ProductCategories;
using Shoppify.Market.App.Service.Queries.ProductCategories;
using Shoppify.Market.App.Service.Services.Contracts;

namespace Shoppify.Market.App.Api.Controllers.V1.ProductCategories.GetList
{
    [Authorize(Policy = Policies.AdminAccess)]
    public class ProductCategoriesController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly ILiteXService _liteXService;
        public ProductCategoriesController(IMediator mediator, ILiteXService liteXService)
        {
            ArgumentNullException.ThrowIfNull(mediator, nameof(mediator));
            ArgumentNullException.ThrowIfNull(liteXService, nameof(liteXService));
            _mediator = mediator;
            _liteXService = liteXService;
        }

        /// <summary>
        /// Returns list of product category
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ProductCategoryDto), StatusCodes.Status200OK)]
        public async Task<ApplicationApiResult<IList<ProductCategoryDto>>> GetProductCategoryList(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetProductCategoryListQuery(), cancellationToken);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("file")]
        public async Task<IActionResult> GetFile(IFormFile formFile)
        {
            var blobs = await _liteXService.UploadBlobAsync(formFile);
            return Ok(blobs);
        }
    }
}
