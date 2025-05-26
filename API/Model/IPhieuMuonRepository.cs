using API.Data;
using API.Dto.request;
using API.Dto.response;

namespace API.Model
{
    public interface IPhieuMuonRepository
    {
        Task<IEnumerable<PhieuMuon>> LayTatCa();
        Task<PhieuMuon?> LayPMTheoMa(int maPM);
        Task<PhieuMuon> ThemPhieuMuon(ThemPhieuMuonRequest request);
        Task<bool> XoaPhieuMuon(int maPM);
        Task<bool> CapNhatPhieuMuon(int maPM, CapNhatPhieuMuonRequest request);
        Task<IEnumerable<ChiTietPhieuMuon>> LayTatCaChiTietPhieuMuon();
    }
}
