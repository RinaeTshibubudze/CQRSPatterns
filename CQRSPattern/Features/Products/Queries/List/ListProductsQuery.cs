using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSPattern.Features.Products.Dtos;
using MediatR;

namespace CQRSPattern.Features.Products.Queries.List
{
    public record ListProductsQuery : IRequest<List<ProductDto>>;
}