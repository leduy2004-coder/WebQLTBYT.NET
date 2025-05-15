using Microsoft.AspNetCore.Mvc;
using WEB.Api;
using WEB.Models.Response;
using WEB.Models.Request;
using Microsoft.AspNetCore.Authorization;

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

        public async Task<IActionResult> Index(int? maPM)
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ThemPhieuMuonRequest request)
        {
            // Set MaNguoiGui trước khi validate model
            request.MaNguoiGui = User.Identity.Name;

            if (!ModelState.IsValid)
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, message = "Dữ liệu không hợp lệ" });
                }
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
                    return Json(new { success = false, message = "Có lỗi xảy ra khi thêm phiếu mượn. Vui lòng thử lại." });
                }
                ModelState.AddModelError("", "Có lỗi xảy ra khi thêm phiếu mượn. Vui lòng thử lại.");
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
    }
} 