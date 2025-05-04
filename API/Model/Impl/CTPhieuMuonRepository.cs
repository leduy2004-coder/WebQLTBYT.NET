


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

        public async Task<bool> DuyetCTPhieuMuon(DuyetChiTietPhieuMuon dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var chiTietList = await _context.Set<ChiTietPhieuMuon>()
                    .Where(ct => dto.DanhSachMaCT.Contains(ct.MaCT) && ct.TinhTrang != 2)
                    .ToListAsync();

                if (!chiTietList.Any())
                {
                    return false;
                }

                // Duyệt từng thiết bị
                foreach (var ct in chiTietList)
                {
                    ct.TinhTrang = 2; // Chấp nhận
                }

                // Cập nhật phiếu mượn
                var phieu = await _context.Set<PhieuMuon>().FirstOrDefaultAsync(p => p.MaPhieuMuon == dto.MaPhieuMuon);
                if (phieu == null)
                {
                    throw new Exception("Không tìm thấy phiếu mượn.");
                }

                phieu.TinhTrang = true;

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
