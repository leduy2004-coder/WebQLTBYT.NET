

namespace WEB.Models
{
    public class ChiTietPhieuNhap
    {
    
        public int MaCT { get; set; }

        public string MaThietBi { get; set; }


        public int SoLuong { get; set; }

        public int MaPhieuNhap { get; set; }

        public ThietBi ThietBi { get; set; }
    }
}
