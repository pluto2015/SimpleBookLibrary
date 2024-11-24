using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBookLibrary.Data
{
    /// <summary>
    /// 科室
    /// </summary>
    public class DepartmentEntity:BaseEntity
    {
        /// <summary>
        /// 科室名
        /// </summary>
        [Column(TypeName = "varchar(500)")]
        public string Name { set; get; }
    }
}
