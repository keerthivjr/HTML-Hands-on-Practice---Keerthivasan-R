using Shared.DTOs;
using Shared.Models;
using ProductService.Repositories;

namespace ProductService.Services
{
    public class ProductManagementService  : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductManagementService (IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _productRepository.GetAllProducts();
        }

        public async Task<Product?> GetProductById(int id)
        {
            return await _productRepository.GetProductById(id);
        }

        public async Task<Product?> CreateProduct(ProductCreateDTO productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                StockQuantity = productDto.StockQuantity,
                Category = productDto.Category
            };

            return await _productRepository.CreateProduct(product);
        }

        public async Task<Product?> UpdateProduct(ProductUpdateDTO productDto)
        {
            var product = new Product
            {
                Id = productDto.Id,
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                StockQuantity = productDto.StockQuantity,
                Category = productDto.Category
            };

            return await _productRepository.UpdateProduct(product);
        }

        public async Task<bool> DeleteProduct(int id)
        {
            return await _productRepository.DeleteProduct(id);
        }

        public async Task<bool> UpdateStock(int productId, int quantity)
        {
            var product = await _productRepository.GetProductById(productId);
            if (product == null) return false;

            if (product.StockQuantity < quantity) return false;

            product.StockQuantity -= quantity;
            await _productRepository.UpdateProduct(product);
            return true;
        }
    }
}