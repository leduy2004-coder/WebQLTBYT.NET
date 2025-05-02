using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Data
{
    public class ThietBi
    {
        [Key]
        [StringLength(20)]
        public string MaThietBi { get; set; }

        [StringLength(100)]
        public string TenThietBi { get; set; }

        public int? SoLuongTong { get; set; }

        public int? SoLuongCon { get; set; }


        [StringLength(20)]
        public string MaDanhMuc { get; set; }

        [StringLength(1)]
        public string MaLoai { get; set; }

        [StringLength(20)]
        public byte[]? HinhAnh { get; set; }

        public string MoTa { get; set; }

        [ForeignKey("MaDanhMuc")]
        public virtual DanhMucTB? DanhMuc { get; set; }
        [ForeignKey("MaLoai")]
        public virtual LoaiTB Loai { get; set; }
    }
}
