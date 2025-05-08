namespace API.Dto.request
{
    public class DuyetChiTietPhieuMuon
    {
        public int MaPhieuMuon { get; set; }
        public List<int> DanhSachMaCT { get; set; } = new();

        public String userId { get; set; }
    }

}
