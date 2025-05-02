
using API.Data;
using API.Dto;
using Microsoft.EntityFrameworkCore;



namespace API.Model
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


        public DbSet<NguoiDung> NguoiDung { get; set; }
        public DbSet<ThietBi> ThietBi { get; set; }
        public DbSet<DanhMucTB> DanhMucTB { get; set; }
        public DbSet<ChiTietPhieuNhap> ChiTietPhieuNhap { get; set; }
        public DbSet<PhieuNhap> PhieuNhap { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

    }
}

