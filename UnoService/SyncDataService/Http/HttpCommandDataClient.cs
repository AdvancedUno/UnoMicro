using System.Text;
using System.Text.Json;
using UnoService.Dtos;
using UnoService.SyncDataService.HttpContext;

namespace UnoService.SyncDataService.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task SendUnotToCommand(UnoReadDto uno)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(uno),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync($"{_configuration["CommandService"]}", httpContent);

            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to CommandService was Okay");
            }
            else
            {
                Console.WriteLine("--> Sync POST to CommandService was NOT Okay");
            }

            
        }
    }
}