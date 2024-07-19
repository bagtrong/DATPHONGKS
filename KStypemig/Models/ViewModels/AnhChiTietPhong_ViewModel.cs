using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KStypemig.Models.ViewModels
{
    public class AnhChiTietPhong_ViewModel
    {
        
    public Phong Phong { get; set; }
        public IEnumerable<AnhChiTietPhong> AnhChiTietPhongs { get; set; }
    }
    public class AnhChitietListPhong_Viewmodel
    {
        public IPagedList<AnhChiTietPhong_ViewModel> anhChiTietPhong_ViewModels { get; set; }
    }
    public class DatPhong_viewmodel
    {
        public AnhChiTietPhong_ViewModel DatPhong { get; set; }
    }
}