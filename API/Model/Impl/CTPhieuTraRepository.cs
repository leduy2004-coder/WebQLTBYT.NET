using API.Data;
using API.Dto.request;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace API.Model.Impl
{
    public class CTPhieuTraRepository : ICTPhieuTraRepository
    {
        private readonly AppDbContext _context;

        public CTPhieuTraRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ChiTietPhieuTra>> LayCTPTTheoMaPT(int maPT)
        {
            return await _context.ChiTietPhieuTra
                                .Where(tb => tb.MaPhieuTra == maPT)
                                .Include(p => p.ThietBi)
                                .ToListAsync();
        }
        public async Task<ChiTietPhieuTra> ThemCTPhieuTra(CTPhieuTraRequest chiTietPhieuTra)
        {
            var tbResponse = chiTietPhieuTra.Adapt<ChiTietPhieuTra>();
            try
            {
                await _context.ChiTietPhieuTra.AddAsync(tbResponse);

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
