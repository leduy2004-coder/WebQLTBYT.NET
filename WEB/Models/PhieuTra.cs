using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB.Models
{
    public class PhieuTra
    {

        public int MaPhieuTra { get; set; }


        public string MaNguoiGui { get; set; }

        public string? MaNguoiDuyet { get; set; }

        public DateTime NgayTra { get; set; }

        public bool TinhTrang { get; set; } = false; // 0: Không duyệt, 1: Đã duyệt


    }
}
