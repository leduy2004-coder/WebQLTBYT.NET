using API.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Model
{
    public interface IDanhMucTBRepository
    {
        Task<IEnumerable<DanhMucTB>> LayTatCa();
        Task<DanhMucTB> LayTheoMa(string maDanhMuc);
        Task<DanhMucTB> ThemDanhMuc(DanhMucTB danhMuc);
        Task<DanhMucTB> CapNhatDanhMuc(DanhMucTB danhMuc);
        Task<bool> XoaDanhMuc(string maDanhMuc);
    }
} 