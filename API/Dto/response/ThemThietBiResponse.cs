

using API.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Dto.response
{
    public class ThemThietBiResponse
    {
        public string MaThietBi { get; set; }

        public string TenThietBi { get; set; }
        public string MaDanhMuc { get; set; }

        public string MaLoai { get; set; }

        public byte[] HinhAnh { get; set; }

        public string MoTa { get; set; }

    }

}
