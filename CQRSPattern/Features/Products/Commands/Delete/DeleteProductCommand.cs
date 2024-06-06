using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace CQRSPattern.Features.Products.Commands.Delete
{
    public record DeleteProductCommand(Guid Id) : IRequest;
}