using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace FakeWebStore.Services
{
    public interface IDashboardService
    {
        Task<DashboardCountsDto> GetDashboardCountsAsync();
        Task<List<ProductItemDto>> GetCarouselProductsAsync(int limit = 4);
        Task<List<UserTableDto>> GetUsersTableAsync(int limit = 10);
    }

    public class DashboardService : IDashboardService
    {
        private readonly HttpClient _httpClient;

        public DashboardService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DashboardCountsDto> GetDashboardCountsAsync()
        {
            try
            {
                var productTask = _httpClient.GetFromJsonAsync<DummyJsonResponse<object>>("https://dummyjson.com/products?limit=1");
                var userTask = _httpClient.GetFromJsonAsync<DummyJsonResponse<object>>("https://dummyjson.com/users?limit=1");
                var cartTask = _httpClient.GetFromJsonAsync<DummyJsonResponse<object>>("https://dummyjson.com/carts?limit=1");

                await Task.WhenAll(productTask, userTask, cartTask);

                return new DashboardCountsDto
                {
                    TotalProducts = productTask.Result?.Total ?? 0,
                    TotalUsers = userTask.Result?.Total ?? 0,
                    TotalReceipts = cartTask.Result?.Total ?? 0
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching dashboard counts: {ex.Message}");
                return new DashboardCountsDto();
            }
        }

        public async Task<List<ProductItemDto>> GetCarouselProductsAsync(int limit = 4)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<DummyProductResponse>($"https://dummyjson.com/products?limit={limit}");
                return response?.Products ?? new List<ProductItemDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching products for carousel: {ex.Message}");
                return new List<ProductItemDto>();
            }
        }

        public async Task<List<UserTableDto>> GetUsersTableAsync(int limit = 10)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<DummyUserResponse>($"https://dummyjson.com/users?limit={limit}");
                return response?.Users ?? new List<UserTableDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching users for table: {ex.Message}");
                return new List<UserTableDto>();
            }
        }
    }

    // --- DTO Models & Wrappers ---

    public class DummyJsonResponse<T>
    {
        [JsonPropertyName("total")]
        public int Total { get; set; }
    }

    public class DashboardCountsDto
    {
        public int TotalProducts { get; set; }
        public int TotalUsers { get; set; }
        public int TotalReceipts { get; set; }
    }

    public class DummyProductResponse
    {
        [JsonPropertyName("products")]
        public List<ProductItemDto> Products { get; set; } = new();
    }

    public class ProductItemDto
    {
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("thumbnail")]
        public string ImageUrl { get; set; } = string.Empty;
    }

    public class DummyUserResponse
    {
        [JsonPropertyName("users")]
        public List<UserTableDto> Users { get; set; } = new();
    }

    public class UserTableDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; } = string.Empty;

        [JsonPropertyName("lastName")]
        public string LastName { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("phone")]
        public string Phone { get; set; } = string.Empty;

        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;
    }
}