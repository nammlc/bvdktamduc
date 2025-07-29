using Microsoft.AspNetCore.Mvc.RazorPages;
using BVTamDuc.Models;
using BVTamDuc.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace BVTamDuc.Pages;

public class ChiTietBaiVietModel : PageModel
{
    private readonly MyDbContext _context;

    public List<BaiViet> BaiVietList { get; set; }
    public List<BaiViet> TinNoiBat { get; set; }
    public List<PhongBan> DanhSachPhongBan { get; set; }
    public List<NhanSu> BacSiList { get; set; }

    public ChiTietBaiVietModel(MyDbContext context)
    {
        _context = context;
    }

    public string tieu_de { get; set; }
    public DateTime ngay_dang { get; set; }
    public int luot_xem { get; set; }
    public List<NoiDungBaiViet> NoiDungBaiViets { get; set; }
    public BaiViet baiViet { get; set; }

    public IActionResult OnGet(int id) 
    {
        baiViet = _context.BaiViet.FirstOrDefault(b => b.id == id);
        tieu_de = baiViet.tieu_de;
        ngay_dang = baiViet.ngay_dang;
        luot_xem = baiViet.luot_xem;

        if (baiViet == null)
        {
            return RedirectToPage("/");
        }

        NoiDungBaiViets = _context.NoiDungBaiViet
            .Where(n => n.bai_viet_id == id)
            .OrderBy(n => n.thu_tu)
            .ToList();

        return Page(); 
    }
}
