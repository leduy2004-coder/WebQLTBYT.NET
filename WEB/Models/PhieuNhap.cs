
using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    public class PhieuNhap
    {
        public int MaPhieuNhap { get; set; }

        public DateTime NgayNhap { get; set; }

        public string MaNguoiDung { get; set; }

        public NguoiDung NguoiDung { get; set; }
    }
    public class NguoiDung
    {
        public string MaNguoiDung { get; set; }


        public string TaiKhoan { get; set; }

        public string MatKhau { get; set; }

        public string HoTen { get; set; }
        public string SDT { get; set; }

        public string Khoa { get; set; }

        public bool TinhTrang { get; set; } = true;

        public int MaVaiTro { get; set; }

    }
}
