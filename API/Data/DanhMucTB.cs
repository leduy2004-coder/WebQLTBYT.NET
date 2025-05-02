using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Data
{
    public class DanhMucTB
    {
            [Key]
            [StringLength(20)]
            public string MaDanhMuc { get; set; }

            [StringLength(50)]
            public string TenDanhMuc { get; set; }

            public string MoTa { get; set; }
        
    }
}
