using System.ComponentModel.DataAnnotations.Schema;

namespace BVTamDuc.Models
{
    [Table("noi_dung_bai_viet")]
    public class NoiDungBaiViet
    {
        public int id { get; set; }
        public int bai_viet_id { get; set; }
        public string loai_noi_dung { get; set; }
        public string tieu_de_doan_van{ get; set; }
        public string noi_dung { get; set; } // đoạn văn hoặc đường dẫn ảnh

        public int thu_tu { get; set; }

        [ForeignKey("bai_viet_id")]
        public BaiViet BaiViet { get; set; }
    }
}