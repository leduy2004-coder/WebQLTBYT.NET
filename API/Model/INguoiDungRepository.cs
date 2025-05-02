using API.Data;
using API.Dto.response;

namespace API.Model
{
    public interface INguoiDungRepository
    {
        Task<DangNhapResponse> DangNhap(string taiKhoan, string matKhau);
    }
}
