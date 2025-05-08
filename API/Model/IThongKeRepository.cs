using API.Data;
using API.Data.ThongKe;
using API.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Model
{
    public interface IThongKeRepository
    {
        Task<ThongKeTongQuan> ThongKeTongQuan(int nam);
        Task<List<PhanBoTheoDanhMuc>> PhanBoTheoDanhMuc(int nam);
        Task<List<ThongKeMuonTraTheoThang>> ThongKeMuonTraTheoThang(int nam);
        Task<List<XuHuongNhapThietBiTheoThang>> XuHuongNhapThietBiTheoThang(int nam); 
        Task<List<ThietBiMuonNhieuNhat>> ThietBiMuonNhieuNhat(int nam, int top);
    }
}

