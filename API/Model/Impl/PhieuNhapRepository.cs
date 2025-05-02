using API.Data;
using API.Dto.request;
using API.Dto.response;
using Mapster;
using Microsoft.EntityFrameworkCore;


namespace API.Model.Impl
{
    public class PhieuNhapRepository : IPhieuNhapRepository
    {
        private readonly AppDbContext _context;

        public PhieuNhapRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PhieuNhap> LayPNTheoMa(int maPN)
        {
            return await _context.PhieuNhap
                                 .Where(tb => tb.MaPhieuNhap == maPN)
                                 .FirstAsync();

        }

        public async Task<IEnumerable<PhieuNhap>> LayTatCa()
        {
            var danhSachPN = await _context.PhieuNhap
                .ToListAsync();
            return danhSachPN;
        }

        public async Task<PhieuNhap> ThemPhieuNhap(PhieuNhapRequest phieuNhap)
        {
            var tbResponse = phieuNhap.Adapt<PhieuNhap>();
            try
            {
                await _context.PhieuNhap.AddAsync(tbResponse);

                await _context.SaveChangesAsync();

                return tbResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra khi thêm phim.", ex);
            }
        }

        public async Task<bool> XoaPhieuNhap(int maPN)
        {
            var pn = await _context.PhieuNhap.FirstOrDefaultAsync(p => p.MaPhieuNhap == maPN);
            if (pn == null) return false;

            _context.PhieuNhap.Remove(pn);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
