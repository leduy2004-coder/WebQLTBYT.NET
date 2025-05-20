using System;
using System.Collections.Generic;

namespace API.Dto.response
{
    public class PhieuMuonDto
    {
        public int MaPhieuMuon { get; set; }
        public string MaNguoiGui { get; set; }
        public string? MaNguoiDuyet { get; set; }
        public bool TinhTrang { get; set; }
        public DateTime? NgayTao { get; set; }
        public NguoiDungDto NguoiGui { get; set; }
        public NguoiDungDto NguoiDuyet { get; set; }
        public List<ChiTietPhieuMuonDto> ChiTietPhieuMuons { get; set; }
    }

    public class ChiTietPhieuMuonDto
    {
        public int MaCT { get; set; }
        public int MaPhieuMuon { get; set; }
        public string MaTB { get; set; }
        public bool TinhCanThiet { get; set; }
        public string MucDich { get; set; }
        public DateTime NgayMuon { get; set; }
        public DateTime NgayDuKienTra { get; set; }
        public int SoLuongTBMuon { get; set; }
        public int TinhTrang { get; set; }
        public ThietBiDto ThietBi { get; set; }
    }

    public class ThietBiDto
    {
        public string MaThietBi { get; set; }
        public string TenThietBi { get; set; }
    }

    public class NguoiDungDto
    {
        public string MaNguoiDung { get; set; }
        public string HoTen { get; set; }
        public string Khoa { get; set; }
    }
} 