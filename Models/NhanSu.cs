using System.ComponentModel.DataAnnotations.Schema;

namespace BVTamDuc.Models
{
    [Table("nhan_su")]

    public class NhanSu
    {
        public int? id { get; set; }
        public string? ten_nhan_su { get; set; }
        public string? sdt_nhan_su { get; set; }
        public string? chuc_vu { get; set; }
        public string? mo_ta { get; set; }
        public int? phong_ban_id { get; set; }  // nullable do ON DELETE SET NULL
        public PhongBan PhongBan { get; set; }
        public string? anh_dai_dien{ get; set; }

    }
}