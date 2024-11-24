using SimpleBookLibrary.Data;
using SimpleBookLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBookLibrary.Service
{
    /// <summary>
    /// 借阅记录
    /// </summary>
    public interface IBorrowHistoryService
    {
        /// <summary>
        /// 搜索借阅记录
        /// </summary>
        /// <param name="bookName">书名</param>
        /// <returns>借阅记录</returns>
        List<BorrowHistoryEntity> SearchBorrowHistory(string bookName);

    }
}
