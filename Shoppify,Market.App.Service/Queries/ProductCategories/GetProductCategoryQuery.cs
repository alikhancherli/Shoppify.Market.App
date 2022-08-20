using MediatR;
using Shoppify.Market.App.Service.Dtos.ProductCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppify.Market.App.Service.Queries.ProductCategories;

public record GetProductCategoryQuery(int id) : IRequest<ProductCategoryDto>;