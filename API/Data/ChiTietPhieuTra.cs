using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Data
{
    public class ChiTietPhieuTra
    {
        [Key]
        public int MaCT { get; set; }

        [StringLength(20)]
        public string MaTB { get; set; }

        public int MaPhieuTra { get; set; }

        public int SoLuongTBTra { get; set; }

        [ForeignKey("MaTB")]
        public virtual ThietBi ThietBi { get; set; }

        [ForeignKey("MaPhieuTra")]
        public virtual PhieuTra PhieuTra { get; set; }
    }
}
