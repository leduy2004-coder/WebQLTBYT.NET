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
            if (!ModelState.IsValid)
            {
                var thietBis = await _thietBiService.LayTatCaThietBi();
                ViewBag.ThietBis = thietBis;
                return View(request);
            }

            try
            {
                request.MaNguoiGui = User.Identity.Name;
                var result = await _phieuMuonService.ThemPhieuMuon(request);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi thêm phiếu mượn. Vui lòng thử lại.");
                var thietBis = await _thietBiService.LayTatCaThietBi();
                ViewBag.ThietBis = thietBis;
                return View(request);
            }
        }
    }
} 