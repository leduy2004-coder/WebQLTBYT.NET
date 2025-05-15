namespace API.Dto.request
{
    public class ThemPhieuMuonRequest
    {
        public string MaNguoiGui { get; set; }
        public List<ChiTietPhieuMuonRequest> ChiTietPhieuMuons { get; set; } = new();
    }

    public class ChiTietPhieuMuonRequest
    {
        public string MaTB { get; set; }
        public bool TinhCanThiet { get; set; }
        public string MucDich { get; set; }
        public DateTime NgayMuon { get; set; }
        public DateTime NgayDuKienTra { get; set; }
        public int SoLuongTBMuon { get; set; }
    }
} 