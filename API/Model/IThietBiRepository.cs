

using API.Data;
using API.Dto.request;
using API.Dto.response;


namespace API.Model
{
    public interface IThietBiRepository
    {
        Task<IEnumerable<ThietBi>> LayTatCa();
        Task<IEnumerable<DanhMucTB>> LayDanhMuc();
        Task<IEnumerable<ThietBi>> LayTBTheoDanhMuc(String maDM);

        Task<ThemThietBiResponse> ThemThietBi(ThemThietBiRequest tb);
    }
}
