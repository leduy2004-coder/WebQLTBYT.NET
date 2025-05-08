using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB.Models.Response
{
    public class ChiTietPhieuMuon
    {
        public int MaCT { get; set; }

        public string MaTB { get; set; }

        public bool TinhCanThiet { get; set; } // 1: Khẩn cấp, 0: Bình thường

        public string MucDich { get; set; }

        public DateTime NgayMuon { get; set; }

        public DateTime NgayDuKienTra { get; set; }

        public int MaPhieuMuon { get; set; }

        public int SoLuongTBMuon { get; set; }

        public int TinhTrang { get; set; } // 1: Chưa duyệt, 2: Chấp nhận, 3: Từ chối

        public ThietBiDTO ThietBi { get; set; }
    }

    public class ThietBiDTO
    {
        public string MaThietBi { get; set; }

        public string TenThietBi { get; set; }

        public int? SoLuongTong { get; set; }

        public int? SoLuongCon { get; set; }


        public string MaDanhMuc { get; set; }

        public string MaLoai { get; set; }

        public byte[]? HinhAnh { get; set; }

        public string MoTa { get; set; }
    }
}
