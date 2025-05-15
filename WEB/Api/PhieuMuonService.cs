using Web.Api;
using WEB.Models;
using WEB.Models.Request;
using WEB.Models.Response;

namespace WEB.Api
{
    public class PhieuMuonService
    {
        private readonly ApiService _apiService;
        public PhieuMuonService(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<PhieuMuon>> LayPhieuMuon()
        {
            var response = await _apiService.GetDataAsync<List<PhieuMuon>>("/api/PhieuMuon/LayTatCaPhieuMuon");

            return response ?? new List<PhieuMuon>();
        }


        public async Task<PhieuMuon> LayPMTheoMa(int maPM)
        {
            string url = $"/api/PhieuMuon/LayPMTheoMa/{maPM}";
            var response = await _apiService.GetDataAsync<PhieuMuon>(url);

            return response;
        }


        public async Task<List<ChiTietPhieuMuon>> LayCTPhieuMuonTheoPM(int maPM)
        {
            string url = $"/api/PhieuMuon/LayCTPMTheoPhieuMuon/{maPM}";
            var response = await _apiService.GetDataAsync<List<ChiTietPhieuMuon>>(url);

            return response ?? new List<ChiTietPhieuMuon>();
        }


        public async Task<bool> DuyetChiTietPhieuMuon(DuyetChiTietPhieuMuon dto)
        {
            var response = await _apiService.PostDataAsync<object>("/api/PhieuMuon/DuyetCTPhieuMuon", dto);
            return response != null;
        }

        public async Task<PhieuMuon> ThemPhieuMuon(ThemPhieuMuonRequest request)
        {
            var response = await _apiService.PostDataAsync<PhieuMuon>("/api/PhieuMuon/ThemPhieuMuon", request);
            return response;
        }
    }
}
