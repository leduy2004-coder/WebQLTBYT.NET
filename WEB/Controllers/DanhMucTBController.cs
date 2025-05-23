using Microsoft.AspNetCore.Mvc;
using WEB.Api;
using WEB.Models;
using WEB.Models.Request;

namespace WEB.Controllers
{
    public class DanhMucTBController : Controller
    {
        private readonly DanhMucTBService _danhMucTBService;

        public DanhMucTBController(DanhMucTBService danhMucTBService)
        {
            _danhMucTBService = danhMucTBService;
        }

        public async Task<IActionResult> Index()
        {
            var danhMucs = await _danhMucTBService.LayTatCa();
            return View(danhMucs);
        }

        [HttpPost]
        public async Task<IActionResult> ThemDanhMuc(DanhMucTBRequest request)
        {
            try
            {
                var result = await _danhMucTBService.ThemDanhMuc(request);
                if (result != null)
                {
                    TempData["Message"] = "Thêm danh mục thành công!";
                    TempData["MessageType"] = "success";
                }
                else
                {
                    TempData["Message"] = "Thêm danh mục thất bại!";
                    TempData["MessageType"] = "danger";
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Có lỗi xảy ra: " + ex.Message;
                TempData["MessageType"] = "danger";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CapNhatDanhMuc(string maDanhMuc, DanhMucTBRequest request)
        {
            try
            {
                var result = await _danhMucTBService.CapNhatDanhMuc(maDanhMuc, request);
                if (result != null)
                {
                    TempData["Message"] = "Cập nhật danh mục thành công!";
                    TempData["MessageType"] = "success";
                }
                else
                {
                    TempData["Message"] = "Cập nhật danh mục thất bại!";
                    TempData["MessageType"] = "danger";
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Có lỗi xảy ra: " + ex.Message;
                TempData["MessageType"] = "danger";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> XoaDanhMuc(string maDanhMuc)
        {
            try
            {
                var result = await _danhMucTBService.XoaDanhMuc(maDanhMuc);
                if (result)
                {
                    TempData["Message"] = "Xóa danh mục thành công!";
                    TempData["MessageType"] = "success";
                }
                else
                {
                    TempData["Message"] = "Xóa danh mục thất bại!";
                    TempData["MessageType"] = "danger";
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Có lỗi xảy ra: " + ex.Message;
                TempData["MessageType"] = "danger";
            }
            return RedirectToAction("Index");
        }
    }
} 