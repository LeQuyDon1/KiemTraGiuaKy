using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemTraGiuaKy
{
    public class SinhVienModel
    {
        public string MaSV { get; set; }

      
        public string HoTenSV { get; set; }

        public DateTime NgaySinh { get; set; }

        public string TenLop { get; set; }

        public virtual Lop Lop { get; set; }
    }
}
