using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB.Models
{
    public class ThietBi
    {

        public string MaThietBi { get; set; }


        public string TenThietBi { get; set; }

        public int SoLuongTong { get; set; }

        public int SoLuongCon { get; set; }

        public string MaDanhMuc { get; set; }

        public string MaLoai { get; set; }

        public byte[]? HinhAnh { get; set; }

        public string MoTa { get; set; }

        public string? HinhAnhBase64 => HinhAnh != null
        ? $"data:image/jpeg;base64,{Convert.ToBase64String(HinhAnh)}"
        : null;
    }
}
