using API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

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

            // Cập nhật tình trạng thành 3 (từ chối) cho các phiếu mượn quá hạn
            foreach (var phieu in phieuMuonQuaHan)
            {
                phieu.TinhTrang = 3;
            }

            await _context.SaveChangesAsync();
        }
    }
} 