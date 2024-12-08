using SimpleBookLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBookLibrary.Service
{
    public interface IBorrowerService
    {
        /// <summary>
        /// 获取借阅人列表
        /// </summary>
        /// <param name="name">借阅人姓名</param>
        /// <returns></returns>
        List<BorrowerEntity> SearchBorrowers(string name);
        /// <summary>
        /// 添加借阅人
        /// </summary>
        /// <param name="name">借阅人名称</param>
        /// <returns></returns>
        BorrowerEntity AddBorrower(string name);
    }
}
