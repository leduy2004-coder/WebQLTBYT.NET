using Microsoft.AspNetCore.Mvc;
using API.Model;
using API.Data;
using API.Dto.request;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuNhapController : ControllerBase
    {
       
        private readonly IPhieuNhapRepository phieuNhapRepository;
        private readonly ICTPhieuNhapRepository CTPhieuNhapRepository;

        public PhieuNhapController(IPhieuNhapRepository phieuNhapRepository, ICTPhieuNhapRepository CTPhieuNhapRepository) { 
            this.phieuNhapRepository = phieuNhapRepository;
            this.CTPhieuNhapRepository = CTPhieuNhapRepository;
        }

   
        [HttpGet("LayTatCaPhieuNhap")]
        public async Task<IActionResult> LayTatCaPN()
        {
            try
            {
                var result = await phieuNhapRepository.LayTatCa();

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }
        [HttpGet("LayPNTheoMa/{maPN}")]
        public async Task<IActionResult> LayPNTheoMa(int maPN)
        {
            try
            {
                var result = await phieuNhapRepository.LayPNTheoMa(maPN);

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }
        [HttpGet("LayCTPNTheoPhieuNhap/{maPN}")]
        public async Task<IActionResult> LayCTPNTheoPN(int maPN)
        {
            try
            {
                var result = await CTPhieuNhapRepository.LayCTPNTheoPN(maPN);

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }
        [HttpPost("ThemPhieuNhap")]
        public async Task<ActionResult> ThemPhieuNhap(PhieuNhapRequest phieuNhap)
        {
            try
            {
                var phieu = await phieuNhapRepository.ThemPhieuNhap(phieuNhap);
                return Ok(phieu);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating data.");
            }
        }
        [HttpPost("ThemCTPhieuNhap")]
        public async Task<ActionResult> ThemCTPhieuNhap(CTPhieuNhapRequest chiTiet)
        {
            try
            {
                var phieu = await CTPhieuNhapRepository.ThemCTPhieuNhap(chiTiet);
                return Ok(phieu);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating data.");
            }
        }
        [HttpDelete("{maPN}")]
        public async Task<IActionResult> XoaPhieuNhap(int maPN)
        {
            try
            {
                var result = await phieuNhapRepository.XoaPhieuNhap(maPN);
                return Ok(maPN);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data.");
            }
        }
    }
}
