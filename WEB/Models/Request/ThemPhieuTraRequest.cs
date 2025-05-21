namespace WEB.Models.Request
{
    public class PhieuTraRequest
    {
        public int MaPhieuTra { get; set; }
        public string MaNguoiGui { get; set; }
        public string? MaNguoiDuyet { get; set; }
        public DateTime NgayTra { get; set; }
        public bool TinhTrang { get; set; }
    }

    public class ThemCTPhieuTraRequest
    {
        public string MaTB { get; set; }
        public int SoLuongTBTra { get; set; }

    }
}
