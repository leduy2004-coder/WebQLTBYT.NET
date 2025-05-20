using System;

namespace API.Dto.request
{
    public class SuaPhieuMuonRequest
    {
        public int MaPM { get; set; }
        public DateTime NgayMuon { get; set; }
        public DateTime NgayTra { get; set; }
        public string? GhiChu { get; set; }
        public List<int> MaThietBis { get; set; } = new();
    }
} 