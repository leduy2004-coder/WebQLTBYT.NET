using API.Data;
using API.Dto.request;
using Mapster;
using Microsoft.EntityFrameworkCore;


namespace API.Model.Impl
{
    public class CTPhieuNhapRepository : ICTPhieuNhapRepository
    {
        private readonly AppDbContext _context;

        public CTPhieuNhapRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ChiTietPhieuNhap>> LayCTPNTheoPN(int maPN)
        {
            return await _context.ChiTietPhieuNhap
                                 .Where(tb => tb.MaPhieuNhap == maPN)
                                 .Include(p => p.ThietBi)
                                 .ToListAsync();
        }

        public Task<IEnumerable<ChiTietPhieuNhap>> LayTatCa()
        {
            throw new NotImplementedException();
        }

        public async Task<ChiTietPhieuNhap> ThemCTPhieuNhap(CTPhieuNhapRequest chiTietPhieuNhap)
        {
            var tbResponse = chiTietPhieuNhap.Adapt<ChiTietPhieuNhap>();
            try
            {
                await _context.ChiTietPhieuNhap.AddAsync(tbResponse);

                await _context.SaveChangesAsync();

                return tbResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra khi thêm phim.", ex);
            }
        }
    }
}
