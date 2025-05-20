using System.ComponentModel.DataAnnotations;

namespace WEB.Models.Request
{
    public class CapNhatPhieuMuonRequest
    {
        [Required(ErrorMessage = "Mã người gửi không được để trống")]
        public string MaNguoiGui { get; set; }

        [Required(ErrorMessage = "Phải có ít nhất một thiết bị được mượn")]
        [MinLength(1, ErrorMessage = "Phải có ít nhất một thiết bị được mượn")]
        public List<ChiTietPhieuMuonRequest> ChiTietPhieuMuons { get; set; } = new();
    }
} 