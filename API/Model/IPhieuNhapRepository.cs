

using API.Data;
using API.Dto.request;
using API.Dto.response;


namespace API.Model
{
    public interface IPhieuNhapRepository
    {
        Task<IEnumerable<PhieuNhap>> LayTatCa();
        Task<PhieuNhap> LayPNTheoMa(int maPN);
        Task<bool> XoaPhieuNhap(int maPN);
        Task<PhieuNhap> ThemPhieuNhap(PhieuNhapRequest phieuNhap);
    }
}
