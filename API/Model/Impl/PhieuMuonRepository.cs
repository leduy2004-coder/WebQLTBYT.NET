using API.Data;
using API.Dto.request;
using API.Dto.response;
using Mapster;
using Microsoft.EntityFrameworkCore;


namespace API.Model.Impl
{
    public class PhieuMuonRepository : IPhieuMuonRepository
    {
        private readonly AppDbContext _context;

        public PhieuMuonRepository(AppDbContext context)
        {
            _context = context;
        }

       
    }
}
