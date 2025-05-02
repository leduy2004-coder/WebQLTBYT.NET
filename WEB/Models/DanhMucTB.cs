using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB.Models
{
    public class DanhMucTB
    {
            public string MaDanhMuc { get; set; }

            [StringLength(50)]
            public string TenDanhMuc { get; set; }

            public string MoTa { get; set; }
        
    }
}
