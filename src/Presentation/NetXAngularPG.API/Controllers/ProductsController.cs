using Microsoft.AspNetCore.Mvc;
using NetXAngularPG.Application.Repositories;
using NetXAngularPG.Application.RequestParameters;
using NetXAngularPG.Domain.Entities;
using System.IO;
using System.IO.Pipes;
using System.Net;

namespace NetXAngularPG.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly private IProductCommandRepository _productCommandRepository;
        readonly private IProductQueryRepository _productQueryRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(
            IProductCommandRepository productCommandRepository,
            IProductQueryRepository productQueryRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _productCommandRepository = productCommandRepository;
            _productQueryRepository = productQueryRepository;
            this._webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination)
        {
            var totalCount = _productQueryRepository.GetAll(false).Count();
            var products = _productQueryRepository.GetAll(false).Skip(pagination.Page * pagination.Size).Take(pagination.Size).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreatedDate,
                p.UpdateDate
            }).ToList();

            return Ok(new
            {
                totalCount,
                products
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _productQueryRepository.GetByIdAsync(id, false));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productCommandRepository.RemoveAsync(id);
            await _productCommandRepository.SaveAsync();
            return Ok();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "resource/product-images");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            Random r = new();
            foreach (IFormFile file in Request.Form.Files)
            {
                string fullPath = Path.Combine(uploadPath, $"{r.Next()}{Path.GetExtension(file.FileName)}");

                using FileStream fileStream = new(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
            }
            return Ok();
        }

    }
}
