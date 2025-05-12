using API.Dto;
using API.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ThongKeController : ControllerBase
{
    private readonly IThongKeRepository _thongKeRepository;

    public ThongKeController(IThongKeRepository thongKeRepository)
    {
        _thongKeRepository = thongKeRepository;
    }

    [HttpGet("ThongKeTongQuan/{nam}")]
    public async Task<IActionResult> ThongKeTongQuan(int nam)
    {
        var result = await _thongKeRepository.ThongKeTongQuan(nam);
        return Ok(result);
    }

    [HttpGet("PhanBoTheoDanhMuc")]
    public async Task<IActionResult> PhanBoTheoDanhMuc()
    {
        var result = await _thongKeRepository.PhanBoTheoDanhMuc();
        return Ok(result);
    }

    [HttpGet("ThongKeMuonTraTheoThang/{nam}")]
    public async Task<IActionResult> ThongKeMuonTraTheoThang(int nam)
    {
        var result = await _thongKeRepository.ThongKeMuonTraTheoThang(nam);
        return Ok(result);
    }

    [HttpGet("XuHuongNhapThietBiTheoThang/{nam}")]
    public async Task<IActionResult> XuHuongNhapThietBiTheoThang(int nam)
    {
        var result = await _thongKeRepository.XuHuongNhapThietBiTheoThang(nam);
        return Ok(result);
    }

    [HttpGet("ThietBiMuonNhieuNhat/{nam}/{top}")]
    public async Task<IActionResult> ThietBiMuonNhieuNhat(int nam, int top)
    {
        var result = await _thongKeRepository.ThietBiMuonNhieuNhat(nam, top);
        return Ok(result);
    }
}