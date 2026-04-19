using System.Text.Json;
using Shared.Models;

namespace OrderService.Services
{
    public class ProductServiceClient : IProductServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProductServiceClient> _logger;

        // Create a static readonly instance to be reused
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public ProductServiceClient(HttpClient httpClient, ILogger<ProductServiceClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<Product?> GetProductById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"http://localhost:5002/api/products/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    // Reuse the static instance instead of creating a new one
                    var apiResponse = JsonSerializer.Deserialize<ApiResponse<Product>>(json, _jsonOptions);
                    return apiResponse?.Data;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling Product Service");
                return null;
            }
        }

        public async Task<bool> UpdateStock(int productId, int quantity)
        {
            try
            {
                // Call Product Service to update stock
                var response = await _httpClient.PutAsync($"http://localhost:5002/api/products/{productId}/stock?quantity={quantity}", null);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating stock");
                return false;
            }
        }
    }
}