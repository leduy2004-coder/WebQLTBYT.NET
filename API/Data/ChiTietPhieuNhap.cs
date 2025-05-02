using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Data
{
    public class ChiTietPhieuNhap
    {
        [Key]
        public int MaCT { get; set; }

        [StringLength(20)]
        public string MaThietBi { get; set; }

        public int SoLuong { get; set; }

        public int MaPhieuNhap { get; set; }

        [ForeignKey("MaThietBi")]
        public virtual ThietBi ThietBi { get; set; }

        [ForeignKey("MaPhieuNhap")]
        public virtual PhieuNhap PhieuNhap { get; set; }
    }
}
