

using Microsoft.AspNetCore.Mvc;

using WEB.Api;
using WEB.Models.Request;
namespace WEB.Controllers;
public class DangNhapController : Controller
{

        private readonly DangNhapService _loginService;
        public DangNhapController(DangNhapService loginService)
        {
            this._loginService = loginService;
        }

        public IActionResult Index()
        {
            return View();
        }
        //login
        [HttpPost("DangNhap")]
        public async Task<IActionResult> DangNhap(DangNhapRequest loginRequest)
        {
            try
            {
                var loginResponse = await _loginService.LoginAsync(loginRequest);

                if (loginResponse != null)
                {
                    HttpContext.Session.SetString("FullName", loginResponse.HoTen);
                    HttpContext.Session.SetString("UserId", loginResponse.MaNguoiDung);
                    HttpContext.Session.SetInt32("Role", loginResponse.MaVaiTro);

                    TempData["Message"] = "Đăng nhập thành công!";
                    TempData["MessageType"] = "success";
                    return RedirectToAction("Index", "TrangChu", new { maDM = "DM01" });


            }

        }
            catch (HttpRequestException ex)
            {
                // API trả về lỗi 401
                TempData["Message"] = "Tài khoản hoặc mật khẩu không chính xác!";
                TempData["MessageType"] = "error";
            }
            catch (Exception ex)
            {
                // Xử lý lỗi khác
                TempData["Message"] = "Đã xảy ra lỗi. Vui lòng thử lại sau!";
                TempData["MessageType"] = "error";
            }

            return View("Index");
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["Message"] = "Đã đăng xuất thành công!";
            TempData["MessageType"] = "success";
            return RedirectToAction("Index", "DangNhap");
        }
    

}