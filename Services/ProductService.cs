using FakeWebStore.Models;
using System.Net.Http.Json;

namespace FakeWebStore.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ProductResponse>(
                    "https://dummyjson.com/products"
                );

                return response?.Products ?? new List<Product>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading products: {ex.Message}");

                return new List<Product>();
            }
        }
    }
}