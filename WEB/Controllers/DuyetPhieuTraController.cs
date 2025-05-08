
using Microsoft.AspNetCore.Mvc;
using WEB.Api;
using WEB.Models;
using WEB.Models.Response;


namespace WEB.Controllers;

public class DuyetPhieuTraController : Controller
{

    private readonly PhieuTraService phieuTraService;
    private readonly ThietBiService thietBiService;
    public DuyetPhieuTraController(PhieuTraService phieuTraService, ThietBiService thietBiService)
    {
        this.phieuTraService = phieuTraService;
        this.thietBiService = thietBiService;
    }

    // GET: PhieuTra/Index
    public async Task<IActionResult> Index(string maPT)
    {
        // Store filter value in ViewBag for form persistence
        ViewBag.MaPTFilter = maPT;

        // Fetch approved PhieuTra (TinhTrang == true)
        var approvedPhieuTraList = await phieuTraService.LayPhieuTra();
        approvedPhieuTraList = approvedPhieuTraList.Where(pt => pt.TinhTrang == true).ToList();

        // Apply maPT filter for approved list
        if (!string.IsNullOrEmpty(maPT) && int.TryParse(maPT, out int maPTFilter))
        {
            approvedPhieuTraList = approvedPhieuTraList
                .Where(pt => pt.MaPhieuTra == maPTFilter)
                .ToList();
        }

        // Fetch unapproved PhieuTra (TinhTrang == false)
        var unapprovedPhieuTraList = await phieuTraService.LayPhieuTra();
        unapprovedPhieuTraList = unapprovedPhieuTraList.Where(pt => pt.TinhTrang == false).ToList();

        // Create view model
        var model = new DuyetPhieuTraResponse
        {
            ApprovedPhieuTraList = approvedPhieuTraList,
            UnapprovedPhieuTraList = unapprovedPhieuTraList
        };

        return View(model);
    }

    // POST: PhieuTra/Approve
    [HttpPost]
    public async Task<IActionResult> Approve(List<int> selectedPhieuTraIds)
    {
        if (selectedPhieuTraIds == null || !selectedPhieuTraIds.Any())
        {
            TempData["Message"] = "Vui lòng chọn ít nhất một phiếu trả để duyệt.";
            TempData["MessageType"] = "danger";
            return RedirectToAction("Index");
        }
        var userId = HttpContext.Session.GetString("UserId");
        try
        {
            foreach (var maPhieuTra in selectedPhieuTraIds)
            {
                var phieuTra = await phieuTraService.DuyetPhieuTra(maPhieuTra, userId);               
            }

            TempData["Message"] = "Duyệt phiếu trả thành công!";
            TempData["MessageType"] = "success";
        }
        catch (Exception ex)
        {
            TempData["Message"] = $"Lỗi khi duyệt phiếu trả: {ex.Message}";
            TempData["MessageType"] = "danger";
        }

        return RedirectToAction("Index");
    }

    // GET: PhieuTra/ChiTietPhieuTra
    public async Task<IActionResult> ChiTietPhieuTra(int maPT)
    {
        var phieuTra = await phieuTraService.LayPTTheoMa(maPT);
        if (phieuTra == null)
        {
            TempData["Message"] = "Phiếu trả không tồn tại.";
            TempData["MessageType"] = "danger";
            return RedirectToAction("Index");
        }

        var chiTietPhieuTraList = await phieuTraService.LayCTPhieuTraTheoPT(maPT);
        var model = new ChiTietPhieuTraResponse
        {
            PhieuTra = phieuTra,
            ChiTietPhieuTraList = chiTietPhieuTraList
        };

        return PartialView("ChiTietPhieuTra", model);
    }
}