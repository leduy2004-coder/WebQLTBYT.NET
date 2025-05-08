namespace WEB.Models.Request
{
    public class DuyetChiTietPhieuMuon
    {
        public int MaPhieuMuon { get; set; }
        public List<int> DanhSachMaCT { get; set; }

        public String userId { get; set; }
    }
}
