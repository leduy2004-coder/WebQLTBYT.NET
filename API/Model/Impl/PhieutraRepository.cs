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
                                 .FirstAsync();
        }

        public async Task<IEnumerable<PhieuTra>> LayTatCa()
        {
            var danhSachPT = await _context.PhieuTra
                .ToListAsync();
            return danhSachPT;
        }
    }
}
