using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Data
{
    public class ChiTietPhieuMuon
    {
        [Key]
        public int MaCT { get; set; }

        [StringLength(20)]
        public string MaTB { get; set; }

        public bool TinhCanThiet { get; set; } // 1: Khẩn cấp, 0: Bình thường

        public string MucDich { get; set; }

        public DateTime NgayMuon { get; set; }

        public DateTime NgayDuKienTra { get; set; }

        public int MaPhieuMuon { get; set; }

        public int SoLuongTBMuon { get; set; }

        public int TinhTrang { get; set; } // 1: Chưa duyệt, 2: Chấp nhận, 3: Từ chối

        [ForeignKey("MaTB")]
        public virtual ThietBi ThietBi { get; set; }

        [JsonIgnore]
        [ForeignKey("MaPhieuMuon")]
        public virtual PhieuMuon PhieuMuon { get; set; }
    }
}
