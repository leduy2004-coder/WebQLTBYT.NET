

using Microsoft.AspNetCore.Mvc;
using Web.Api;
using WEB.Models;
using WEB.Models.Request;

namespace WEB.Api
{
    public class DangNhapService
    {
        private readonly ApiService _apiService;
        public DangNhapService(ApiService apiService)
        {
            _apiService = apiService;
        }
      
        public async Task<DangNhapModel> LoginAsync(DangNhapRequest loginRequest)
        {
            // Gửi yêu cầu POST đến API đăng nhập
            var response = await _apiService.PostDataAsync<DangNhapModel>("/api/NguoiDung/DangNhap", loginRequest);

            // Kiểm tra kết quả từ API
            if (response != null )
            {
                return response; // Đăng nhập thành công
            }

            return null;
        }

      
    }
}
