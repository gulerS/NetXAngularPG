using MediatR;
using NetXAngularPG.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P = NetXAngularPG.Domain.Entities;

namespace NetXAngularPG.Application.Features.Queries.Product.GetByIdProduct
{
    internal class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {

        readonly IProductQueryRepository _productQueryRepository;
        public GetByIdProductQueryHandler(IProductQueryRepository productQueryRepository)
        {
            _productQueryRepository = productQueryRepository;
        }

        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
            P.Product product = await _productQueryRepository.GetByIdAsync(request.Id, false);
            return new()
            {
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock
            };
        }
    }
}
