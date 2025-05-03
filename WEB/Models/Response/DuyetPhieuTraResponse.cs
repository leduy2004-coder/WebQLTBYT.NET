

namespace WEB.Models.Response
{
    public class DuyetPhieuTraResponse
    {
        public List<PhieuTra> ApprovedPhieuTraList { get; set; }
        public List<PhieuTra> UnapprovedPhieuTraList { get; set; }
    }

    public class ChiTietPhieuTraResponse
    {
        public PhieuTra PhieuTra { get; set; }
        public List<ChiTietPhieuTra> ChiTietPhieuTraList { get; set; }
    }

}
