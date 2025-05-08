

namespace WEB.Models.Response
{
    public class DuyetPhieuMuonResponse
    {
        public List<PhieuMuon> PhieuMuonDaDuyetList { get; set; }
        public List<PhieuMuon> PhieuMuonChuaDuyetList { get; set; }
    }

    public class ChiTietPhieuMuonResponse
    {
        public PhieuMuon PhieuMuon { get; set; }
        public List<ChiTietPhieuMuon> ChiTietPhieuMuonList { get; set; }
    }

}
