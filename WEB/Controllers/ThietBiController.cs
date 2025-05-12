
using Microsoft.AspNetCore.Mvc;
using WEB.Api;
using WEB.Models;
using WEB.Models.Request;
using WEB.Models.Response;

namespace WEB.Controllers;

public class ThietBiController : Controller
{
   
    private readonly ThietBiService thietBiService;
    public ThietBiController(ThietBiService thietBiService)
    {
        this.thietBiService = thietBiService;
    }

    [HttpPost]
    public async Task<IActionResult> TaoTBMoi(ThemThietBiRequest request)
    {
        if (!ModelState.IsValid)
            return View(); // hoặc return RedirectToAction với thông báo lỗi

        request.SoLuongCon = 0;
        request.SoLuongTong = 0;

        await thietBiService.LuuThietBi(request,"/api/ThietBi", HttpMethod.Post);

        return RedirectToAction("Index", "TrangChu");
    }

    [HttpPost]
    public async Task<IActionResult> CapNhatThietBi(ThemThietBiRequest request)
    {


        await thietBiService.LuuThietBi(request, "/api/ThietBi", HttpMethod.Put);

        return RedirectToAction("Index", "TrangChu");
    }
}
