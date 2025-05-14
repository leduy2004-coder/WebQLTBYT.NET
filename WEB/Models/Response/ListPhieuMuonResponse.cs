namespace WEB.Models.Response
{
    public class ListPhieuMuonTraResponse
    {
        public List<ChiTietPhieuMuon> ChiTietPhieuMuons { get; set; }
        public List<PhieuTra> ListPTDaDuyet { get; set; }
        public List<PhieuTra> ListPTChuaDuyet { get; set; }
        
    }   
}
