namespace API.Dto.request
{
    public class ThemThietBiRequest
    {
        public string MaThietBi { get; set; }
        public string TenThietBi { get; set; }
        public string MaDanhMuc { get; set; }
        public string MaLoai { get; set; }
        public IFormFile? HinhAnh { get; set; }
        public string MoTa { get; set; }
        public int SoLuongTong { get; set; }
        public int SoLuongCon { get; set; }
    }

}
