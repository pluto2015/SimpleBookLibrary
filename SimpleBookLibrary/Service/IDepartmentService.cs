using SimpleBookLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBookLibrary.Service
{
    /// <summary>
    /// 科室
    /// </summary>
    public interface IDepartmentService
    {
        /// <summary>
        /// 通过名称获取科室，科室不存在的新增
        /// </summary>
        /// <param name="name">科室名</param>
        /// <returns>科室</returns>
        DepartmentEntity GetDepartmentByName(string name);
    }
}
