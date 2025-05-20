using Microsoft.AspNetCore.Mvc;
using WEB.Api;
using WEB.Models.Response;
using WEB.Models.Request;
using Microsoft.AspNetCore.Authorization;
using System.Text;

namespace WEB.Controllers
{
    [Authorize(Roles = "Staff")]
    public class PhieuMuonStaffController : Controller
    {
        private readonly PhieuMuonService _phieuMuonService;
        private readonly ThietBiService _thietBiService;

        public PhieuMuonStaffController(PhieuMuonService phieuMuonService, ThietBiService thietBiService)
        {
            _phieuMuonService = phieuMuonService;
            _thietBiService = thietBiService;
        }

        public async Task<IActionResult> Index(int? maPM, bool? tinhTrang)
        {
            var userId = User.Identity.Name; // Lấy ID của user đang đăng nhập
            var allPhieuMuon = await _phieuMuonService.LayPhieuMuon();
            
            // Lọc chỉ lấy phiếu mượn của staff hiện tại
            var phieuMuonList = allPhieuMuon.Where(pm => pm.MaNguoiGui == userId);

            // Lọc theo mã phiếu mượn nếu có
            if (maPM.HasValue)
            {
                phieuMuonList = phieuMuonList.Where(pm => pm.MaPhieuMuon == maPM);
                ViewBag.MaPMFilter = maPM;
            }

            // Lọc theo tình trạng duyệt nếu có
            if (tinhTrang.HasValue)
            {
                phieuMuonList = phieuMuonList.Where(pm => pm.TinhTrang == tinhTrang.Value);
                ViewBag.TinhTrangFilter = tinhTrang;
            }

            return View(phieuMuonList.ToList());
        }

        public async Task<IActionResult> ChiTietPhieuMuon(int maPM)
        {
            var userId = User.Identity.Name;
            var phieuMuon = await _phieuMuonService.LayPMTheoMa(maPM);

            // Kiểm tra xem phiếu mượn có thuộc về staff hiện tại không
            if (phieuMuon == null || phieuMuon.MaNguoiGui != userId)
            {
                return RedirectToAction("Index");
            }

            var chiTietPhieuMuonList = await _phieuMuonService.LayCTPhieuMuonTheoPM(maPM);

            var model = new ChiTietPhieuMuonResponse
            {
                PhieuMuon = phieuMuon,
                ChiTietPhieuMuonList = chiTietPhieuMuonList
            };

            return PartialView("ChiTietPhieuMuon", model);
        }

