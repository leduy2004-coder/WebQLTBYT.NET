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
        public async Task<IActionResult> DuyetPhieuTra([FromBody] int maPT)
        {
            try
            {
                var result = await phieuTraRepository.DuyetPhieuTra(maPT);
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
    }
}
