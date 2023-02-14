using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using VersioningAPI.DTO.V2;

namespace VersioningAPI.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public ProductController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [MapToApiVersion("2.0")]
        [HttpGet(Name = "GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            _httpClient.DefaultRequestHeaders.Clear();
            var response = await _httpClient.GetStreamAsync("https://fakestoreapi.com/products");
            var products = await JsonSerializer.DeserializeAsync<IEnumerable<Product>>(response);
            return Ok(products);

        }

    }
}
