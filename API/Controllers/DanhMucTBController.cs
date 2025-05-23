using API.Data;
using API.Dto.request;
using API.Dto.response;
using API.Model;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DanhMucTBController : ControllerBase
    {
        private readonly IDanhMucTBRepository _danhMucTBRepository;

        public DanhMucTBController(IDanhMucTBRepository danhMucTBRepository)
        {
            _danhMucTBRepository = danhMucTBRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DanhMucTBResponse>>> LayTatCa()
        {
            var danhMucs = await _danhMucTBRepository.LayTatCa();
            return Ok(danhMucs.Adapt<IEnumerable<DanhMucTBResponse>>());
        }

        [HttpGet("{maDanhMuc}")]
        public async Task<ActionResult<DanhMucTBResponse>> LayTheoMa(string maDanhMuc)
        {
            var danhMuc = await _danhMucTBRepository.LayTheoMa(maDanhMuc);
            if (danhMuc == null)
                return NotFound();

            return Ok(danhMuc.Adapt<DanhMucTBResponse>());
        }

        [HttpPost]
        public async Task<ActionResult<DanhMucTBResponse>> ThemDanhMuc(DanhMucTBRequest request)
        {
            var danhMuc = request.Adapt<DanhMucTB>();
            var result = await _danhMucTBRepository.ThemDanhMuc(danhMuc);
            return CreatedAtAction(nameof(LayTheoMa), new { maDanhMuc = result.MaDanhMuc }, result.Adapt<DanhMucTBResponse>());
        }

        [HttpPut("{maDanhMuc}")]
        public async Task<ActionResult<DanhMucTBResponse>> CapNhatDanhMuc(string maDanhMuc, DanhMucTBRequest request)
        {
            if (maDanhMuc != request.MaDanhMuc)
                return BadRequest();

            var danhMuc = request.Adapt<DanhMucTB>();
            try
            {
                var result = await _danhMucTBRepository.CapNhatDanhMuc(danhMuc);
                return Ok(result.Adapt<DanhMucTBResponse>());
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{maDanhMuc}")]
        public async Task<IActionResult> XoaDanhMuc(string maDanhMuc)
        {
            var result = await _danhMucTBRepository.XoaDanhMuc(maDanhMuc);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
} 