using MediatR;
using NetXAngularPG.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetXAngularPG.Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        readonly IProductCommandRepository _productCommandRepository;
       

        public CreateProductCommandHandler(IProductCommandRepository productCommandRepository)
        {
            _productCommandRepository = productCommandRepository;
           
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _productCommandRepository.AddAsync(new()
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Stock = request.Stock
            });
            await _productCommandRepository.SaveAsync();
           
            return new();
        }
    }
}
