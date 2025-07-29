using Microsoft.AspNetCore.Mvc.RazorPages;
using BVTamDuc.Models;
using BVTamDuc.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BVTamDuc.Pages;

public class TrangChuModel : PageModel
{
    private readonly MyDbContext _context;

    public List<BaiViet> BaiVietList { get; set; }
    public List<BaiViet> TinNoiBat { get; set; }
    public List<PhongBan> DanhSachPhongBan { get; set; }
    public List<NhanSu> BacSiList { get; set; }

    public TrangChuModel(MyDbContext context)
    {
        _context = context;
    }

    public void OnGet()
    {
        // Bài viết mới nhất
        BaiVietList = _context.BaiViet
            .OrderByDescending(b => b.ngay_dang)
            .Take(9)
            .ToList();

        // Tin nổi bật
        TinNoiBat = _context.BaiViet
            .OrderByDescending(b => b.ngay_dang)
            .Take(6)
            .ToList();

        // Lấy danh sách 6 phòng ban (đơn vị chuyên khoa)
        DanhSachPhongBan = _context.PhongBan
            .OrderBy(p => p.ten_phong_ban)
            .Take(6)
            .ToList();

        BacSiList = _context.NhanSu.ToList();


    }
}
