using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetXAngularPG.Application.Abstractions.Storage;
using NetXAngularPG.Application.Features.Commands.Product.CreateProduct;
using NetXAngularPG.Application.Features.Queries.Product.GetAllProduct;
using NetXAngularPG.Application.Repositories;
using NetXAngularPG.Application.RequestParameters;
using NetXAngularPG.Domain.Entities;
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


        readonly private IFileCommandRepository _fileCommandRepository;
        readonly private IFileQueryRepository _fileQueryRepository;
        readonly private IProductImageFileCommandRepository _productImageFileCommandRepository;
        readonly private IProductImageFileQueryRepository _productImageFileQueryRepository;
        readonly private IInvoiceFileQueryRepository _invoiceFileQueryRepository;
        readonly private IInvoiceFileCommandRepository _invoiceFileCommandRepository;
        readonly private IStorageService _storageService;
        readonly private IConfiguration _configuration;

        readonly IMediator _mediator;

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
            IStorageService storageService,
            IConfiguration configuration,
            IMediator mediator)
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
            _configuration = configuration;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _productQueryRepository.GetByIdAsync(id, false));
        }



        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
            CreateProductCommandResponse response = await _mediator.Send(createProductCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);
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





        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductImages(Guid id)
        {
            var product = await _productQueryRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(x => x.Id == id);



            return Ok(product?.ProductImageFiles.Select(x => new
            {
                Id = x.Id,
                Path = $"{_configuration["BaseStorageUrl"]}/{x.Path}",
                FileName = x.FileName
            }));
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteProductImage(Guid id, Guid imageId)
        {
            var product = await _productQueryRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(x => x.Id == id);

            ProductImageFile productImageFile = product.ProductImageFiles.FirstOrDefault(x => x.Id == imageId);

            product.ProductImageFiles.Remove(productImageFile);

            _productCommandRepository.SaveAsync();



            return Ok();
        }
    }
}
