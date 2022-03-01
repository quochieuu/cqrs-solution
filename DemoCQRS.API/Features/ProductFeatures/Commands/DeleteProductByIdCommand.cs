using DemoCQRS.API.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DemoCQRS.API.Features.ProductFeatures.Commands
{
    public class DeleteProductByIdCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand, Guid>
        {
            private readonly ApplicationContext _context;
            public DeleteProductByIdCommandHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<Guid> Handle(DeleteProductByIdCommand command, CancellationToken cancellationToken)
            {
                var product = await _context.Products.Where(a => a.Id == command.Id).FirstOrDefaultAsync();
                if (product == null) return default;
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return product.Id;
            }
        }
    }
}
