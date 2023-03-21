using Microsoft.AspNetCore.Mvc;
using NetXAngularPG.Application.Repositories;
using NetXAngularPG.Application.RequestParameters;

namespace NetXAngularPG.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase

    {
        readonly private IProductCommandRepository _productCommandRepository;
        readonly private IProductQueryRepository _productQueryRepository;

        public ProductsController(IProductCommandRepository productCommandRepository, IProductQueryRepository productQueryRepository)
        {
            _productCommandRepository = productCommandRepository;
            _productQueryRepository = productQueryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination)
        {
            var totalCount = _productQueryRepository.GetAll(false).Count();
            var products = _productQueryRepository.GetAll(false).Skip(pagination.Page*pagination.Size).Take(pagination.Size).Select(x => new
            {
                x.Id,
                x.Name,
                x.Description,
                x.Stock,
                x.Price,
                x.CreatedDate,
                x.UpdateDate
            }).ToList();

            return Ok(new {
            totalCount,
            products
            });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
          
            return Ok();
        }
    }
}
