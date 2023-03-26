using Microsoft.AspNetCore.Mvc;
using NetXAngularPG.Application.Repositories;
using NetXAngularPG.Application.RequestParameters;
using NetXAngularPG.Application.Services;
using NetXAngularPG.Domain.Entities;
using System.IO;
using System.IO.Pipes;
using System.Net;

namespace NetXAngularPG.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        readonly private IProductCommandRepository _productCommandRepository;
        readonly private IProductQueryRepository _productQueryRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _fileService;

        readonly private IFileCommandRepository _fileCommandRepository;
        readonly private IFileQueryRepository _fileQueryRepository;
        readonly private IProductImageFileCommandRepository _productImageFileCommandRepository;
        readonly private IProductImageFileQueryRepository _productImageFileQueryRepository;
        readonly private IInvoiceFileQueryRepository _invoiceFileQueryRepository;
        readonly private IInvoiceFileCommandRepository _invoiceFileCommandRepository;

        public ProductController(
            IProductCommandRepository productCommandRepository,
            IProductQueryRepository productQueryRepository,
            IWebHostEnvironment webHostEnvironment,
            IFileService fileService,
            IFileCommandRepository fileCommandRepository,
            IFileQueryRepository fileQueryRepository,
            IProductImageFileCommandRepository productImageFileCommandRepository,
            IProductImageFileQueryRepository productImageFileQueryRepository,
            IInvoiceFileQueryRepository invoiceFileQueryRepository,
            IInvoiceFileCommandRepository invoiceFileCommandRepository)
        {
            _productCommandRepository = productCommandRepository;
            _productQueryRepository = productQueryRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
            _fileCommandRepository = fileCommandRepository;
            _fileQueryRepository = fileQueryRepository;
            _productImageFileCommandRepository = productImageFileCommandRepository;
            _productImageFileQueryRepository = productImageFileQueryRepository;
            _invoiceFileQueryRepository = invoiceFileQueryRepository;
            _invoiceFileCommandRepository = invoiceFileCommandRepository;
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
           var result = await _fileService.UploadAsync("resources/product", Request.Form.Files);


           await _productImageFileCommandRepository.AddRangeAsync(result.Select(x=> new ProductImageFile() {         
            FileName=x.fileName,
            Path = x.path,
            }).ToList());

            await _productImageFileCommandRepository.SaveAsync();


            return Ok(result);
        }

    }
}
