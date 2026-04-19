using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Models;
using ProductService.Services;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<IEnumerable<Product>>>> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();
            return Ok(ApiResponse<IEnumerable<Product>>.SuccessResponse(products));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<Product>>> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound(ApiResponse<Product>.ErrorResponse("Product not found"));
            }
            return Ok(ApiResponse<Product>.SuccessResponse(product));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<Product>>> CreateProduct([FromBody] ProductCreateDTO productDto)
        {
            var product = await _productService.CreateProduct(productDto);
            if (product == null)
            {
                return BadRequest(ApiResponse<Product>.ErrorResponse("Failed to create product"));
            }
            return Ok(ApiResponse<Product>.SuccessResponse(product, "Product created successfully"));
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<Product>>> UpdateProduct([FromBody] ProductUpdateDTO productDto)
        {
            var product = await _productService.UpdateProduct(productDto);
            if (product == null)
            {
                return NotFound(ApiResponse<Product>.ErrorResponse("Product not found"));
            }
            return Ok(ApiResponse<Product>.SuccessResponse(product, "Product updated successfully"));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProduct(id);
            if (!result)
            {
                return NotFound(ApiResponse<bool>.ErrorResponse("Product not found"));
            }
            return Ok(ApiResponse<bool>.SuccessResponse(true, "Product deleted successfully"));
        }

        [HttpPut("{id}/stock")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateStock(int id, [FromQuery] int quantity)
        {
            var result = await _productService.UpdateStock(id, quantity);
            if (!result)
            {
                return BadRequest(ApiResponse<bool>.ErrorResponse("Failed to update stock. Product not found or insufficient stock."));
            }
            return Ok(ApiResponse<bool>.SuccessResponse(true, "Stock updated"));
        }
    }
}