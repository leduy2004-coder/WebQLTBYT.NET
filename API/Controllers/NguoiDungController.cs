using Microsoft.AspNetCore.Mvc;
using API.Model;
using API.Data;
using API.Dto;
using API.Dto.request;
using API.Dto.response;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NguoiDungController : ControllerBase
    {
       
        private readonly INguoiDungRepository userRepository;

        public NguoiDungController(INguoiDungRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        // Đăng nhập
        [HttpPost("DangNhap")]
        public async Task<ActionResult<DangNhapResponse>> LoginAsync(DangNhapRequest request)
        {
            // Kiểm tra nếu dữ liệu không hợp lệ
            if (request == null || string.IsNullOrEmpty(request.TaiKhoan) || string.IsNullOrEmpty(request.MatKhau))
            {
                return BadRequest("Tên tài khoản và mật khẩu không được để trống.");
            }

            var user = await userRepository.DangNhap(request.TaiKhoan, request.MatKhau);
    

            if (user == null)
            {
                return Unauthorized("Tài khoản hoặc mật khẩu không chính xác.");
            }

            return Ok(user);
        }

       
    }
}
