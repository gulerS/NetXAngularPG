using MediatR;
using Microsoft.EntityFrameworkCore;
using NetXAngularPG.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetXAngularPG.Application.Features.Commands.ProductImageFile.RemoveProductImage
{
    public class RemoveProductImageCommandHandler : IRequestHandler<RemoveProductImageCommandRequest, RemoveProductImageCommandResponse>
    {

        readonly IProductQueryRepository _productQueryRepository;
        readonly IProductCommandRepository _productCommandRepository;

        public RemoveProductImageCommandHandler(IProductQueryRepository productQueryRepository, IProductCommandRepository productCommandRepository)
        {
            _productQueryRepository = productQueryRepository;
            _productCommandRepository = productCommandRepository;
        }

        public async Task<RemoveProductImageCommandResponse> Handle(RemoveProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Product? product = await _productQueryRepository.Table.Include(p => p.ProductImageFiles)
                .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));

            Domain.Entities.ProductImageFile? productImageFile = product?.ProductImageFiles.FirstOrDefault(p => p.Id == Guid.Parse(request.ImageId));

            if (productImageFile != null)
                product?.ProductImageFiles.Remove(productImageFile);

            await _productCommandRepository.SaveAsync();
            return new();
        }
    }
}
