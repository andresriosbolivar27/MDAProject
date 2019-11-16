using System.Threading.Tasks;
using MDAProject.Common.Models;

namespace MDAProject.Common.Services
{
    public interface IApiService
    {
        Task<bool> CheckConnectionAsync(string url);
        Task<Response<DeviceResponse>> GetDeviceByWareHouseAsync(
            string urlBase,
            string servicePrefix, 
            string controller, 
            string tokenType, 
            string accessToken, 
            int codeWareHouse);
        Task<Response<TokenResponse>> GetTokenAsync(
            string urlBase, 
            string servicePrefix, 
            string controller, 
            TokenRequest request);
        Task<Response<WareHouseManagerResponse>> GetWareHoyseManagerByEmailAsync(
            string urlBase, 
            string servicePrefix, 
            string controller, 
            string tokenType, 
            string accessToken, 
            string email);
    }
}