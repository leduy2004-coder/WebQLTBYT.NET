

using API.Data;
using API.Dto.request;
using API.Dto.response;


namespace API.Model
{
    public interface ICTPhieuNhapRepository
    {
        Task<IEnumerable<ChiTietPhieuNhap>> LayTatCa();

        Task<IEnumerable<ChiTietPhieuNhap>> LayCTPNTheoPN(int maPN);

        Task<ChiTietPhieuNhap> ThemCTPhieuNhap(CTPhieuNhapRequest chiTietPhieuNhap);
    }
}
