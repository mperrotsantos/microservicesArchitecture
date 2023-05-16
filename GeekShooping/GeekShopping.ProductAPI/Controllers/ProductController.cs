using GeekShopping.ProductAPI.Data.Dto;
using GeekShopping.ProductAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }
        [HttpGet]
        public async Task<ActionResult<ProductDto>> FindAll()
        {
            var products = await _productRepository.FindAll();
            return Ok(products);
        }
        [HttpGet("id")]
        public async Task<ActionResult<ProductDto>> FindById(long id)
        {
            var product = await _productRepository.FindById(id);
            if (product == null) 
                return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>>Create(ProductDto productDto)
        {
            if (productDto == null)
                return BadRequest();
            var product = await _productRepository.Create(productDto);
            return Created("", product);
        }
        [HttpPut]
        public async Task<ActionResult<ProductDto>> Update(ProductDto productDto)
        {
            if (productDto == null)
                return BadRequest();
            var product = await _productRepository.Update(productDto);
            return Ok(product);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> Delete(long id)
        {
            var status = await _productRepository.DeleteById(id);
            if (!status)
                return BadRequest();
            return Ok(status);
        }
    }
}
