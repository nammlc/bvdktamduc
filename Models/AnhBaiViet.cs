using System.ComponentModel.DataAnnotations.Schema;

namespace BVTamDuc.Models
{
    [Table("anh_bai_viet")]

    public class AnhBaiViet
    {
        public int? id { get; set; }
        public string? duong_dan_anh { get; set; }
        public string? mo_ta_anh { get; set; }
        public string? thu_tu { get; set; }
        public string? mo_ta { get; set; }
        public int? bai_viet_id { get; set; }  // nullable do ON DELETE SET NULL
        [ForeignKey("bai_viet_id")]

        public BaiViet? baiViet { get; set; }

    }
}