using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Data
{
    public class PhieuNhap
    {
        [Key]
        public int MaPhieuNhap { get; set; }

        public DateTime NgayNhap { get; set; }

        [StringLength(20)]
        public string MaNguoiDung { get; set; }

        [ForeignKey("MaNguoiDung")]
        public virtual NguoiDung NguoiDung { get; set; }

        public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }
    }
}
