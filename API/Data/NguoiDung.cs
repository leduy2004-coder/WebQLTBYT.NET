using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Data
{
    public class NguoiDung
    {
        [Key]
        [StringLength(20)]
        public string MaNguoiDung { get; set; }

        [StringLength(50)]
        public string TaiKhoan { get; set; }

        [StringLength(100)]
        public string MatKhau { get; set; }

        [StringLength(100)]
        public string HoTen { get; set; }

        [StringLength(15)]
        public string SDT { get; set; }

        [StringLength(100)]
        public string Khoa { get; set; }

        public bool TinhTrang { get; set; } = true;

        public int MaVaiTro { get; set; }

    }
}
