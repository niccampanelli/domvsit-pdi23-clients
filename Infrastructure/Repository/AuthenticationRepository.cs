﻿using Domain.Dto.Attendant;
using Domain.Repository;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly HttpClient _httpClient;

        public AuthenticationRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GenerateTokenForAttendantOutputDto> GenerateTokenForAttendant(GenerateTokenForAttendantInputDto input)
        {
            var jsonInput = JsonSerializer.Serialize(input);
            StringContent content = new StringContent(jsonInput, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/Authentication/GenerateTokenForAttendant", content);
            return await response.Content.ReadFromJsonAsync<GenerateTokenForAttendantOutputDto>();
        }

        public async Task<ExtractIdFromTokenOutputDto> ExtractIdFromToken(string token)
        {
            using var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/Authentication/ExtractIdFromToken");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.SendAsync(requestMessage);
            return await response.Content.ReadFromJsonAsync<ExtractIdFromTokenOutputDto>();
        }
    }
}
