using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB.Models
{
    public class ChiTietPhieuTra
    {
   
        public int MaCT { get; set; }


        public string MaTB { get; set; }

        public int MaPhieuTra { get; set; }

        public int SoLuongTBTra { get; set; }

        public ThietBi ThietBi { get; set; }
    }
}
