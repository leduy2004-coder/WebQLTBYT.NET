﻿using Microsoft.AspNetCore.Mvc;
using API.Model;
using API.Data;
using API.Dto.request;
using API.Model.Impl;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuMuonController : ControllerBase
    {
        private readonly IPhieuMuonRepository phieuMuonRepository;
        private readonly ICTPhieuMuonRepository cTPhieuMuonRepository;

        public PhieuMuonController(IPhieuMuonRepository phieuMuonRepository, ICTPhieuMuonRepository cTPhieuMuonRepository)
        {
            this.phieuMuonRepository = phieuMuonRepository;
            this.cTPhieuMuonRepository = cTPhieuMuonRepository;
        }

        [HttpGet("LayTatCaPhieuMuon")]
        public async Task<IActionResult> LayTatCaPM()
        {
            try
            {
                var result = await phieuMuonRepository.LayTatCa();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Lỗi khi lấy dữ liệu.");
            }
        }

        [HttpGet("LayPMTheoMa/{maPM}")]
        public async Task<IActionResult> LayPMTheoMa(int maPM)
        {
            try
            {
                var result = await phieuMuonRepository.LayPMTheoMa(maPM);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Lỗi khi lấy dữ liệu.");
            }
        }

        [HttpGet("LayCTPMTheoPhieuMuon/{maPM}")]
        public async Task<IActionResult> LayCTPMTheoPM(int maPM)
        {
            try
            {
                var result = await cTPhieuMuonRepository.LayCTPMTheoMaPM(maPM);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Lỗi khi lấy dữ liệu.");
            }
        }

        [HttpPost("DuyetCTPhieuMuon")]
        public async Task<IActionResult> DuyetCTPhieuMuon([FromBody] DuyetChiTietPhieuMuon dto)
        {
            try
            {
                var thanhCong = await cTPhieuMuonRepository.DuyetCTPhieuMuon(dto);

                if (thanhCong)  
                    return Ok(new { Message = "Duyệt thành công." });
                else
                    return BadRequest(new { Message = "Không có chi tiết nào được duyệt." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Lỗi khi xử lý duyệt phiếu mượn.");
            }
        }
    }
}
