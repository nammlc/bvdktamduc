using Microsoft.AspNetCore.Mvc.RazorPages;
using BVTamDuc.Models;
using BVTamDuc.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace BVTamDuc.Pages;

public class TinTucModel : PageModel
{
    private readonly MyDbContext _context;



    public TinTucModel(MyDbContext context)
    {
        _context = context;
    }

    public string SearchTerm { get; set; }
    public int Page { get; set; } = 1;
    public int TotalPages { get; set; }
    public List<BaiViet> BaiVietList { get; set; }

    private const int PageSize = 9;

    public void OnGet()
    {
        // Lấy giá trị từ query string thay vì dùng [BindProperty]
        SearchTerm = Request.Query["SearchTerm"];
        var pageStr = Request.Query["Page"];

        if (!string.IsNullOrEmpty(pageStr) && int.TryParse(pageStr, out int page))
        {
            Page = page;
        }

        var query = _context.BaiViet.AsQueryable();

        if (!string.IsNullOrEmpty(SearchTerm))
        {
            query = query.Where(b => b.tieu_de.Contains(SearchTerm));
        }

        int totalItems = query.Count();
        TotalPages = (int)Math.Ceiling(totalItems / (double)PageSize);

        BaiVietList = query
            .OrderByDescending(b => b.ngay_dang)
            .Skip((Page - 1) * PageSize)
            .Take(PageSize)
            .ToList();
    }
}
