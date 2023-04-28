using MediatR;
using NetXAngularPG.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetXAngularPG.Application.Features.Commands.Product.RemoveProduct
{
    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommandRequest, RemoveProductCommandResponse>
    {
        readonly IProductCommandRepository _productCommandRepository;

        public RemoveProductCommandHandler(IProductCommandRepository productCommandRepository)
        {
            _productCommandRepository = productCommandRepository;
        }

        public async Task<RemoveProductCommandResponse> Handle(RemoveProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _productCommandRepository.RemoveAsync(request.Id);
            await _productCommandRepository.SaveAsync();
            return new();
        }
    }
}
