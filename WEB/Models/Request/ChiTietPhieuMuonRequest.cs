using System.ComponentModel.DataAnnotations;

namespace WEB.Models.Request
{
    public class ChiTietPhieuMuonRequest : IValidatableObject
    {
        [Required(ErrorMessage = "Vui lòng chọn thiết bị")]
        public string MaTB { get; set; }

        public bool TinhCanThiet { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mục đích mượn")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Mục đích mượn phải từ 10 đến 500 ký tự")]
        public string MucDich { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày mượn")]
        public DateTime NgayMuon { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày dự kiến trả")]
        public DateTime NgayDuKienTra { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng mượn")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng mượn phải lớn hơn 0")]
        public int SoLuongTBMuon { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (NgayMuon < DateTime.Today)
            {
                yield return new ValidationResult("Ngày mượn không thể là ngày trong quá khứ", new[] { nameof(NgayMuon) });
            }

            if (NgayDuKienTra <= NgayMuon)
            {
                yield return new ValidationResult("Ngày dự kiến trả phải sau ngày mượn", new[] { nameof(NgayDuKienTra) });
            }
        }
    }
} 