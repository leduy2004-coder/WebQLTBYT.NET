using API.Data;
using API.Dto.request;
using API.Dto.response;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace API.Model.Impl
{
    public class PhieuTraRepository : IPhieuTraRepository
    {
        private readonly AppDbContext _context;

        public PhieuTraRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DuyetPhieuTra(DuyetPhieuTraRequest request)
        {
            var phieuTra = await _context.PhieuTra.FindAsync(request.MaPT);
            if (phieuTra == null)
            {
                return false; // Không tìm thấy phiếu trả
            }
            phieuTra.MaNguoiDuyet = request.UserId;
            phieuTra.TinhTrang = true; // Đã duyệt

            _context.PhieuTra.Update(phieuTra);
            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<PhieuTra> LayPTTheoMa(int maPT)
        {
            return await _context.PhieuTra
                                 .Where(tb => tb.MaPhieuTra == maPT)
                                 .Include(p => p.NguoiDuyet)
                                 .Include(p => p.NguoiGui)
                                 .FirstAsync();
        }

        public async Task<IEnumerable<PhieuTra>> LayTatCa()
        {
            var danhSachPT = await _context.PhieuTra
                .Include(p => p.NguoiDuyet)
                  .Include(p => p.NguoiGui)
                .ToListAsync();
            return danhSachPT;
        }
        public async Task<bool> XoaPhieuTra(int maPT)
        {
            var pt = await _context.PhieuTra.FirstOrDefaultAsync(p => p.MaPhieuTra == maPT);
            if (pt == null) return false;

            _context.PhieuTra.Remove(pt);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<PhieuTra> ThemPhieuTra(PhieuTraRequest phieuTra)
        {
            var tbResponse = phieuTra.Adapt<PhieuTra>();
            try
            {
                await _context.PhieuTra.AddAsync(tbResponse);

                await _context.SaveChangesAsync();

                return tbResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra khi thêm.", ex);
            }
        }
    }
}
