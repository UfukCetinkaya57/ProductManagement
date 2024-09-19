using MediatR;
using ProductManagement.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly ProductManagementDbContext _dbContext;

        public UpdateProductCommandHandler(ProductManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products.FindAsync(request.Id);
            if (product == null)
            {
                return false;
            }

            product.Name = request.Name;
            product.Price = request.Price;

            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
