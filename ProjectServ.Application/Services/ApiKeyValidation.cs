using Microsoft.Extensions.Configuration;
using ProjectServ.Application.Common;

namespace ProjectServ.Application.Services;

public class ApiKeyValidation : IApiKeyValidation
{
    private readonly IConfiguration _configuration;
    
    public ApiKeyValidation(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public bool IsValidApiKey(string userApiKey)
    {
        if (string.IsNullOrWhiteSpace(userApiKey))
            return false;
        
        string? apiKey = _configuration[Constants.ApiKeyName];
        
        return !(apiKey == null || apiKey != userApiKey);
    }
}