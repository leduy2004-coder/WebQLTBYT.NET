using System;
using System.ComponentModel.DataAnnotations;

namespace API.Dto.response
{
    public class LayThietBiResponse
    {
        public string MaNguoiDung { get; set; }

        public string HoTen { get; set; }

        public string SDT { get; set; }

        public string Khoa { get; set; }

        public int MaVaiTro { get; set; }

    }
}
