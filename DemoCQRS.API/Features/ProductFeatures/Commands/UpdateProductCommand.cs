using DemoCQRS.API.Context;
using MediatR;

namespace DemoCQRS.API.Features.ProductFeatures.Commands
{
    public class UpdateProductCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Guid>
        {
            private readonly ApplicationContext _context;
            public UpdateProductCommandHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<Guid> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
            {
                var product = _context.Products.Where(a => a.Id == command.Id).FirstOrDefault();

                if (product == null)
                {
                    return default;
                }
                else
                {
                    product.Name = command.Name;
                    product.Detail = command.Detail;
                    product.Price = command.Price;
                    product.Quantity = command.Quantity;
                    await _context.SaveChangesAsync();
                    return product.Id;
                }
            }
        }
    }
}
