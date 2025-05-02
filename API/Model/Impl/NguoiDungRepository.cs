using API.Data;
using API.Dto.response;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model.Impl
{
    public class NguoiDungRepository : INguoiDungRepository
    {
        private readonly AppDbContext _context;

        public NguoiDungRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DangNhapResponse> DangNhap(string tk, string mk)
        {
            var user = await _context.NguoiDung
                        .FirstOrDefaultAsync(kh => kh.TaiKhoan == tk && kh.MatKhau == mk);

            if (user == null)
                return null;

            // Sử dụng Mapper để chuyển đổi sang DTO
            var userReponse = user.Adapt<DangNhapResponse>();

            return userReponse;
        }

    }
}
