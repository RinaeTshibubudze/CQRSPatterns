using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSPattern.Features.Products.Dtos
{
    public record ProductDto(Guid Id, string Name, string Description, decimal Price);
}