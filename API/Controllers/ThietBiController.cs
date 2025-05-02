using Microsoft.AspNetCore.Mvc;
using API.Model;
using API.Data;
using API.Dto;
using API.Dto.request;
using API.Dto.response;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThietBiController : ControllerBase
    {
       
        private readonly IThietBiRepository thietBiRepository;

        public ThietBiController(IThietBiRepository thietBiRepository)
        {
            this.thietBiRepository = thietBiRepository;
        }

   
        [HttpGet("LayTatCa")]
        public async Task<IActionResult> LayTatCaTB()
        {
            try
            {
                var result = await thietBiRepository.LayTatCa();

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }
        [HttpGet("LayTBTheoDanhMuc/{maDM}")]
        public async Task<IActionResult> LayTBTheoDanhMuc(String maDM)
        {
            try
            {
                var result = await thietBiRepository.LayTBTheoDanhMuc(maDM);

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }
        [HttpGet("LayDanhMuc")]
        public async Task<IActionResult> LayDanhMucTB()
        {
            try
            {
                var result = await thietBiRepository.LayDanhMuc();

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }
        // Thêm mới 
        [HttpPost]
        public async Task<ActionResult<ThemThietBiResponse>> ThemThietBi(ThemThietBiRequest tb)
        {
            if (tb == null)
            {
                return BadRequest("Thông tin thiết bị không hợp lệ."); // Trả về lỗi 400 nếu dữ liệu không hợp lệ
            }
            try
            {
                var tbm = await thietBiRepository.ThemThietBi(tb);
                return Ok(tbm);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi thêm thiết bị: {ex.Message}");
            }
        }

    }
}
