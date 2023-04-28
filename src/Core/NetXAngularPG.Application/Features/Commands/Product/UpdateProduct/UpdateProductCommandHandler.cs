using MediatR;
using NetXAngularPG.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetXAngularPG.Application.Features.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        readonly IProductQueryRepository _productQueryRepository;
        readonly IProductCommandRepository _productCommandRepository;

        public UpdateProductCommandHandler(IProductQueryRepository productQueryRepository, IProductCommandRepository productCommandRepository)
        {
            _productQueryRepository = productQueryRepository;
            _productCommandRepository = productCommandRepository;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Product product = await _productQueryRepository.GetByIdAsync(request.Id);
            product.Stock = request.Stock;
            product.Name = request.Name;
            product.Price = request.Price;
            await _productCommandRepository.SaveAsync();
            return new();
        }
    }
}
