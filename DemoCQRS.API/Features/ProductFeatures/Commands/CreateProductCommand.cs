using DemoCQRS.API.Context;
using DemoCQRS.API.Models;
using MediatR;

namespace DemoCQRS.API.Features.ProductFeatures.Commands
{
    public class CreateProductCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Detail { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
        {
            private readonly ApplicationContext _context;
            public CreateProductCommandHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<Guid> Handle(CreateProductCommand command, CancellationToken cancellationToken)
            {
                var product = new Product();
                product.Name = command.Name;
                product.Detail = command.Detail;
                product.Price = command.Price;
                product.Quantity = command.Quantity;
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return product.Id;
            }
        }
    }
}
