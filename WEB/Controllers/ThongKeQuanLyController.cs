using Microsoft.AspNetCore.Mvc;
using WEB.Api;
using WEB.Models;
using WEB.Models.Response;
using System.Threading.Tasks;
using System.Linq;

namespace WEB.Controllers;

public class ThongKeQuanLyController : Controller
{
    private readonly PhieuTraService phieuTraService;
    private readonly ThietBiService thietBiService;
    private readonly DanhMucTBService danhMucTBService;

    public ThongKeQuanLyController(
        PhieuTraService phieuTraService, 
        ThietBiService thietBiService,
        DanhMucTBService danhMucTBService)
    {
        this.phieuTraService = phieuTraService;
        this.thietBiService = thietBiService;
        this.danhMucTBService = danhMucTBService;
    }

    public async Task<IActionResult> Index()
    {
        var danhMucs = await danhMucTBService.LayTatCa();
        return View(danhMucs);
    }

    [HttpGet]
    public async Task<IActionResult> GetDeviceList(string type, string categoryId = null)
    {
        try
        {
            var devices = await thietBiService.LayTatCaTB();
            ViewBag.ListType = type;
            
            switch (type.ToLower())
            {
                case "total":
                    return PartialView("_DeviceList", devices);
                case "available":
                    var availableDevices = devices.Where(d => d.SoLuongCon > 0).ToList();
                    return PartialView("_DeviceList", availableDevices);
                case "category":
                    if (!string.IsNullOrEmpty(categoryId))
                    {
                        var categoryDevices = devices.Where(d => d.MaDanhMuc == categoryId).ToList();
                        var category = await danhMucTBService.LayTatCa();
                        ViewBag.CategoryName = category.FirstOrDefault(c => c.MaDanhMuc == categoryId)?.TenDanhMuc ?? categoryId;
                        return PartialView("_DeviceList", categoryDevices);
                    }
                    return BadRequest("Category ID is required");
                default:
                    return BadRequest("Invalid type");
            }
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}