using API.Data;
using API.Dto.request;
using API.Dto.response;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model.Impl
{
    public class ThietBiRepository : IThietBiRepository
    {
        private readonly AppDbContext _context;

        public ThietBiRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ThietBi>> LayTatCa()
        { 
            try
            {
                var danhSachTB = await _context.ThietBi
                .Include(p => p.DanhMuc) 
                .ToListAsync();
                return danhSachTB;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<IEnumerable<DanhMucTB>> LayDanhMuc()
        {
            try
            {
                var danhSachTB = await _context.DanhMucTB
                .ToListAsync();
                return danhSachTB;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<IEnumerable<ThietBi>> LayTBTheoDanhMuc(string maDM)
        {
            return await _context.ThietBi
                                 .Where(tb => tb.MaDanhMuc == maDM)
                                 .ToListAsync();
        }

        public async Task<ThemThietBiResponse> ThemThietBi(ThemThietBiRequest tb)
        {
            byte[]? imageData = null;

            if (tb.HinhAnh != null && tb.HinhAnh.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await tb.HinhAnh.CopyToAsync(memoryStream);
                    imageData = memoryStream.ToArray();
                }
            }
            var entity = new ThietBi
            {
                MaThietBi = tb.MaThietBi,
                TenThietBi = tb.TenThietBi,
                SoLuongTong = tb.SoLuongTong,
                SoLuongCon = tb.SoLuongCon,
                MaDanhMuc = tb.MaDanhMuc,
                MaLoai = tb.MaLoai,
                MoTa = tb.MoTa,
                HinhAnh = imageData
            };

            _context.ThietBi.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Xử lý lỗi nếu cần thiết
                throw new Exception("Lỗi khi thêm thiết bị: " + ex.Message);
            }

            var tbResponse = entity.Adapt<ThemThietBiResponse>();
            return tbResponse;
        }

    }
}
