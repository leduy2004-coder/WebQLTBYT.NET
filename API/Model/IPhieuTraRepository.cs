

using API.Data;
using API.Dto.request;
using API.Dto.response;


namespace API.Model
{
    public interface IPhieuTraRepository
    {
        Task<IEnumerable<PhieuTra>> LayTatCa();
        Task<PhieuTra> LayPTTheoMa(int maPT);

        Task<bool> XoaPhieuTra(int maPT);
        Task<bool> DuyetPhieuTra(DuyetPhieuTraRequest request);
    }
}
