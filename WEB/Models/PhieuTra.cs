

namespace WEB.Models
{
    public class PhieuTra
    {

        public int MaPhieuTra { get; set; }

        public string MaNguoiGui { get; set; }

        public string? MaNguoiDuyet { get; set; }

        public DateTime NgayTra { get; set; }

        public bool TinhTrang { get; set; } = false; // 0: Không duyệt, 1: Đã duyệt

        public  NguoiDung? NguoiGui { get; set; }

    
        public  NguoiDung? NguoiDuyet { get; set; }
    }

   
}
