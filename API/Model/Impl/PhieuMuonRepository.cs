using API.Data;
using API.Dto.request;
using API.Dto.response;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace API.Model.Impl
{
    public class PhieuMuonRepository : IPhieuMuonRepository
    {
        private readonly AppDbContext _context;

        public PhieuMuonRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<PhieuMuon>> LayTatCa()
        {
            return await _context.Set<PhieuMuon>()
                .Include(p => p.NguoiGui)
                .Include(p => p.NguoiDuyet)
                .ToListAsync();
        }

        public async Task<PhieuMuon?> LayPMTheoMa(int maPM)
        {
            return await _context.Set<PhieuMuon>()
                .Include(p => p.NguoiGui)
                .Include(p => p.NguoiDuyet)
                //.Include(p => p.ChiTietPhieuMuons)
                .FirstOrDefaultAsync(p => p.MaPhieuMuon == maPM);
        }

        public async Task<PhieuMuon> ThemPhieuMuon(ThemPhieuMuonRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Tạo phiếu mượn mới
                var phieuMuon = new PhieuMuon
                {
                    MaNguoiGui = request.MaNguoiGui,
                    TinhTrang = false, // Chưa duyệt
                    NgayTao = DateTime.Now
                };

                await _context.PhieuMuon.AddAsync(phieuMuon);
                await _context.SaveChangesAsync();

                // Thêm chi tiết phiếu mượn
                foreach (var chiTiet in request.ChiTietPhieuMuons)
                {
                    var ctPhieuMuon = new ChiTietPhieuMuon
                    {
                        MaPhieuMuon = phieuMuon.MaPhieuMuon,
                        MaTB = chiTiet.MaTB,
                        TinhCanThiet = chiTiet.TinhCanThiet,
                        MucDich = chiTiet.MucDich,
                        NgayMuon = chiTiet.NgayMuon,
                        NgayDuKienTra = chiTiet.NgayDuKienTra,
                        SoLuongTBMuon = chiTiet.SoLuongTBMuon,
                        TinhTrang = 1 // Chưa duyệt
                    };

                    await _context.ChiTietPhieuMuon.AddAsync(ctPhieuMuon);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return phieuMuon;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> XoaPhieuMuon(int maPM)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Lấy phiếu mượn
                var phieuMuon = await _context.PhieuMuon
                    .Include(p => p.ChiTietPhieuMuons)
                    .FirstOrDefaultAsync(p => p.MaPhieuMuon == maPM);

                if (phieuMuon == null)
                    return false;

                // Xóa các chi tiết phiếu mượn trước
                _context.ChiTietPhieuMuon.RemoveRange(phieuMuon.ChiTietPhieuMuons);

                // Xóa phiếu mượn
                _context.PhieuMuon.Remove(phieuMuon);

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

        public async Task<bool> CapNhatPhieuMuon(int maPM, CapNhatPhieuMuonRequest request)
        {
            try
            {
                var phieuMuon = await _context.PhieuMuon.FindAsync(maPM);
                if (phieuMuon == null)
                    return false;

                // Cập nhật thông tin phiếu mượn
                phieuMuon.MaNguoiGui = request.MaNguoiGui;

                // Xóa các chi tiết phiếu mượn cũ
                var chiTietCu = await _context.ChiTietPhieuMuon.Where(ct => ct.MaPhieuMuon == maPM).ToListAsync();
                _context.ChiTietPhieuMuon.RemoveRange(chiTietCu);

                // Thêm các chi tiết phiếu mượn mới
                foreach (var chiTiet in request.ChiTietPhieuMuons)
                {
                    var chiTietMoi = new ChiTietPhieuMuon
                    {
                        MaPhieuMuon = maPM,
                        MaTB = chiTiet.MaTB,
                        TinhCanThiet = chiTiet.TinhCanThiet,
                        MucDich = chiTiet.MucDich,
                        NgayMuon = chiTiet.NgayMuon,
                        NgayDuKienTra = chiTiet.NgayDuKienTra,
                        SoLuongTBMuon = chiTiet.SoLuongTBMuon,
                        TinhTrang = 1 // Chưa duyệt
                    };
                    _context.ChiTietPhieuMuon.Add(chiTietMoi);
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<IEnumerable<ChiTietPhieuMuon>> LayTatCaChiTietPhieuMuon()
        {
            var danhSachCTM = await _context.ChiTietPhieuMuon
                .Include(c => c.ThietBi) // Load thông tin thiết bị nếu cần
                .ToListAsync();

            return danhSachCTM;
        }
    }
}
