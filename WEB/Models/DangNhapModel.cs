using System;
using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    public class DangNhapModel
    {
        public string MaNguoiDung { get; set; }
        public string TaiKhoan { get; set; }    

        public string HoTen { get; set; }

        public string SDT { get; set; }

        public string Khoa { get; set; }

        public int MaVaiTro { get; set; }

    }
}
