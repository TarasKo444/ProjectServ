using Microsoft.AspNetCore.Mvc.Filters;
using ProjectServ.Application.Common;
using ProjectServ.Application.Services;

namespace ProjectServ.WebApi.Filters
{
    public class ApiKeyAuthFilter : IAuthorizationFilter
    {
        private readonly IApiKeyValidation _apiKeyValidation;
        public ApiKeyAuthFilter(IApiKeyValidation apiKeyValidation)
        {
            _apiKeyValidation = apiKeyValidation;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string userApiKey = context.HttpContext.Request.Headers[Constants.ApiKeyHeaderName].ToString();

            if (string.IsNullOrWhiteSpace(userApiKey) || !_apiKeyValidation.IsValidApiKey(userApiKey))
                throw new UserFriendlyException(401, "Wrong key");
        }
    }
}