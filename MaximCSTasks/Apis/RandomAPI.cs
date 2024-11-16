using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace MaximCSTasks.Apis;

public class RandomAPI
{
    private static readonly HttpClient _httpClient = new HttpClient();
    
    public RandomAPI()
    {
        _httpClient.BaseAddress = new Uri("http://www.randomnumberapi.com/api/v1.0/");
    }

    public async Task<int> GetAsyncRandomNumber(int min, int max)
    {
        var rsp = await _httpClient.GetAsync($"random?min={min}&max={max}&count=1");
        rsp.EnsureSuccessStatusCode();
        var stringRsp = await rsp.Content.ReadAsStringAsync();
        var jsonRsp = JsonSerializer.Deserialize<List<int>>(stringRsp);
        
        if (jsonRsp == null || jsonRsp.Count < 1)
        {
            throw new ApplicationException("Invalid JSON");
        }
        return jsonRsp[0];
    }
}