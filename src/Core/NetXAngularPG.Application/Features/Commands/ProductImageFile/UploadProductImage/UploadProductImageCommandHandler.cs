using MediatR;
using NetXAngularPG.Application.Abstractions.Storage;
using NetXAngularPG.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetXAngularPG.Application.Features.Commands.ProductImageFile.UploadProductImage
{
    public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse>
    {
        readonly IStorageService _storageService;
        readonly IProductQueryRepository _productQueryRepository;
        readonly IProductImageFileCommandRepository _productImageFileCommandRepository;

        public UploadProductImageCommandHandler(IStorageService storageService, IProductQueryRepository productQueryRepository, IProductImageFileCommandRepository productImageFileCommandRepository)
        {
            _storageService = storageService;
            _productQueryRepository = productQueryRepository;
            _productImageFileCommandRepository = productImageFileCommandRepository;
        }

        public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            List<(string fileName, string pathOrContainerName)> result = await _storageService.UploadAsync("photo-images", request.Files);


            Domain.Entities.Product product = await _productQueryRepository.GetByIdAsync(request.Id);


            await _productImageFileCommandRepository.AddRangeAsync(result.Select(r => new Domain.Entities.ProductImageFile
            {
                FileName = r.fileName,
                Path = r.pathOrContainerName,
                Storage = _storageService.StorageName,
                Products = new List<Domain.Entities.Product>() { product }
            }).ToList());

            await _productImageFileCommandRepository.SaveAsync();

            return new();
        }
    }
}
