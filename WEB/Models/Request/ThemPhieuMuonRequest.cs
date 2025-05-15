using System.ComponentModel.DataAnnotations;

namespace WEB.Models.Request
{
    public class ThemPhieuMuonRequest
    {
        public string MaNguoiGui { get; set; }
        public List<ChiTietPhieuMuonRequest> ChiTietPhieuMuons { get; set; } = new();
    }

    public class ChiTietPhieuMuonRequest
    {
        [Required(ErrorMessage = "Vui lòng chọn thiết bị")]
        public string MaTB { get; set; }

        public bool TinhCanThiet { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mục đích mượn")]
        public string MucDich { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày mượn")]
        public DateTime NgayMuon { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày dự kiến trả")]
        public DateTime NgayDuKienTra { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng mượn")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng mượn phải lớn hơn 0")]
        public int SoLuongTBMuon { get; set; }
    }
} 