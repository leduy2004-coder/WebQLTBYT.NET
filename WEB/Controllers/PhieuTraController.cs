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

    public async Task<IActionResult> Index(string maPM)
    {
        ViewBag.MaPTFilter = maPM;

        var allCTPhieuMuonTT2 = await phieuMuonService.LayCTPhieuMuonTheoTT(2);
        var allPhieuTra = await phieuTraService.LayPhieuTra();
        var PhieutraDaDuyet = allPhieuTra.Where(pt => pt.TinhTrang == true).ToList();
        var PhieutraChuaDuyet = allPhieuTra.Where(pt => pt.TinhTrang == false).ToList();

        if (!string.IsNullOrEmpty(maPM) && int.TryParse(maPM, out int maPMFilter))
        {
            allCTPhieuMuonTT2 = allCTPhieuMuonTT2
                .Where(pm => pm.MaPhieuMuon == maPMFilter)
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
    public ActionResult ThemPhieuTra()
    {
        var allCTPhieuMuonTT2 = phieuMuonService.LayCTPhieuMuonTheoTT(2).Result;
        var model = new ListPhieuMuonTraResponse
        {
            ListPTDaDuyet = null,
            ListPTChuaDuyet = null,
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