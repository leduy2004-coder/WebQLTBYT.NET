


using Web.Api;
using WEB.Models;


namespace WEB.Api
{
    public class PhieuTraService
    {
        private readonly ApiService _apiService;
        public PhieuTraService(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<PhieuTra>> LayPhieuTra()
        {

            var response = await _apiService.GetDataAsync<List<PhieuTra>>("/api/PhieuTra/LayTatCaPhieuTra");

            if (response != null)
            {
                return response; 
            }

            return null;
        }
        public async Task<PhieuTra> LayPTTheoMa(int maPT)
        {
            string url = $"api/PhieuTra/LayPTTheoMa/{maPT}";

            var response = await _apiService.GetDataAsync<PhieuTra>(url);


            if (response != null)
            {
                return response;
            }

            return null;
        }
        public async Task<List<ChiTietPhieuTra>> LayCTPhieuTraTheoPT(int maPT)
        {
            string url = $"api/PhieuTra/LayCTPTTheoPhieuTra/{maPT}";

            var response = await _apiService.GetDataAsync<List<ChiTietPhieuTra>>(url);


            if (response != null)
            {
                return response;
            }

            return null;
        }
        public async Task<bool> DuyetPhieuTra(int maPT, string userId)
        {
            var data = new { maPT = maPT, userId = userId };
            var response = await _apiService.PostDataAsync<bool>("/api/PhieuTra/DuyetPhieuTra", data);
            return response;
        }
        public async Task<bool> XoaPhieuTra(int maPT)
        {

            string url = $"api/PhieuTra/{maPT}";
            bool deleteSuccess = await _apiService.DeleteDataAsync(url);

            return deleteSuccess;
        }

    }
}
