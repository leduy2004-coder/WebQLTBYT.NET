
using Microsoft.AspNetCore.Mvc;
using WEB.Api;
using WEB.Models.Response;

namespace WEB.Controllers;

public class TrangChuController : Controller
{
   
    private readonly ThietBiService thietBiService;
    public TrangChuController(ThietBiService thietBiService)
    {
        this.thietBiService = thietBiService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(String maDM)
    {
        if(string.IsNullOrEmpty(maDM))
        {
            maDM = "DM01"; 
        }
        var listDanhMuc = await thietBiService.LayDanhMuc();
        var listThietBi = await thietBiService.LayTBTheoDM(maDM);
        var model = new TrangChuResponse
        {
            listDanhMuc = listDanhMuc.ToList(),
            maDMDuocChon = maDM,
            listTBTheoDM = listThietBi.ToList(),   
        };

        return View(model);
    }

}
