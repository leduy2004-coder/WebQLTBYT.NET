using Web.Api;
using WEB.Models;
using WEB.Models.Request;
using WEB.Models.Response;

namespace WEB.Api
{
    public class ThietBiService
    {
        private readonly ApiService _apiService;
        public ThietBiService(ApiService apiService)
        {
            _apiService = apiService;
        }
      
        public async Task<List<DanhMucTB>> LayDanhMuc()
        {
            // Gửi yêu cầu POST đến API đăng nhập
            var response = await _apiService.GetDataAsync<List<DanhMucTB>>("/api/thietBi/LayDanhMuc");

            // Kiểm tra kết quả từ API
            if (response != null )
            {
                return response; // Đăng nhập thành công
            }

            return null;
        }
        public async Task<List<ThietBi>> LayTatCaTB()
        {
            // Gửi yêu cầu POST đến API đăng nhập
            var response = await _apiService.GetDataAsync<List<ThietBi>>("/api/thietBi/LayTatCa");

            // Kiểm tra kết quả từ API
            if (response != null)
            {
                return response; // Đăng nhập thành công
            }

            return null;
        }
        public async Task<List<ThietBi>> LayTBTheoDM(String maDM)
        {
            string url = $"api/thietBi/LayTBTheoDanhMuc/{maDM}";
    
            var response = await _apiService.GetDataAsync<List<ThietBi>>(url);

     
            if (response != null)
            {
                return response; 
            }

            return null;
        }
        //Them Va Cap nhat TB
        public async Task<ThietBi> LuuThietBi(ThemThietBiRequest md, string apiEndpoint, HttpMethod method)
        {
            var formData = new MultipartFormDataContent();

            // Thêm các trường vào form
            AddModelFieldsToFormData(md, formData);

            // Gửi yêu cầu HTTP POST hoặc PUT đến API
            if (method == HttpMethod.Post)
                return await _apiService.PostDataFormAsync<ThietBi>(apiEndpoint, formData);
            else if (method == HttpMethod.Put)
                return await _apiService.PutDataFormAsync<ThietBi>(apiEndpoint, formData);

            return null;
        }

        private void AddModelFieldsToFormData(ThemThietBiRequest tb, MultipartFormDataContent formData)
        {
            formData.Add(new StringContent(tb.MaThietBi ?? string.Empty), "MaThietBi");
            formData.Add(new StringContent(tb.TenThietBi ?? string.Empty), "TenThietBi");
            formData.Add(new StringContent(tb.MaDanhMuc ?? string.Empty), "MaDanhMuc");
            formData.Add(new StringContent(tb.MaLoai ?? string.Empty), "MaLoai");
            formData.Add(new StringContent(tb.MoTa ?? string.Empty), "MoTa");
            formData.Add(new StringContent(tb.SoLuongTong.ToString()), "SoLuongTong");
            formData.Add(new StringContent(tb.SoLuongCon.ToString()), "SoLuongCon");

            if (tb.HinhAnh != null && tb.HinhAnh.Length > 0)
            {
                var streamContent = new StreamContent(tb.HinhAnh.OpenReadStream());
                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(tb.HinhAnh.ContentType);
                formData.Add(streamContent, "HinhAnh", tb.HinhAnh.FileName);
            }
        }

        public async Task<List<ThietBiDTO>> LayTatCaThietBi()
        {
            var response = await _apiService.GetDataAsync<List<ThietBiDTO>>("/api/thietBi/LayTatCa");
            return response ?? new List<ThietBiDTO>();
        }
    }
}
