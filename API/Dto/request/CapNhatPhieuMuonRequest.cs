using System.ComponentModel.DataAnnotations;

namespace API.Dto.request
{
    public class CapNhatPhieuMuonRequest
    {
        [Required]
        public int MaPhieuMuon { get; set; }
        
        [Required]
        [StringLength(20)]
        public string MaNguoiGui { get; set; }

        [Required]
        public List<ChiTietPhieuMuonRequest> ChiTietPhieuMuons { get; set; }
    }
} 