using Microsoft.AspNetCore.Mvc;
using NetXAngularPG.Application.Repositories;

namespace NetXAngularPG.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
   
    {
        readonly private IProductCommandRepository _productCommandRepository;
        readonly private IProductQueryRepository _productQueryRepository;

        public ProductController(IProductCommandRepository productCommandRepository, IProductQueryRepository productQueryRepository)
        {
            _productCommandRepository = productCommandRepository;
            _productQueryRepository = productQueryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response =  _productQueryRepository.GetAll().ToList();

            return Ok(response);
        }
    }
}
