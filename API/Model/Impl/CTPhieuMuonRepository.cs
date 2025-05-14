


using API.Data;
using API.Dto.request;
using Microsoft.EntityFrameworkCore;

namespace API.Model.Impl
{
    public class CTPhieuMuonRepository : ICTPhieuMuonRepository
    {
        private readonly AppDbContext _context;

        public CTPhieuMuonRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ChiTietPhieuMuon>> LayCTPMTheoMaPM(int maPM)
        {
            return await _context.Set<ChiTietPhieuMuon>()
                .Where(ct => ct.MaPhieuMuon == maPM)
                .Include(ct => ct.ThietBi)
                .ToListAsync();
        }
        public async Task<IEnumerable<ChiTietPhieuMuon>> LayCTPMTheoTT(int TT)
        {
            return await _context.Set<ChiTietPhieuMuon>()
                .Where(ct => ct.TinhTrang == TT)
                .Include(ct => ct.ThietBi)
                .ToListAsync();
        }

        public async Task<bool> DuyetCTPhieuMuon(DuyetChiTietPhieuMuon dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // 1. Lấy tất cả chi tiết của phiếu mượn
                var allCTs = await _context.Set<ChiTietPhieuMuon>()
                    .Where(ct => ct.MaPhieuMuon == dto.MaPhieuMuon)
                    .ToListAsync();

                if (!allCTs.Any())
                    return false;

                // 2. Duyệt (TinhTrang = 2) cho những mã được chọn
                var danhSachDuyet = allCTs.Where(ct => dto.DanhSachMaCT.Contains(ct.MaCT)).ToList();
                foreach (var ct in danhSachDuyet)
                {
                    ct.TinhTrang = 2;
                }

                // 3. Từ chối (TinhTrang = 3) cho những mã không được chọn
                var danhSachTuChoi = allCTs.Where(ct => !dto.DanhSachMaCT.Contains(ct.MaCT)).ToList();
                foreach (var ct in danhSachTuChoi)
                {
                    ct.TinhTrang = 3;
                }

                // 4. Cập nhật phiếu mượn thành đã duyệt
                var phieu = await _context.Set<PhieuMuon>().FirstOrDefaultAsync(p => p.MaPhieuMuon == dto.MaPhieuMuon);
                if (phieu == null)
                    throw new Exception("Không tìm thấy phiếu mượn.");

                phieu.TinhTrang = true;
                phieu.MaNguoiDuyet = dto.userId; 

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
