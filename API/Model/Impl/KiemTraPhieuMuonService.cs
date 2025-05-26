using API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace API.Model.Impl
{
    public class KiemTraPhieuMuonService : IKiemTraPhieuMuonService
    {
        private readonly AppDbContext _context;

        public KiemTraPhieuMuonService(AppDbContext context)
        {
            _context = context;
        }

        public async Task KiemTraPhieuMuonQuaHan()
        {
            var currentDate = DateTime.Now.Date;

            // Lấy tất cả chi tiết phiếu mượn có tình trạng là 1 (chưa duyệt) và ngày mượn nhỏ hơn ngày hiện tại
            var phieuMuonQuaHan = await _context.ChiTietPhieuMuon
                .Where(ct => ct.TinhTrang == 1 && ct.NgayMuon.Date < currentDate)
                .ToListAsync();

            // Lấy danh sách các mã phiếu mượn cần cập nhật
            var maPhieuMuonList = phieuMuonQuaHan.Select(p => p.MaPhieuMuon).Distinct().ToList();

            // Cập nhật tất cả chi tiết phiếu mượn có cùng mã phiếu mượn
            var allChiTietToUpdate = await _context.ChiTietPhieuMuon
                .Where(ct => maPhieuMuonList.Contains(ct.MaPhieuMuon))
                .ToListAsync();

            foreach (var chiTiet in allChiTietToUpdate)
            {
                chiTiet.TinhTrang = 3; // Cập nhật tình trạng thành từ chối
            }

            // Cập nhật tình trạng phiếu mượn thành đã duyệt (1)
            var phieuMuonToUpdate = await _context.PhieuMuon
                .Where(pm => maPhieuMuonList.Contains(pm.MaPhieuMuon))
                .ToListAsync();

            foreach (var phieuMuon in phieuMuonToUpdate)
            {
                phieuMuon.TinhTrang = true; // Đã duyệt
            }

            await _context.SaveChangesAsync();
        }
    }
} 