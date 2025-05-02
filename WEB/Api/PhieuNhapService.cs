


using Web.Api;
using WEB.Models;


namespace WEB.Api
{
    public class PhieuNhapService
    {
        private readonly ApiService _apiService;
        public PhieuNhapService(ApiService apiService)
        {
            _apiService = apiService;
        }
      
        public async Task<List<PhieuNhap>> LayPhieuNhap()
        {
            // Gửi yêu cầu POST đến API đăng nhập
            var response = await _apiService.GetDataAsync<List<PhieuNhap>>("/api/PhieuNhap/LayTatCaPhieuNhap");

            // Kiểm tra kết quả từ API
            if (response != null )
            {
                return response; // Đăng nhập thành công
            }

            return null;
        }
        public async Task<PhieuNhap> LayPNTheoMa(int maPN)
        {
            string url = $"api/PhieuNhap/LayPNTheoMa/{maPN}";

            var response = await _apiService.GetDataAsync<PhieuNhap>(url);


            if (response != null)
            {
                return response;
            }

            return null;
        }
        public async Task<List<ChiTietPhieuNhap>> LayCTPhieuNhapTheoPN(int maPN)
        {
            string url = $"api/PhieuNhap/LayCTPNTheoPhieuNhap/{maPN}";
    
            var response = await _apiService.GetDataAsync<List<ChiTietPhieuNhap>>(url);

     
            if (response != null)
            {
                return response; 
            }

            return null;
        }

        public async Task<PhieuNhap> LuuPhieuNhap(String userId)
        {
            PhieuNhap pn = new PhieuNhap();
            pn.NgayNhap = DateTime.Now;
            pn.MaNguoiDung = userId;
            return await _apiService.PostDataAsync<PhieuNhap>("api/PhieuNhap/ThemPhieuNhap", pn);
        }
        public async Task<ChiTietPhieuNhap> LuuCTPhieuNhap(ChiTietPhieuNhap chiTiet)
        {
            
            return await _apiService.PostDataAsync<ChiTietPhieuNhap>("api/PhieuNhap/ThemCTPhieuNhap", chiTiet);
        }

        public async Task<bool> XoaPhieuNhap(int maPN)
        {

            string url = $"api/PhieuNhap/{maPN}";
            bool deleteSuccess = await _apiService.DeleteDataAsync(url);

            return deleteSuccess;
        }

        public async Task<List<PhieuNhap>> TimPhieuNhap(int maPN)
        {
            string url = $"api/PhieuNhap/TimKiem/{maPN}"; ;

            return await _apiService.GetDataAsync<List<PhieuNhap>>(url);
        }
    }
}
