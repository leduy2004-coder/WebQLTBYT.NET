using API.Data.ThongKe;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace API.Model.Impl
{
    public class ThongKeRepository : IThongKeRepository
    {
        private readonly AppDbContext _context;

        public ThongKeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ThongKeTongQuan> ThongKeTongQuan(int nam)
        {
            var result = await Task.Run(() =>
                    _context.Set<ThongKeTongQuan>()
                        .FromSqlRaw("EXEC ThongKeTongQuan @Nam", new SqlParameter("@Nam", nam))
                        .AsEnumerable()
                        .FirstOrDefault()
                );

            return result ?? new ThongKeTongQuan
            {
                TongThietBi = 0,
                ThietBiConLai = 0,
                PhieuMuon = 0,
                PhieuTra = 0,
            };
        }

        public async Task<List<PhanBoTheoDanhMuc>> PhanBoTheoDanhMuc()
        {
            return await _context.Set<PhanBoTheoDanhMuc>()
                .FromSqlRaw("EXEC PhanBoTheoDanhMuc")
                .ToListAsync();
        }

        public async Task<List<ThongKeMuonTraTheoThang>> ThongKeMuonTraTheoThang(int nam)
        {
            return await _context.Set<ThongKeMuonTraTheoThang>()
                .FromSqlRaw("EXEC ThongKeMuonTraTheoThang @Nam", new SqlParameter("@Nam", nam))
                .ToListAsync();
        }

        public async Task<List<XuHuongNhapThietBiTheoThang>> XuHuongNhapThietBiTheoThang(int nam)
        {
            return await _context.Set<XuHuongNhapThietBiTheoThang>()
                .FromSqlRaw("EXEC XuHuongNhapThietBiTheoThang @Nam", new SqlParameter("@Nam", nam))
                .ToListAsync();
        }
        public async Task<List<ThietBiMuonNhieuNhat>> ThietBiMuonNhieuNhat(int nam, int top)
        {
            return await _context.Set<ThietBiMuonNhieuNhat>()
                .FromSqlRaw("EXEC ThietBiMuonNhieuNhat @Nam, @TopN",
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@TopN", top))
                .ToListAsync();
        }

    }
}