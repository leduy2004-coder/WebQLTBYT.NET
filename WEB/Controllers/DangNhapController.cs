using Microsoft.AspNetCore.Mvc;
using WEB.Api;
using WEB.Models.Request;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

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
                // Lưu thông tin vào session
                HttpContext.Session.SetString("FullName", loginResponse.HoTen);
                HttpContext.Session.SetString("UserId", loginResponse.MaNguoiDung);
                HttpContext.Session.SetInt32("Role", loginResponse.MaVaiTro);

                // Tạo claims cho authentication
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, loginResponse.MaNguoiDung),
                    new Claim(ClaimTypes.Role, loginResponse.MaVaiTro == 5 ? "Staff" : "Other")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true, // Cookie sẽ được lưu trữ sau khi đóng trình duyệt
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1) // Cookie hết hạn sau 1 giờ
                };

                // Đăng nhập user
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

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

    // Đăng xuất
    public async Task<IActionResult> Logout()
    {
        // Xóa session
        HttpContext.Session.Clear();

        // Đăng xuất authentication
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Index", "DangNhap");
    }
}