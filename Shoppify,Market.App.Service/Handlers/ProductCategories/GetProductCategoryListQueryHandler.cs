using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shoppify.Market.App.Domain.Entites;
using Shoppify.Market.App.Persistence.Repositories.Contracts;
using Shoppify.Market.App.Service.Dtos.ProductCategories;
using Shoppify.Market.App.Service.Queries.ProductCategories;
using System.Linq.Expressions;

namespace Shoppify.Market.App.Service.Handlers.ProductCategories
{
    public class GetProductCategoryListQueryHandler : IRequestHandler<GetProductCategoryListQuery, IList<ProductCategoryDto>>
    {
        private readonly IRepository<ProductCategory> _repository;
        private readonly IMapper _mapper;

        public GetProductCategoryListQueryHandler(IMapper mapper, IRepository<ProductCategory> repository)
        {
            ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));
            ArgumentNullException.ThrowIfNull(repository, nameof(repository));
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IList<ProductCategoryDto>> Handle(GetProductCategoryListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<ProductCategory, bool>> predicate = _ => request.OnlyEnabled ? !_.IsDisabled : true;

            var result = await _repository.TableAsNoTracking.Include(a => a.Parent).Where(predicate).ToListAsync(cancellationToken);

            return _mapper.Map<IList<ProductCategoryDto>>(result);
        }
    }
}
