using System.ComponentModel.DataAnnotations.Schema;

namespace BVTamDuc.Models
{
    [Table("bai_viet")]

    public class BaiViet
    {
        public int id { get; set; }
        public string tieu_de { get; set; }
        public string mo_ta_ngan { get; set; }
        public string noi_dung { get; set; }
        public string tac_gia { get; set; }
        public DateTime ngay_dang { get; set; }
        public string anh_dai_dien { get; set; }
        public string trang_thai { get; set; }
        public int luot_xem { get; set; }
        public List<AnhBaiViet> AnhBaiViets { get; set; }

    }
}