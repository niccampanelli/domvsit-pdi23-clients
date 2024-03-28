using Domain.Dto.Event;
using Domain.Repository;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly HttpClient _httpClient;

        public EventRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeleteEventByParams(DeleteEventByParamsInputDto input)
        {
            var jsonInput = JsonSerializer.Serialize(input);
            StringContent content = new StringContent(jsonInput, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync("/Event/Delete", content);
        }
    }
}
