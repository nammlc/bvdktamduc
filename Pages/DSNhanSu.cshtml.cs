using Microsoft.AspNetCore.Mvc.RazorPages;
using BVTamDuc.Models;
using BVTamDuc.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace BVTamDuc.Pages;

public class NhanSuModel : PageModel
{
    private readonly MyDbContext _context;

    public NhanSuModel(MyDbContext context)
    {
        _context = context;
    }

    public List<NhanSu> PagedNhanSu { get; set; } = new();
    public List<PhongBan> PhongBans { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string SearchQuery { get; set; }

    [BindProperty(SupportsGet = true)]
    public int? phongBanId { get; set; }
    public int Page { get; set; } = 1;
    public int? SelectedPhongBanId => phongBanId;

    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }

    private const int PageSize = 9;

    public async Task<IActionResult> OnGetAsync()
    {
        // Lấy giá trị page từ query string, không cần dùng BindProperty
        int page = 1;
        if (Request.Query.ContainsKey("page") && int.TryParse(Request.Query["page"], out var parsedPage))
        {
            page = parsedPage > 0 ? parsedPage : 1;
        }

        // GÁN GIÁ TRỊ page CHO THUỘC TÍNH Page
        Page = page;
        CurrentPage = Page;

        var query = _context.NhanSu.Include(n => n.PhongBan).AsQueryable();

        if (!string.IsNullOrWhiteSpace(SearchQuery))
        {
            var keyword = SearchQuery.Trim();
            query = query.Where(n => n.ten_nhan_su.Contains(keyword));
        }

        if (phongBanId.HasValue)
        {
            query = query.Where(n => n.phong_ban_id == phongBanId.Value);
        }

        int totalItems = await query.CountAsync();
        TotalPages = (int)Math.Ceiling(totalItems / (double)PageSize);

        PagedNhanSu = await query
            .Skip((CurrentPage - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();

        PhongBans = await _context.PhongBan.ToListAsync();

        return Page();
    }
}

