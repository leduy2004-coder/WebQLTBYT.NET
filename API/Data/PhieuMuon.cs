﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Data
{
    public class PhieuMuon
    {
        [Key]
        public int MaPhieuMuon { get; set; }

        [Required, StringLength(20)]
        public string MaNguoiGui { get; set; }

        [StringLength(20)]
        public string? MaNguoiDuyet { get; set; }

        public bool TinhTrang { get; set; } = false; // 0: Không duyệt, 1: Đã duyệt

        [ForeignKey("MaNguoiGui")]
        public virtual NguoiDung NguoiGui { get; set; }

        [ForeignKey("MaNguoiDuyet")]
        public virtual NguoiDung? NguoiDuyet { get; set; }

        [JsonIgnore]
        public virtual ICollection<ChiTietPhieuMuon> ChiTietPhieuMuons { get; set; }

        public DateTime NgayTao { get; set; }
    }
}
