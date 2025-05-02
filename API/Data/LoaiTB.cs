using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Data
{
    public class LoaiTB
    {
        [Key]
        [StringLength(1)]
        public string MaLoai { get; set; }

        [StringLength(100)]
        public string MoTa { get; set; }
    }
}
