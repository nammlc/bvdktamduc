// File: Data/MyDbContext.cs
using Microsoft.EntityFrameworkCore;
using BVTamDuc.Models;

namespace BVTamDuc.Data
{
    public class MyDbContext : DbContext
    {
        public DbSet<BaiViet> BaiViet { get; set; }
        public DbSet<PhongBan> PhongBan { get; set; }
        public DbSet<NhanSu> NhanSu { get; set; }
        public DbSet<NoiDungBaiViet> NoiDungBaiViet{ get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình mối quan hệ một-nhiều giữa PhongBan và NhanSu
            modelBuilder.Entity<NhanSu>()
                .HasOne(n => n.PhongBan)
                .WithMany(p => p.NhanSus)
                .HasForeignKey(n => n.phong_ban_id)
                .OnDelete(DeleteBehavior.SetNull); // Khi xóa Phòng ban thì để null trong bảng NhanSu

            base.OnModelCreating(modelBuilder);
        }
    }
}
