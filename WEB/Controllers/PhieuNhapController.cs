
using Microsoft.AspNetCore.Mvc;
using WEB.Api;
using WEB.Models;
using WEB.Models.Response;


namespace WEB.Controllers;

public class PhieuNhapController : Controller
{

    private readonly PhieuNhapService phieuNhapService;
    private readonly ThietBiService thietBiService;
    public PhieuNhapController(PhieuNhapService phieuNhapService, ThietBiService thietBiService)
    {
        this.phieuNhapService = phieuNhapService;
        this.thietBiService = thietBiService;
    }

    public async Task<IActionResult> Index(string maPN, string ngayNhap)
    {
        
        ViewBag.MaPNFilter = maPN;
        ViewBag.NgayNhapFilter = ngayNhap;


        var phieuNhapList = await phieuNhapService.LayPhieuNhap();


        if (!string.IsNullOrEmpty(maPN) && int.TryParse(maPN, out int maPNFilter))
        {
            phieuNhapList = phieuNhapList
                .Where(pn => pn.MaPhieuNhap == maPNFilter)
                .ToList();
        }

        if (!string.IsNullOrEmpty(ngayNhap))
        {
            if (DateTime.TryParse(ngayNhap, out DateTime filterDate))
            {
                phieuNhapList = phieuNhapList
                    .Where(pn => pn.NgayNhap.Date == filterDate.Date)
                    .ToList();
            }
        }

        return View(phieuNhapList);
    }
    public async Task<IActionResult> ChiTietPhieuNhap(int maPN)
    {
        var phieuNhap = await phieuNhapService.LayPNTheoMa(maPN);
        var listCTPhieuNhap = await phieuNhapService.LayCTPhieuNhapTheoPN(maPN);
        var model = new CTPhieuNhapResponse
        {
            PhieuNhap = phieuNhap,
            ChiTietPhieuNhaps = listCTPhieuNhap
        };
        return View(model);
    }
    public async Task<IActionResult> ThemPhieuNhap()
    {
        var listTB = await thietBiService.LayTatCaTB();
   
        var model = new ThemPNResponse
        {
            listTB = listTB
        };
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> Create(List<ChiTietPhieuNhap> chiTietPhieuNhaps)
    {
        var userId = HttpContext.Session.GetString("UserId");
        var phieuNhap = await phieuNhapService.LuuPhieuNhap(userId);
         
        foreach (var chiTiet in chiTietPhieuNhaps)
        {
            chiTiet.MaPhieuNhap = phieuNhap.MaPhieuNhap;
 
            var chiTietPN = await phieuNhapService.LuuCTPhieuNhap(chiTiet);
           
        }
        TempData["Message"] = "Thêm phiếu nhập thành công!";
        TempData["MessageType"] = "success";
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(String maPhieuNhap)
    {
        if (string.IsNullOrEmpty(maPhieuNhap))
        {
            TempData["Message"] = "Mã phiếu nhập không hợp lệ.";
            TempData["MessageType"] = "danger";
            return RedirectToAction("Index");
        }

        try
        {
            var phieuNhap = await phieuNhapService.XoaPhieuNhap(int.Parse(maPhieuNhap));
            if (phieuNhap == false)
            {
                TempData["Message"] = "Phiếu nhập không tồn tại.";
                TempData["MessageType"] = "danger";
                return RedirectToAction("Index");
            }        

            TempData["Message"] = "Xóa phiếu nhập thành công!";
            TempData["MessageType"] = "success";
        }
        catch (Exception ex)
        {
            TempData["Message"] = $"Lỗi khi xóa phiếu nhập: {ex.Message}";
            TempData["MessageType"] = "danger";
        }

        return RedirectToAction("Index");
    }
}