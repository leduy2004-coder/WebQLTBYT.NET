using Microsoft.AspNetCore.Mvc;
using WEB.Api;
using WEB.Models;
using WEB.Models.Response;
using WEB.Models.Request;
namespace WEB.Controllers;

public class PhieuTraController : Controller
{
    private readonly PhieuTraService phieuTraService;
    private readonly PhieuMuonService phieuMuonService;

    public PhieuTraController(PhieuTraService phieuTraService, PhieuMuonService phieuMuonService)
    {
        this.phieuTraService = phieuTraService;
        this.phieuMuonService = phieuMuonService;
    }

    public async Task<IActionResult> Index(string maCT)
    {
        ViewBag.MaCTFilter = maCT;
        var userId = HttpContext.Session.GetString("UserId");
        var allCTPhieuMuonTT2 = await phieuMuonService.LayCTPhieuMuonTheoTTVaMaNG(2, userId);
        var allPhieuTra = await phieuTraService.LayPhieuTra();
        
        var PhieutraDaDuyet = allPhieuTra
            .Where(pt => pt.TinhTrang == true && pt.MaNguoiGui == userId)
            .ToList();
        var PhieutraChuaDuyet = allPhieuTra
            .Where(pt => pt.TinhTrang == false && pt.MaNguoiGui == userId)
            .ToList();

        if (!string.IsNullOrEmpty(maCT) && int.TryParse(maCT, out int maCTFilter))
        {
            allCTPhieuMuonTT2 = allCTPhieuMuonTT2
                .Where(pm => pm.MaCT == maCTFilter)
                .ToList();
        }
        var model = new ListPhieuMuonTraResponse
        {
            ListPTDaDuyet = PhieutraDaDuyet,
            ListPTChuaDuyet = PhieutraChuaDuyet,
            ChiTietPhieuMuons = allCTPhieuMuonTT2,
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int maPT)
    {
        if (maPT <= 0)
        {
            TempData["Message"] = "Mã phiếu trả không hợp lệ.";
            TempData["MessageType"] = "danger";
            return RedirectToAction("Index");
        }

        try
        {
            var result = await phieuTraService.XoaPhieuTra(maPT);
            if (!result)
            {
                TempData["Message"] = "Phiếu trả không tồn tại.";
                TempData["MessageType"] = "danger";
                return RedirectToAction("Index");
            }

            TempData["Message"] = "Xóa phiếu trả thành công!" + maPT;
            TempData["MessageType"] = "success";
        }
        catch (Exception ex)
        {
            TempData["Message"] = $"Lỗi khi xóa phiếu trả: {ex.Message}";
            TempData["MessageType"] = "danger";
        }

        return RedirectToAction("Index");
    }
  
    public async Task<IActionResult> ThemPhieuTra()
    {
        var userId = HttpContext.Session.GetString("UserId");
        var allCTPhieuMuonTT2 = await phieuMuonService.LayCTPhieuMuonTheoTTVaMaNG(2, userId);
        var allPhieuTra = await phieuTraService.LayPhieuTra();
        var PhieutraChuaDuyet = await phieuTraService.LayCTPhieuTraTheoTTVaMaNG(false, userId);

        var model = new ThemPhieuTraResponse
        {
            ListPTDaDuyet = null,
            ListPTChuaDuyet = PhieutraChuaDuyet,
            ChiTietPhieuMuons = allCTPhieuMuonTT2,
        };
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> Create(List<ThemCTPhieuTraRequest> chiTietPhieuTras)
    {
        var userId = HttpContext.Session.GetString("UserId");
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var phieuTra = await phieuTraService.LuuPhieuTra(userId);

        foreach (var chiTiet in chiTietPhieuTras)
        {
            var chiTietModel = new ChiTietPhieuTra
            {
                MaPhieuTra = phieuTra.MaPhieuTra,
                MaTB = chiTiet.MaTB,
                SoLuongTBTra = chiTiet.SoLuongTBTra
            };

            await phieuTraService.LuuCTPhieuTra(chiTietModel);
        }

        TempData["Message"] = "Thêm phiếu trả thành công!";
        TempData["MessageType"] = "success";
        return RedirectToAction("Index");
    }
}