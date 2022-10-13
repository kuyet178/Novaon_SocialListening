using AioCore.Domain.Common.Constants;
using Microsoft.AspNetCore.Http;

namespace AioCore.Services.CommonServices
{
    public interface IClientService
    {
        string GetClient();
    }

    public class ClientService : IClientService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClientService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetClient()
        {
            return _httpContextAccessor.HttpContext?.Request.Headers[RequestHeaders.UserClient] ?? string.Empty;
        }
    }
}