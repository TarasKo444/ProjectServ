namespace ProjectServ.Application.Services;

public interface IApiKeyValidation
{
    bool IsValidApiKey(string userApiKey);
}