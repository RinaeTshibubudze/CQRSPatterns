using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSPattern.Features.Products.Dtos;
using CQRSPattern.Persistence;
using MediatR;

namespace CQRSPattern.Features.Products.Queries.Get
{
    public class GetProductQueryHandler(AppDbContext context)
    : IRequestHandler<GetProductQuery, ProductDto?>
    {
        public async Task<ProductDto?> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await context.Products.FindAsync(request.Id);
            if (product == null)
            {
                return null;
            }
            return new ProductDto(product.Id, product.Name, product.Description, product.Price);
        }
    }
}