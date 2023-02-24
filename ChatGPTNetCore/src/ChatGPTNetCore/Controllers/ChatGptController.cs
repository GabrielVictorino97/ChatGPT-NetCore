using ChatGPTNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ChatGPTNetCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatGptController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ChatGptController> _logger;

        public ChatGptController(ILogger<ChatGptController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }


        [HttpGet]
        public async Task<IActionResult> Get(string text, [FromServices] IConfiguration configuration)
        {
            var token = configuration.GetValue<string>("ChatGptSecretKey");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var model = new ChatGptRequest(text);

            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://api.openai.com/v1/completions", content);

            var result = await response.Content.ReadFromJsonAsync<ChatGptResponse>();

            return Ok(result.choices.First().text.Replace("\n", "").Replace("\t", ""));

        }
    }
}