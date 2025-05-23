using API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Model.Impl
{
    public class DanhMucTBRepository : IDanhMucTBRepository
    {
        private readonly AppDbContext _context;

        public DanhMucTBRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DanhMucTB>> LayTatCa()
        {
            return await _context.DanhMucTB.ToListAsync();
        }

        public async Task<DanhMucTB> LayTheoMa(string maDanhMuc)
        {
            return await _context.DanhMucTB
                .FirstOrDefaultAsync(dm => dm.MaDanhMuc == maDanhMuc);
        }

        public async Task<DanhMucTB> ThemDanhMuc(DanhMucTB danhMuc)
        {
            try
            {
                await _context.DanhMucTB.AddAsync(danhMuc);
                await _context.SaveChangesAsync();
                return danhMuc;
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra khi thêm danh mục.", ex);
            }
        }

        public async Task<DanhMucTB> CapNhatDanhMuc(DanhMucTB danhMuc)
        {
            try
            {
                var existingDanhMuc = await _context.DanhMucTB
                    .FirstOrDefaultAsync(dm => dm.MaDanhMuc == danhMuc.MaDanhMuc);

                if (existingDanhMuc == null)
                    throw new KeyNotFoundException("Không tìm thấy danh mục với mã: " + danhMuc.MaDanhMuc);

                existingDanhMuc.TenDanhMuc = danhMuc.TenDanhMuc;
                existingDanhMuc.MoTa = danhMuc.MoTa;

                _context.DanhMucTB.Update(existingDanhMuc);
                await _context.SaveChangesAsync();
                return existingDanhMuc;
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra khi cập nhật danh mục.", ex);
            }
        }

        public async Task<bool> XoaDanhMuc(string maDanhMuc)
        {
            var danhMuc = await _context.DanhMucTB
                .FirstOrDefaultAsync(dm => dm.MaDanhMuc == maDanhMuc);

            if (danhMuc == null)
                return false;

            _context.DanhMucTB.Remove(danhMuc);
            await _context.SaveChangesAsync();
            return true;
        }
    }
} 