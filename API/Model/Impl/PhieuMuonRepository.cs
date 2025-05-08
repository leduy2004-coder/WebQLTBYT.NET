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
    }
}
