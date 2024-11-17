using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace MaximCSTasks.Apis;

public class RandomAPI
{
    private static readonly HttpClient _httpClient = new HttpClient();
    private readonly IConfiguration _configuration;
    
    public RandomAPI(IConfiguration configuration)
    {
        _configuration = configuration;
        _httpClient.BaseAddress = new Uri(configuration["AppSettings:RandomAPI:URL"]);
    }

    public async Task<int> GetAsyncRandomNumber(int min, int max)
    {
        var rsp = await _httpClient.GetAsync($"{_configuration["AppSettings:RandomAPI:Path"]}?min={min}&max={max}&count=1");
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