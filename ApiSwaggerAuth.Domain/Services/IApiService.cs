using System.Threading.Tasks;
using ApiSwaggerAuth.Domain.Entities;

namespace ApiSwaggerAuth.Domain.Services
{
    public interface IApiService
    {
        public Task<ApiData> GetAsync(string apiUrl);
    }
}