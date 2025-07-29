using System.ComponentModel.DataAnnotations.Schema;

namespace BVTamDuc.Models
{
    [Table("phong_ban")]

    public class PhongBan
    {
        public int id { get; set; }
        public string ten_phong_ban { get; set; }
        public string mo_ta { get; set; }

        public List<NhanSu> NhanSus { get; set; } = new List<NhanSu>();

    }
}