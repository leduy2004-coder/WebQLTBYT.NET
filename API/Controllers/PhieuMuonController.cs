using Microsoft.AspNetCore.Mvc;
using API.Model;
using API.Data;
using API.Dto.request;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuMuonController : ControllerBase
    {
       
        private readonly IPhieuMuonRepository phieuMuonRepository;
        private readonly ICTPhieuMuonRepository cTPhieuMuonRepository;

        public PhieuMuonController(IPhieuMuonRepository phieuMuonRepository, ICTPhieuMuonRepository cTPhieuMuonRepository) { 
            this.phieuMuonRepository = phieuMuonRepository;
            this.cTPhieuMuonRepository = cTPhieuMuonRepository;
        }

   
    }
}
