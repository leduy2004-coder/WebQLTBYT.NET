

using API.Data;
using API.Dto.request;
using API.Dto.response;


namespace API.Model
{
    public interface ICTPhieuMuonRepository
    {
        Task<IEnumerable<ChiTietPhieuMuon>> LayCTPMTheoMaPM(int maPM);

        Task<IEnumerable<ChiTietPhieuMuon>> LayCTPMTheoTT(int TT);
        Task<bool> DuyetCTPhieuMuon(DuyetChiTietPhieuMuon dto);
    }
}
