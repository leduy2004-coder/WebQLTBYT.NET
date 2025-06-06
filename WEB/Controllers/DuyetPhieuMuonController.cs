﻿
using Microsoft.AspNetCore.Mvc;
using WEB.Api;
using WEB.Models;
using WEB.Models.Request;
using WEB.Models.Response;


namespace WEB.Controllers;

public class DuyetPhieuMuonController : Controller
{

    private readonly PhieuMuonService phieuMuonService;
    private readonly ThietBiService thietBiService;

    public DuyetPhieuMuonController(PhieuMuonService phieuMuonService, ThietBiService thietBiService)
    {
        this.phieuMuonService = phieuMuonService;
        this.thietBiService = thietBiService;
    }

    // GET: PhieuMuon/Index
    public async Task<IActionResult> Index(string maPM)
    {
        ViewBag.MaPMFilter = maPM;

        var allPhieuMuon = await phieuMuonService.LayPhieuMuon();
        var approvedPhieuMuonList = allPhieuMuon.Where(pm => pm.TinhTrang == true).ToList();

        if (!string.IsNullOrEmpty(maPM) && int.TryParse(maPM, out int maPMFilter))
        {
            approvedPhieuMuonList = approvedPhieuMuonList
                .Where(pm => pm.MaPhieuMuon == maPMFilter)
                .ToList();
        }

        var unapprovedPhieuMuonList = allPhieuMuon.Where(pm => pm.TinhTrang == false).ToList();

        var model = new DuyetPhieuMuonResponse
        {
            PhieuMuonDaDuyetList = approvedPhieuMuonList,
            PhieuMuonChuaDuyetList = unapprovedPhieuMuonList
        };

        return View(model);
    }

    // POST: PhieuMuon/Approve
    //[HttpPost]
    //public async Task<IActionResult> Approve(List<int> selectedPhieuMuonIds)
    //{
    //    if (selectedPhieuMuonIds == null || !selectedPhieuMuonIds.Any())
    //    {
    //        TempData["Message"] = "Vui lòng chọn ít nhất một phiếu mượn để duyệt.";
    //        TempData["MessageType"] = "danger";
    //        return RedirectToAction("Index");
    //    }

    //    try
    //    {
    //        foreach (var maPhieuMuon in selectedPhieuMuonIds)
    //        {
    //            var duyetDto = new DuyetChiTietPhieuMuon
    //            {
    //                MaPhieuMuon = maPhieuMuon,
    //                DanhSachMaCT = new List<int>()
    //            };

    //            var chiTietList = await phieuMuonService.LayCTPhieuMuonTheoPM(maPhieuMuon);

    //            duyetDto.DanhSachMaCT = chiTietList.Select(ct => ct.MaCT).ToList();

    //            var result = await phieuMuonService.DuyetChiTietPhieuMuon(duyetDto);

    //            if (!result)
    //            {
    //                throw new Exception($"Duyệt phiếu mượn {maPhieuMuon} thất bại.");
    //            }
    //        }

    //        TempData["Message"] = "Duyệt phiếu mượn thành công!";
    //        TempData["MessageType"] = "success";
    //    }
    //    catch (Exception ex)
    //    {
    //        TempData["Message"] = $"Lỗi khi duyệt phiếu mượn: {ex.Message}";
    //        TempData["MessageType"] = "danger";
    //    }

    //    return RedirectToAction("Index");
    //}

    // GET: PhieuMuon/ChiTietPhieuMuon
    public async Task<IActionResult> ChiTietPhieuMuon(int maPM)
    {
        var phieuMuon = await phieuMuonService.LayPMTheoMa(maPM);

        if (phieuMuon == null)
        {
            TempData["Message"] = "Phiếu mượn không tồn tại.";
            TempData["MessageType"] = "danger";
            return RedirectToAction("Index");
        }

        var chiTietPhieuMuonList = await phieuMuonService.LayCTPhieuMuonTheoPM(maPM);

        var model = new ChiTietPhieuMuonResponse
        {
            PhieuMuon = phieuMuon,
            ChiTietPhieuMuonList = chiTietPhieuMuonList
        };

        return PartialView("ChiTietPhieuMuon", model);
    }

    // GET: PhieuMuon/ChiTietDuyetTra
    public async Task<IActionResult> ChiTietDuyetTra(int maPM)
    {
        var phieuMuon = await phieuMuonService.LayPMTheoMa(maPM);
        if (phieuMuon == null)
            return NotFound();

        var chiTietPhieuMuonList = await phieuMuonService.LayCTPhieuMuonTheoPM(maPM);

        var model = new ChiTietPhieuMuonResponse
        {
            PhieuMuon = phieuMuon,
            ChiTietPhieuMuonList = chiTietPhieuMuonList
        };

        return PartialView("ChiTietDuyetTra", model);
    }

    [HttpPost]
    public async Task<IActionResult> DuyetChiTietPhieuMuon([FromBody] DuyetChiTietPhieuMuon dto)
    {
        if (dto == null || dto.DanhSachMaCT == null || !dto.DanhSachMaCT.Any())
            return Json(new { success = false, message = "Không có thiết bị nào được chọn để duyệt." });
        var userId = HttpContext.Session.GetString("UserId");
        dto.userId = userId;
        var result = await phieuMuonService.DuyetChiTietPhieuMuon(dto);

        if (result)
            return Json(new { success = true });
        else
            return Json(new { success = false, message = "Duyệt thất bại." });
    }
}
