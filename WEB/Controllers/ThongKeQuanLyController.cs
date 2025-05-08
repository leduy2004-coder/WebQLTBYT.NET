
using Microsoft.AspNetCore.Mvc;
using WEB.Api;
using WEB.Models;
using WEB.Models.Response;


namespace WEB.Controllers;

public class ThongKeQuanLyController : Controller
{

    private readonly PhieuTraService phieuTraService;
    private readonly ThietBiService thietBiService;
    public ThongKeQuanLyController(PhieuTraService phieuTraService, ThietBiService thietBiService)
    {
        this.phieuTraService = phieuTraService;
        this.thietBiService = thietBiService;
    }


    public async Task<IActionResult> Index()
    {
       

        return View();
    }

}