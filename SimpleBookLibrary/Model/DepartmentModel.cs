using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBookLibrary.Model
{
    public partial class DepartmentModel : BaseModel
    {
        /// <summary>
        /// 科室名
        /// </summary>
        [ObservableProperty]
        private string _name;
    }
}
