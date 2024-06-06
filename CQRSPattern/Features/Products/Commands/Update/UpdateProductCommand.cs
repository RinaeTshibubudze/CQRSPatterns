using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace CQRSPattern.Features.Products.Commands.Update
{
    public record DeleteProductCommand(Guid Id) : IRequest;
}