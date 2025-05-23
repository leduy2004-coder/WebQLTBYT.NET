using System.ComponentModel.DataAnnotations;

namespace WEB.Models.Request
{
    public class DanhMucTBRequest
    {
        [Required]
        [StringLength(20)]
        public string MaDanhMuc { get; set; }

        [Required]
        [StringLength(50)]
        public string TenDanhMuc { get; set; }

        public string MoTa { get; set; }
    }
} 