        public async Task<IActionResult> Create()
        {
            // Lấy danh sách thiết bị để hiển thị trong dropdown
            var thietBis = await _thietBiService.LayTatCaThietBi();
            ViewBag.ThietBis = thietBis;

            // Khởi tạo model với MaNguoiGui
            var model = new ThemPhieuMuonRequest
            {
                MaNguoiGui = User.Identity.Name
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ThemPhieuMuonRequest request)
        {
            // Đảm bảo luôn có MaNguoiGui
            if (string.IsNullOrEmpty(request.MaNguoiGui))
            {
                request.MaNguoiGui = User.Identity.Name;
            }

            // Validate model
            if (!ModelState.IsValid)
            {
                var errorMessages = new StringBuilder();
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        errorMessages.AppendLine(error.ErrorMessage);
                    }
                }

                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, message = errorMessages.ToString() });
                }

                var thietBis = await _thietBiService.LayTatCaThietBi();
                ViewBag.ThietBis = thietBis;
                return View(request);
            }

            // Validate business rules
            if (request.ChiTietPhieuMuons == null || !request.ChiTietPhieuMuons.Any())
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, message = "Phải có ít nhất một thiết bị được mượn" });
                }
                ModelState.AddModelError("", "Phải có ít nhất một thiết bị được mượn");
                var thietBis = await _thietBiService.LayTatCaThietBi();
                ViewBag.ThietBis = thietBis;
                return View(request);
            }

            try
            {
                var result = await _phieuMuonService.ThemPhieuMuon(request);
                
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = true, message = "Tạo phiếu mượn thành công" });
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, message = ex.Message });
                }
                ModelState.AddModelError("", ex.Message);
                var thietBis = await _thietBiService.LayTatCaThietBi();
                ViewBag.ThietBis = thietBis;
                return View(request);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int maPM)
        {
            try
            {
                var userId = User.Identity.Name;
                var phieuMuon = await _phieuMuonService.LayPMTheoMa(maPM);

                // Kiểm tra xem phiếu mượn có tồn tại và thuộc về user hiện tại không
                if (phieuMuon == null || phieuMuon.MaNguoiGui != userId)
                {
                    return Json(new { success = false, message = "Không tìm thấy phiếu mượn hoặc bạn không có quyền xóa phiếu mượn này." });
                }

                // Kiểm tra xem phiếu mượn đã được duyệt chưa
                if (phieuMuon.TinhTrang)
                {
                    return Json(new { success = false, message = "Không thể xóa phiếu mượn đã được duyệt." });
                }

                // Thực hiện xóa phiếu mượn
                var result = await _phieuMuonService.XoaPhieuMuon(maPM);
                return Json(new { success = true, message = "Xóa phiếu mượn thành công." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra khi xóa phiếu mượn. Vui lòng thử lại." });
            }
        }

        public async Task<IActionResult> Edit(int maPM)
        {
            var userId = User.Identity.Name;
            var phieuMuon = await _phieuMuonService.LayPMTheoMa(maPM);

            // Kiểm tra xem phiếu mượn có thuộc về staff hiện tại không
            if (phieuMuon == null || phieuMuon.MaNguoiGui != userId)
            {
                return RedirectToAction("Index");
            }

            // Kiểm tra xem phiếu mượn đã được duyệt chưa
            if (phieuMuon.TinhTrang)
            {
                TempData["Error"] = "Không thể sửa phiếu mượn đã được duyệt.";
                return RedirectToAction("Index");
            }

            var chiTietPhieuMuonList = await _phieuMuonService.LayCTPhieuMuonTheoPM(maPM);
            var thietBis = await _thietBiService.LayTatCaThietBi();
            ViewBag.ThietBis = thietBis;

            var model = new CapNhatPhieuMuonRequest
            {
                MaNguoiGui = phieuMuon.MaNguoiGui,
                ChiTietPhieuMuons = chiTietPhieuMuonList
                    .Where(ct => ct.TinhTrang == 1)
                    .Select(ct => new ChiTietPhieuMuonRequest
                    {
                        MaTB = ct.MaTB,
                        TinhCanThiet = ct.TinhCanThiet,
                        MucDich = ct.MucDich,
                        NgayMuon = ct.NgayMuon,
                        NgayDuKienTra = ct.NgayDuKienTra,
                        SoLuongTBMuon = ct.SoLuongTBMuon
                    }).ToList()
            };

            ViewBag.MaPM = maPM;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int maPM, CapNhatPhieuMuonRequest request)
        {
            // Đảm bảo luôn có MaNguoiGui
            if (string.IsNullOrEmpty(request.MaNguoiGui))
            {
                request.MaNguoiGui = User.Identity.Name;
            }

            // Validate model
            if (!ModelState.IsValid)
            {
                var errorMessages = new StringBuilder();
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        errorMessages.AppendLine(error.ErrorMessage);
                    }
                }

                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, message = errorMessages.ToString() });
                }

                var thietBis = await _thietBiService.LayTatCaThietBi();
                ViewBag.ThietBis = thietBis;
                ViewBag.MaPM = maPM;
                return View(request);
            }

            // Validate business rules
            if (request.ChiTietPhieuMuons == null || !request.ChiTietPhieuMuons.Any())
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, message = "Phải có ít nhất một thiết bị được mượn" });
                }
                ModelState.AddModelError("", "Phải có ít nhất một thiết bị được mượn");
                var thietBis = await _thietBiService.LayTatCaThietBi();
                ViewBag.ThietBis = thietBis;
                ViewBag.MaPM = maPM;
                return View(request);
            }

            try
            {
                var result = await _phieuMuonService.CapNhatPhieuMuon(maPM, request);
                
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = true, message = "Cập nhật phiếu mượn thành công" });
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, message = ex.Message });
                }
                ModelState.AddModelError("", ex.Message);
                var thietBis = await _thietBiService.LayTatCaThietBi();
                ViewBag.ThietBis = thietBis;
                ViewBag.MaPM = maPM;
                return View(request);
            }
        }
    }
} 