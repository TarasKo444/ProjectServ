using Microsoft.AspNetCore.Mvc;

namespace ProjectServ.WebApi.Filters
{
    public class ApiKeyAttribute : ServiceFilterAttribute
    {
        public ApiKeyAttribute()
            : base(typeof(ApiKeyAuthFilter))
        {
        }
    }
}