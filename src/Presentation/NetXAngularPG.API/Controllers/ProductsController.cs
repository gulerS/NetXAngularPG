using Microsoft.AspNetCore.Mvc;
using NetXAngularPG.Application.Abstractions.Storage;
using NetXAngularPG.Application.Repositories;
using NetXAngularPG.Application.RequestParameters;
using NetXAngularPG.Domain.Entities;

namespace NetXAngularPG.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        readonly private IProductCommandRepository _productCommandRepository;
        readonly private IProductQueryRepository _productQueryRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;


        readonly private IFileCommandRepository _fileCommandRepository;
        readonly private IFileQueryRepository _fileQueryRepository;
        readonly private IProductImageFileCommandRepository _productImageFileCommandRepository;
        readonly private IProductImageFileQueryRepository _productImageFileQueryRepository;
        readonly private IInvoiceFileQueryRepository _invoiceFileQueryRepository;
        readonly private IInvoiceFileCommandRepository _invoiceFileCommandRepository;
        readonly private IStorageService _storageService;

        public ProductController(
            IProductCommandRepository productCommandRepository,
            IProductQueryRepository productQueryRepository,
            IWebHostEnvironment webHostEnvironment,

            IFileCommandRepository fileCommandRepository,
            IFileQueryRepository fileQueryRepository,
            IProductImageFileCommandRepository productImageFileCommandRepository,
            IProductImageFileQueryRepository productImageFileQueryRepository,
            IInvoiceFileQueryRepository invoiceFileQueryRepository,
            IInvoiceFileCommandRepository invoiceFileCommandRepository,
            IStorageService storageService)
        {
            _productCommandRepository = productCommandRepository;
            _productQueryRepository = productQueryRepository;
            _webHostEnvironment = webHostEnvironment;

            _fileCommandRepository = fileCommandRepository;
            _fileQueryRepository = fileQueryRepository;
            _productImageFileCommandRepository = productImageFileCommandRepository;
            _productImageFileQueryRepository = productImageFileQueryRepository;
            _invoiceFileQueryRepository = invoiceFileQueryRepository;
            _invoiceFileCommandRepository = invoiceFileCommandRepository;
            _storageService = storageService;
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
        public async Task<IActionResult> Upload(string id)
        {
            List<(string fileName, string pathOrContainerName)> result = await _storageService.UploadAsync("photo-images", Request.Form.Files);


            Product product = await _productQueryRepository.GetByIdAsync(id);

            //foreach (var r in result)
            //{
            //    product.ProductImageFiles.Add(new()
            //    {
            //        FileName = r.fileName,
            //        Path = r.pathOrContainerName,
            //        Storage = _storageService.StorageName,
            //        Products = new List<Product>() { product }
            //    });
            //}

            await _productImageFileCommandRepository.AddRangeAsync(result.Select(r => new ProductImageFile
            {
                FileName = r.fileName,
                Path = r.pathOrContainerName,
                Storage = _storageService.StorageName,
                Products = new List<Product>() { product }
            }).ToList());

            await _productImageFileCommandRepository.SaveAsync();

            return Ok();
        }

    }
}
