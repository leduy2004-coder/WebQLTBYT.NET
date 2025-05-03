


namespace API.Model.Impl
{
    public class CTPhieuMuonRepository : ICTPhieuMuonRepository
    {
        private readonly AppDbContext _context;

        public CTPhieuMuonRepository(AppDbContext context)
        {
            _context = context;
        }

       
    }
}
