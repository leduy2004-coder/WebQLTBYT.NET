using Web.Api;
using WEB.Models;
using WEB.Models.Request;
using WEB.Models.Response;

namespace WEB.Api
{
    public class DanhMucTBService
    {
        private readonly ApiService _apiService;
        public DanhMucTBService(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<DanhMucTB>> LayTatCa()
        {
            var response = await _apiService.GetDataAsync<List<DanhMucTB>>("/api/DanhMucTB");
            return response ?? new List<DanhMucTB>();
        }

        public async Task<DanhMucTB> LayTheoMa(string maDanhMuc)
        {
            string url = $"/api/DanhMucTB/{maDanhMuc}";
            var response = await _apiService.GetDataAsync<DanhMucTB>(url);
            return response;
        }

        public async Task<DanhMucTB> ThemDanhMuc(DanhMucTBRequest request)
        {
            var response = await _apiService.PostDataAsync<DanhMucTB>("/api/DanhMucTB", request);
            return response;
        }

        public async Task<DanhMucTB> CapNhatDanhMuc(string maDanhMuc, DanhMucTBRequest request)
        {
            string url = $"/api/DanhMucTB/{maDanhMuc}";
            var response = await _apiService.PutDataAsync<DanhMucTB>(url, request);
            return response;
        }

        public async Task<bool> XoaDanhMuc(string maDanhMuc)
        {
            string url = $"/api/DanhMucTB/{maDanhMuc}";
            var response = await _apiService.DeleteDataAsync(url);
            return response;
        }
    }
} 