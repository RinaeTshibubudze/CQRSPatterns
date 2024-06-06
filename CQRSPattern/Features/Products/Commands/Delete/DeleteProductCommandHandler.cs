using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSPattern.Persistence;
using MediatR;

namespace CQRSPattern.Features.Products.Commands.Delete
{
    public class DeleteProductCommandHandler(AppDbContext context) : IRequestHandler<DeleteProductCommand>
    {
        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await context.Products.FindAsync(request.Id);
            if (product == null) return;
            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }
    }
}