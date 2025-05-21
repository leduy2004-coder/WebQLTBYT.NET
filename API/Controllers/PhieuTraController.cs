using Microsoft.AspNetCore.Mvc;
using API.Model;
using API.Data;
using API.Dto.request;
using API.Model.Impl;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuTraController : ControllerBase
    {

        private readonly IPhieuTraRepository phieuTraRepository;
        private readonly ICTPhieuTraRepository cTPhieuTraRepository;

        public PhieuTraController(IPhieuTraRepository phieuTraRepository, ICTPhieuTraRepository cTPhieuTraRepository)
        {
            this.phieuTraRepository = phieuTraRepository;
            this.cTPhieuTraRepository = cTPhieuTraRepository;
        }

        [HttpGet("LayTatCaPhieuTra")]
        public async Task<IActionResult> LayTatCaPT()
        {
            try
            {
                var result = await phieuTraRepository.LayTatCa();

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }
        [HttpGet("LayPTTheoMa/{maPT}")]
        public async Task<IActionResult> LayPTTheoMa(int maPT)
        {
            try
            {
                var result = await phieuTraRepository.LayPTTheoMa(maPT);

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }
        [HttpGet("LayCTPTTheoPhieuTra/{maPT}")]
        public async Task<IActionResult> LayCTPTTheoPT(int maPT)
        {
            try
            {
                var result = await cTPhieuTraRepository.LayCTPTTheoMaPT(maPT);

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }

        [HttpPost("DuyetPhieuTra")]
        public async Task<IActionResult> DuyetPhieuTra([FromBody] DuyetPhieuTraRequest request)
        {
            try
            {
                var result = await phieuTraRepository.DuyetPhieuTra(request);
                if (result)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound(new { message = "Không tìm thấy phiếu trả." });
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error processing request.");
            }
        }
        [HttpDelete("{maPT}")]
        public async Task<IActionResult> XoaPhieuTra(int maPT)
        {
            try
            {
                var result = await phieuTraRepository.XoaPhieuTra(maPT);
                return Ok(maPT);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data.");
            }
        }
        [HttpPost("ThemPhieuTra")]
        public async Task<ActionResult> ThemPhieuTra(PhieuTraRequest phieuTra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Trả về chi tiết lỗi
            }
            try
            {
                var phieu = await phieuTraRepository.ThemPhieuTra(phieuTra);
                return Ok(phieu);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating data.");
            }
        }
        [HttpPost("ThemCTPhieuTra")]
        public async Task<ActionResult> ThemCTPhieuTra(CTPhieuTraRequest chiTiet)
        {
            try
            {
                var phieu = await cTPhieuTraRepository.ThemCTPhieuTra(chiTiet);
                return Ok(phieu);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating data.");
            }
        }

        [HttpGet("LayCTPTTheoTinhTrangVaNguoiGui/{TT}/{maNG}")]
        public async Task<IActionResult> LayCTPTTheoTinhTrangVaNguoiGui(bool TT, string maNG)
        {
            try
            {
                var result = await cTPhieuTraRepository.LayCTPTTheoTinhTrangVaNguoiGui(TT, maNG);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Lỗi khi lấy dữ liệu.");
            }
        }
    }
}
