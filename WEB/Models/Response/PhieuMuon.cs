using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB.Models.Response
{
    public class PhieuMuon
    {
        public int MaPhieuMuon { get; set; }

        public string MaNguoiGui { get; set; }

        public string? MaNguoiDuyet { get; set; }

        public bool TinhTrang { get; set; } = false; // 0: Không duyệt, 1: Đã duyệt

        public NguoiDungDto NguoiGui { get; set; }

        public NguoiDungDto NguoiDuyet { get; set; }
    }

    public class NguoiDungDto
    {
        public string MaNguoiDung { get; set; }

        public string HoTen { get; set; }

        public string Khoa { get; set; }
    }
}
